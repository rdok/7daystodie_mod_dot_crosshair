# 7 Days to Die Mod - Dot Crosshair

[![nexus-mods-collection-immersive-hud](https://img.shields.io/badge/Nexus%20Mods%20Collection-Immersive%20HUD%20-orange?style=flat-square&logo=spinrilla)](https://next.nexusmods.com/7daystodie/collections/epfqzi) [![nexus-mods-page](https://img.shields.io/badge/Nexus%20Mod-Dot%20Crosshair%20-orange?style=flat-square&logo=spinrilla)](https://www.nexusmods.com/7daystodie/mods/5640) [![github-repository](https://img.shields.io/badge/GitHub-Repository-green?style=flat-square&logo=github)](https://github.com/rdok/7daystodie_mod_dot_crosshair)

> **Dot Crosshair:** Replaces the default crosshair with a dot universally.
> **EAC Caveat:** This mod uses custom code that is not compatible with Easy Anti-Cheat (EAC).

[![Dot Crosshairs Showcase](https://github.com/rdok/7daystodie_mod_dot_crosshair/blob/main/documentation/showcase.gif?raw=true)](https://www.nexusmods.com/7daystodie/mods/5640)

## Features
- Universally replaces the default crosshair with a dot.
- [Immersive Crosshair](https://www.nexusmods.com/7daystodie/mods/5601) and [Dot Crosshair](https://www.nexusmods.com/7daystodie/mods/5640) mods are designed to complement each other.
  - Hides crosshair when not having an interactable. E.g. using weapons with iron sights.
- Now, features in-game, optional, configuration options. Requires [Gears](https://www.nexusmods.com/7daystodie/mods/4017) & [Quartz](https://www.nexusmods.com/7daystodie/mods/2409/)
  - Use https://www.color-hex.com/ to generate your RGBA color.
  - The mod will still work with the default values if you are not using Gears.
  - See Videos tab on this NexusMods page for an example of [customizing the crosshair](https://www.nexusmods.com/7daystodie/mods/5640?tab=videos#lg=2&slide=0) to use the Fallout 4 green color.
- Game Version: 1.0. Install with [Vortex](https://www.nexusmods.com/about/vortex/).


### Development
- Use Google sheet to edit the Localization.txt. Just add the english versions.
- To translate to all supported in game languages, using Google Translate API:
  - `npm run translate:localization` everytime you add a new localization.
  - This will translate and replace in place the Localization.txt

## Changelog
#### v1.2.0 16-Aug-24
- feat: Optionally configurable dot size, color, and shadow using [Gears - A Mod Settings Manager](https://www.nexusmods.com/7daystodie/mods/4017)
  - Use https://www.color-hex.com/ to generate your RGBA color.
  - The mod will still work with the default values if you are not using Gears.
#### v1.1.0 12-Aug-24
- feat: Deprecated rendering logic using resource tools; use Immersive Crosshair instead.
  - fix: Resolved an issue where the Immersive Crosshair enabled the crosshair, but the dot failed to render.
- feat: Global version deprecated; Dot Crosshair now replaces the crosshair universally.
#### v1.0.4 Main Version 10-Aug-24
- fix: Replace the crosshair when holding any resource based tool instead of non-ranged weapons.
  - Apparently there are ranged repair tools such as nail guns 
  - Many thanks to [MrSamuelAdams1992](https://next.nexusmods.com/profile/MrSamuelAdams1992/about-me?gameId=1059) for [reporting this issue](https://www.nexusmods.com/7daystodie/mods/5601?tab=posts&jump_to_comment=142699761)
#### v1.0.3 Main Version & v11.0.1 Global Dot 08 Aug 2024
- Decreases dot crosshair size by 14%
- Decreases dot crosshair shadow size by 20%, better alignment with dot.
- [As per discussion.](https://www.nexusmods.com/7daystodie/mods/5640?tab=posts&jump_to_comment=142559019)
#### v1.0.2 03 Aug 2024
- Decreases crosshair transparency by 25%.
#### v1.0.1 03 Aug 2024
- Adds missing dot shadow and decreases size.
#### v1.0.0 03 Aug 2024 
- Replace the default (X) crosshair with a dot.
- Preserves the default functionality for ranged weapons.
    - Optional Mod file: Dot Crosshair Global. Install this instead of the main version if you want the dot crosshair globally.
- Compatible with any other mods.
 