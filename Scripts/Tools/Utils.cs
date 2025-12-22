using Terraria.ID;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Discraria.Scripts.Tools
{
    public static class Utils
    {
        public static string GetTranslation(string category, string key)
        {
            return Language.GetText($"Mods.Discraria.{category}.{key}").Value;
        }

        public static void AddLoggerInfo(bool console, bool chat, string tag, string message)
        {
            string log = $"[{tag}] {message}";

            if (console && Config.Instance?.ShowLogger == true)
            {
                ModContent.GetInstance<Discraria>().Logger.Info(log);
            }

            if (chat && Config.Instance?.ShowLoggerChat == true && Main.netMode != NetmodeID.Server)
            {
                Main.NewText(log, 150, 150, 255);
            }
        }
    }
}
