using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace DotCrosshair.Harmony
{
    public static class ModSettings
    {
        private static readonly ILogger Logger = new Logger();
        private static XDocument _settingsXml;

        private static void EnsureSettingsLoaded()
        {
            if (_settingsXml != null) return;

            var modPath = Assembly.GetExecutingAssembly().Location;
            var modDirectory = Path.GetDirectoryName(modPath);
            var xmlPath = Path.Combine(modDirectory, "ModSettings.xml");
            _settingsXml = XDocument.Load(xmlPath);

            Logger.Debug($"Settings loaded from: {xmlPath}");
        }

        private static string GetSettingValue(string tab, string category, string settingName)
        {
            EnsureSettingsLoaded();
            var value = _settingsXml.Element("ModSettings")
                ?.Element("Global")
                ?.Elements("Tab")
                ?.FirstOrDefault(e => e.Attribute("name")?.Value == tab)
                ?.Elements("Category")
                ?.FirstOrDefault(e => e.Attribute("name")?.Value == category)
                ?.Elements()
                ?.FirstOrDefault(e => e.Attribute("name")?.Value == settingName)
                ?.Attribute("defaultValue")
                ?.Value;

            Logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public static float GetFloat(string tab, string category, string settingName)
        {
            var value = float.Parse(GetSettingValue(tab, category, settingName));
            Logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public static byte GetByte(string tab, string category, string settingName)
        {
            var value = byte.Parse(GetSettingValue(tab, category, settingName));
            Logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public static bool GetBool(string tab, string category, string settingName)
        {
            var value = bool.Parse(GetSettingValue(tab, category, settingName));
            Logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public static DotShape GetDotShape(string tab, string category, string settingName)
        {
            var shapeValue = GetSettingValue(tab, category, settingName);
            var dotShape = DotShape.FromString(shapeValue);
            Logger.Debug($"{tab}.{category}.{settingName}: {dotShape}");
            return dotShape;
        }
    }
}