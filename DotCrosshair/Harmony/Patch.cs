using System;
using HarmonyLib;
using UnityEngine;

namespace DotCrosshair.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.guiDrawCrosshair))]
    public static class Patch
    {
        public static bool Prefix(EntityPlayerLocal __instance, NGuiWdwInGameHUD _guiInGame, bool bModalWindowOpen)
        {
            const bool disableGuiDrawCrosshairFunction = false;

            if (!_guiInGame.showCrosshair ||
                Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive ||
                __instance.AttachedToEntity != null)
            {
                Debug.Log("[DotCrosshair] Crosshair not drawn due to invalid player state.");
                return disableGuiDrawCrosshairFunction;
            }

            if (bModalWindowOpen || __instance.inventory == null)
            {
                Debug.Log("[DotCrosshair] Crosshair not drawn due to modal window open or inventory null.");
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

            Debug.Log("[DotCrosshair] Drawing custom crosshair.");
            DotCrosshair.Draw(__instance);

            return disableGuiDrawCrosshairFunction;
        }
    }
}