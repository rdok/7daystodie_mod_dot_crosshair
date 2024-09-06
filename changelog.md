## Changelog

#### 1.4.0 31-Aug-24

- feat: Gear option `Ranged Weapons`; disable this to use the vanilla crosshair when holding ranged weapons.
    - [As per discussion.](https://www.nexusmods.com/7daystodie/mods/5640?tab=posts&jump_to_comment=143481219&BH=0)

#### 1.3.3 24-Aug-24

- fix: Change the default dot size from 5 to 8 as the round dot is less visible than previously default square dot.

#### 1.3.2 21-Aug-24

- fix: Regression issue with Gears not loading properly
- fix: Remove debugging logs on production

#### 1.3.1 21-Aug-24

- fix: Compatability fix with other mods that hide the crosshair. Tested:
    - [Danzo No Crosshair](https://www.nexusmods.com/7daystodie/mods/3252)
    - [Black Wolf's better ADS accuracy and no crosshair](https://www.nexusmods.com/7daystodie/mods/5832)

#### 1.3.0 21-Aug-24

- feat: Add alternative dot shape: round. Configurable through the Gears, the in game mod settings manager.
- feat: Set default crosshair shape to round as it feels much better.
- feat: Moved square only shadow settings to its own tab.
- fix: Add missing translations for the tab title

#### 1.2.1 16-Aug-24

- fix: Use default (white) color when not using Gears.

#### 1.2.0 16-Aug-24

- feat: Optionally configurable dot size, color, and shadow
  using [Gears - A Mod Settings Manager](https://www.nexusmods.com/7daystodie/mods/4017)
    - Use https://www.color-hex.com/ to generate your RGBA color.
    - The mod will still work with the default values if you are not using Gears.
    - Translated to all in game languages.

#### 1.1.0 12-Aug-24

- feat: Deprecated rendering logic using resource tools; use Immersive Crosshair instead.
    - fix: Resolved an issue where the Immersive Crosshair enabled the crosshair, but the dot failed to render.
- feat: Global version deprecated; Dot Crosshair now replaces the crosshair universally.

#### 1.0.4 Main Version 10-Aug-24

- fix: Replace the crosshair when holding any resource based tool instead of non-ranged weapons.
    - Apparently there are ranged repair tools such as nail guns
    - Many thanks to [MrSamuelAdams1992](https://next.nexusmods.com/profile/MrSamuelAdams1992/about-me?gameId=1059)
      for [reporting this issue](https://www.nexusmods.com/7daystodie/mods/5601?tab=posts&jump_to_comment=142699761)

#### 1.0.3 Main Version & v11.0.1 Global Dot 08 Aug 2024

- Decreases dot crosshair size by 14%
- Decreases dot crosshair shadow size by 20%, better alignment with dot.
- [As per discussion.](https://www.nexusmods.com/7daystodie/mods/5640?tab=posts&jump_to_comment=142559019)

#### 1.0.2 03 Aug 2024

- Decreases crosshair transparency by 25%.

#### 1.0.1 03 Aug 2024

- Adds missing dot shadow and decreases size.

#### 1.0.0 03 Aug 2024

- Replace the default (X) crosshair with a dot.
- Preserves the default functionality for ranged weapons.
    - Optional Mod file: Dot Crosshair Global. Install this instead of the main version if you want the dot crosshair
      globally.
- Compatible with any other mods.
 
