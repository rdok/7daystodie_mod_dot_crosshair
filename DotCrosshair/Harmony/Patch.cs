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
            Logger.Info("Patch: Prefix called on EntityPlayerLocal.guiDrawCrosshair.");

            var holdingItem = __instance.inventory.holdingItemItemValue;
            var repairToolTags = new[] { "tool", "harvestingSkill", "corpseRemoval", "perkBrawler" };
            var holdingTool = holdingItem?.ItemClass?.Actions?
                .Any(action => HasTags(action, repairToolTags, TagCheckType.Any)) ?? false;

#if MAIN_VERSION || DEBUG
            const bool callGuidDrawCrosshairFunction = true;
            if (!holdingTool)
            {
                Logger.Info("Patch: Player is holding no tool or using hands, aborting.");
                return callGuidDrawCrosshairFunction;
            }
#endif
            const bool disableGuiDrawCrosshairFunction = false;

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

        private enum TagCheckType
        {
            Any,
            All
        }

        private static bool HasTags([CanBeNull] ItemAction _itemAction, string[] tagNames, TagCheckType checkType)
        {
            Logger.Info("HasAnyTag: Checking if the item has any of the specified tags.");

            if (_itemAction == null)
            {
                Logger.Info("HasAnyTag: _itemAction is null");
                return false;
            }

            if (_itemAction.item == null)
            {
                Logger.Info("HasAnyTag: _itemAction.item is null");
                return false;
            }

            if (!_itemAction.item.Properties.Values.ContainsKey("Tags"))
            {
                Logger.Info("HasAnyTag: No tags in _itemAction.item.Properties.Values");
                return false;
            }

            var tags = _itemAction.item.Properties.Values["Tags"];
            Logger.Info($"HasAnyTag: Item Tags: {tags}");

            bool hasTags;

            switch (checkType)
            {
                case TagCheckType.Any:
                    hasTags = tagNames.Any(tagName => tags.Contains(tagName));
                    break;
                case TagCheckType.All:
                    hasTags = tagNames.All(tagName => tags.Contains(tagName));
                    break;
                default:
                    Logger.Info("HasTags: Invalid check type");
                    return false;
            }

            Logger.Info(hasTags
                ? $"The item has {checkType.ToString().ToLower()} of the specified tags."
                : $"The item does not have {checkType.ToString().ToLower()} of the specified tags.");

            return hasTags;
        }
    }
}