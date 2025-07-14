using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace cedaro.PickUpAtHome;

[StaticConstructorOnStartup]
public class FloatMenuOptionProvider_PickUpItemAtHome : FloatMenuOptionProvider
{
    protected override bool Drafted => true;
    protected override bool Undrafted => true;
    protected override bool Multiselect => false;
    
    protected override bool AppliesInt(FloatMenuContext context)
    {
        return !context.FirstSelectedPawn.IsFormingCaravan() && context.FirstSelectedPawn.Map.IsPlayerHome && context.FirstSelectedPawn.inventory != null;
    }
    
    public override IEnumerable<FloatMenuOption> GetOptionsFor(Thing clickedThing, FloatMenuContext context)
    {
        if (clickedThing.def.category != ThingCategory.Item || !clickedThing.def.EverHaulable || clickedThing.def.orderedTakeGroup != null)
		{
			yield break;
		}
		if (!context.FirstSelectedPawn.CanReach(clickedThing, PathEndMode.ClosestTouch, Danger.Deadly))
		{
			yield return new FloatMenuOption("CannotPickUp".Translate(clickedThing.Label, clickedThing) + ": " + "NoPath".Translate().CapitalizeFirst(), null);
			yield break;
		}
		if (MassUtility.WillBeOverEncumberedAfterPickingUp(context.FirstSelectedPawn, clickedThing, 1))
		{
			yield return new FloatMenuOption("CannotPickUp".Translate(clickedThing.Label, clickedThing) + ": " + "TooHeavy".Translate(), null);
			yield break;
		}
		if (clickedThing.stackCount == 1)
		{
			yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("PickUpOne".Translate(clickedThing.LabelNoCount, clickedThing), delegate
			{
				clickedThing.SetForbidden(value: false, warnOnFail: false);
				Job job = JobMaker.MakeJob(JobDefOf.TakeInventory, clickedThing);
				job.count = 1;
				job.checkEncumbrance = true;
				job.takeInventoryDelay = 120;
				context.FirstSelectedPawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
			}, MenuOptionPriority.High), context.FirstSelectedPawn, clickedThing);
			yield break;
		}
		if (MassUtility.WillBeOverEncumberedAfterPickingUp(context.FirstSelectedPawn, clickedThing, clickedThing.stackCount))
		{
			yield return new FloatMenuOption("CannotPickUpAll".Translate(clickedThing.Label, clickedThing) + ": " + "TooHeavy".Translate(), null);
		}
		else
		{
			yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("PickUpAll".Translate(clickedThing.Label, clickedThing), delegate
			{
				clickedThing.SetForbidden(value: false, warnOnFail: false);
				Job job2 = JobMaker.MakeJob(JobDefOf.TakeInventory, clickedThing);
				job2.count = clickedThing.stackCount;
				job2.checkEncumbrance = true;
				job2.takeInventoryDelay = 120;
				context.FirstSelectedPawn.jobs.TryTakeOrderedJob(job2, JobTag.Misc);
			}, MenuOptionPriority.High), context.FirstSelectedPawn, clickedThing);
		}
		yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("PickUpSome".Translate(clickedThing.LabelNoCount, clickedThing), delegate
		{
			int to = Mathf.Min(MassUtility.CountToPickUpUntilOverEncumbered(context.FirstSelectedPawn, clickedThing), clickedThing.stackCount);
			Dialog_Slider window = new Dialog_Slider("PickUpCount".Translate(clickedThing.LabelNoCount, clickedThing), 1, to, delegate(int count)
			{
				clickedThing.SetForbidden(value: false, warnOnFail: false);
				Job job3 = JobMaker.MakeJob(JobDefOf.TakeInventory, clickedThing);
				job3.count = count;
				job3.checkEncumbrance = true;
				job3.takeInventoryDelay = 120;
				context.FirstSelectedPawn.jobs.TryTakeOrderedJob(job3, JobTag.Misc);
			});
			Find.WindowStack.Add(window);
		}, MenuOptionPriority.High), context.FirstSelectedPawn, clickedThing);
    }
}