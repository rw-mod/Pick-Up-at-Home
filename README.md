# Pick Up At Home (1.6 Update)

This is a **reimplementation** of the [original "Pick Up At Home" mod](https://steamcommunity.com/sharedfiles/filedetails/?id=2982694750) for RimWorld, updated to support **version 1.6**.

In RimWorld 1.5, the original mod added a Float Menu option via **Harmony patching**, allowing pawns to manually pick up items within player-controlled home maps.  
However, as of RimWorld 1.6, this functionality can now be added using the **native `FloatMenuOptionProvider` system**, which this mod takes full advantage of.

⚠️ Version 1.5 still requires Harmony!

---

## ✅ Features

- Adds a **Float Menu option** when right-clicking on haulable items within player home maps.
- Allows selected pawns to **manually pick up items** into their inventory.
- Takes into account:
  - Pawn encumbrance limits
  - Path reachability
  - Stack count and partial pickups (slider dialog included)

---

## 📦 Differences from the Original Mod

- **No Harmony patching** is used — all functionality is built using RimWorld 1.6's **native modding API**.
- Refactored and cleaned up logic for better compatibility with the new FloatMenu system.
- Fully compatible with other FloatMenuOptionProviders in 1.6.

---

## 📌 Notes

- This is **not an official update** by the original author.
- This version was rebuilt by reviewing and understanding the original mod’s functionality, then adapting it using new 1.6 tools.
- The mod should be safe to use in existing saves, but always back up your game just in case.

---

## 📜 Credits

- Original mod by **[Cedar](https://steamcommunity.com/id/CedarO/)** ([Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2982694750))
- 1.6 update and native FloatMenu integration by **[Bounty](https://github.com/b0unt9)**

---

## 🔧 Compatibility

- RimWorld 1.6+
- Likely incompatible with the original mod (disable one or the other)
- Compatible with most other mods unless they interfere with inventory or FloatMenu logic

---

## 💬 Feedback

Issues or suggestions?  
Feel free to leave a comment or contact me via GitHub: [github.com/rw-mod/Pick-Up-at-Home](https://github.com/rw-mod/Pick-Up-at-Home)

---
