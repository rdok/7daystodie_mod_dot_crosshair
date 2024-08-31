using System.Linq;
using JetBrains.Annotations;

namespace DotCrosshair.Harmony
{
    public class Tags
    {
        private static readonly ILogger Logger = new Logger();

        public static bool HasTags(
            [CanBeNull] ItemAction itemAction, string[] tagNames, TagCheckType checkType
        )
        {
            if (itemAction?.item == null) return false;

            if (!itemAction.item.Properties.Values.ContainsKey("Tags"))
            {
                Logger.Info("HasAnyTag: No tags in _itemAction.item.Properties.Values");
                return false;
            }

            var tags = itemAction.item.Properties.Values["Tags"];
            Logger.Info($"HasAnyTag: Item Tags: {tags}");
            var tagsArray = tags.Split(',');
            bool hasTags;

            switch (checkType)
            {
                case TagCheckType.Any:
                    hasTags = tagNames.Any(tagName => tagsArray.Contains(tagName));
                    break;
                case TagCheckType.All:
                    hasTags = tagNames.All(tagName => tagsArray.Contains(tagName));
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

        public enum TagCheckType
        {
            Any,
            All
        }
    }
}