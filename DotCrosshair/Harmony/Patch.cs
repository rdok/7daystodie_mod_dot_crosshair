using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace DotCrosshair.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.guiDrawCrosshair))]
    public static class Patch
    {
        public static bool EnabledForRangedWeaponsSetting { get; set; } = true;

        public static bool Prefix(EntityPlayerLocal __instance, NGuiWdwInGameHUD _guiInGame, bool bModalWindowOpen)
        {
            if (!EnabledForRangedWeaponsSetting && HoldingRangedWeapon(__instance))
            {
                return true;
            }

            const bool disableGuiDrawCrosshairFunction = false;

            if (!_guiInGame.showCrosshair ||
                Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive ||
                __instance.AttachedToEntity != null)
            {
                return disableGuiDrawCrosshairFunction;
            }

            if (bModalWindowOpen || __instance.inventory == null)
            {
                return disableGuiDrawCrosshairFunction;
            }

            var crosshairType =
                __instance.inventory.holdingItem.GetCrosshairType(__instance.inventory.holdingItemData);

            if (crosshairType == ItemClass.EnumCrosshairType.Crosshair ||
                crosshairType == ItemClass.EnumCrosshairType.CrosshairOnAiming)
            {
                if (crosshairType == ItemClass.EnumCrosshairType.Crosshair
                    && __instance.AimingGun
                    && !ItemAction.ShowDistanceDebugInfo)
                {
                    return disableGuiDrawCrosshairFunction;
                }
            }

            DotCrosshair.Draw(__instance);

            return disableGuiDrawCrosshairFunction;
        }

        private static bool HoldingRangedWeapon(EntityPlayerLocal entityPlayerLocal)
        {
            if (entityPlayerLocal == null) return false;

            var holdingItem = entityPlayerLocal.inventory.holdingItemItemValue;

            var actions = holdingItem?.ItemClass?.Actions;

            return actions?.Any(action => Tags.HasTags(
                action, new[] { "weapon", "ranged" }, Tags.TagCheckType.All
            )) ?? false;
        }
    }
}