using Terraria.Localization;

namespace Discraria.Scripts.Tools
{
    public static class Utils
    {
        public static string GetTranslation(string category, string key)
        {
            return Language.GetText($"Mods.Discraria.{category}.{key}").Value;
        }
    }
}
