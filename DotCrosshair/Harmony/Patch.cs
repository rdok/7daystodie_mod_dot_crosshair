using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace DotCrosshair.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), "guiDrawCrosshair")]
    public static class Patch
    {
        private static readonly ILogger Logger = new Logger();

        public static bool Prefix(EntityPlayerLocal __instance, NGuiWdwInGameHUD _guiInGame, bool bModalWindowOpen)
        {
            const bool disableGuiDrawCrosshairFunction = false;

            if (Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive || __instance.AttachedToEntity != null)
            {
                return disableGuiDrawCrosshairFunction;
            }

            if (bModalWindowOpen || __instance.inventory == null)
            {
                return disableGuiDrawCrosshairFunction;
            }

            if (_guiInGame.showCrosshair)
            {
                DotCrosshair.Draw(__instance);
            }

            return disableGuiDrawCrosshairFunction;
        }
    }
}