using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace DotCrosshair.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), "guiDrawCrosshair")]
    public static class Patch
    {
        private static readonly ILogger Logger = new Logger();

        public static bool Prefix(EntityPlayerLocal __instance, NGuiWdwInGameHUD _guiInGame, bool bModalWindowOpen)
        {
            Logger.Info("Patch: Prefix called on EntityPlayerLocal.guiDrawCrosshair.");

            var holdingItem = __instance.inventory.holdingItemItemValue;
            var holdingRangedWeapon = holdingItem?.ItemClass?.Actions?
                .Any(action => action is ItemActionRanged) ?? false;

            const bool disableGuiDrawCrosshairFunction = false;

#if RELEASE
            const bool callGuidDrawCrosshairFunction = true;
            if (holdingRangedWeapon)
            {
                Logger.Info("Patch: Player is holding a ranged weapon, using default crosshair.");
                return callGuidDrawCrosshairFunction;
            }
#endif

            if (!_guiInGame.showCrosshair || Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive || __instance.AttachedToEntity != null)
            {
                Logger.Info("Patch: Conditions not met for crosshair rendering, skipping.");
                return disableGuiDrawCrosshairFunction;
            }

            if (bModalWindowOpen || __instance.inventory == null)
            {
                Logger.Info(
                    "Patch: Modal window open or inventory is null, skipping crosshair rendering.");
                return disableGuiDrawCrosshairFunction;
            }

            Logger.Info("Patch: Player is not holding a ranged weapon, drawing dot crosshair.");
            DotCrosshair.Draw(__instance);

            return disableGuiDrawCrosshairFunction;
        }
    }
}