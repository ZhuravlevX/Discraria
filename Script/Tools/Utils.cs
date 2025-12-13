using Terraria.Localization;

namespace Discraria.Script.Tools
{
    public static class Utils
    {
        public static string GetTranslation(string category, string key)
        {
            return Language.GetText($"Mods.Discraria.{category}.{key}").Value;
        }
    }
}
