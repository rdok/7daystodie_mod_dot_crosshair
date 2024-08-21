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

            if (Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive || __instance.AttachedToEntity != null)
                return disableGuiDrawCrosshairFunction;

            if (bModalWindowOpen || __instance.inventory == null) return disableGuiDrawCrosshairFunction;

            if (_guiInGame.showCrosshair) DotCrosshair.Draw();

            return disableGuiDrawCrosshairFunction;
        }
    }
}