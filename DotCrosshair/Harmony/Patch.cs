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
            var holdingItem = __instance.inventory.holdingItemItemValue;
            var repairToolTags = new[] { "tool", "harvestingSkill", "knife", "perkBrawler" };
            var holdingTool = holdingItem?.ItemClass?.Actions?
                .Any(action => HasTags(action, repairToolTags, TagCheckType.Any)) ?? false;

#if MAIN_VERSION || DEBUG
            const bool callGuidDrawCrosshairFunction = true;
            if (!holdingTool)
            {
                return callGuidDrawCrosshairFunction;
            }
#endif
            const bool disableGuiDrawCrosshairFunction = false;

            if (!_guiInGame.showCrosshair || Event.current.type != EventType.Repaint ||
                __instance.IsDead() ||
                __instance.emodel.IsRagdollActive || __instance.AttachedToEntity != null)
            {
                return disableGuiDrawCrosshairFunction;
            }

            if (bModalWindowOpen || __instance.inventory == null)
            {
                return disableGuiDrawCrosshairFunction;
            }

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
            if (_itemAction?.item == null) return false;

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