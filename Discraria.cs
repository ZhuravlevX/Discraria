using Terraria.ModLoader;
using DiscordRPC;
using Discraria.Scripts;
using Discraria.Scripts.Tools;
using System.Threading.Tasks;

namespace Discraria
{
    public class Discraria : Mod
    {
        public static DiscordRpcClient client;
        public static bool RichPresenceActive = false;

        public override async void Load()
        {
            Logger.Info("[DiscordRPC] Load called");

            Logger.Info("[DiscordRPC] Load called");
            client = new DiscordRpcClient("731023188930199662");
            client.Initialize();

            await Task.Delay(10000);

            SetMenuStatus();
        }

        public override void Unload()
        {
            Logger.Info("[DiscordRPC] Unload called");

            client?.Dispose();
            client = null;
        }

        public static void SetMenuStatus()
        {
            client?.SetPresence(new DiscordRPC.RichPresence()
            {
                Details = Utils.GetTranslation("RichPresence", "Menu"),
                Assets = new Assets()
                {
                    LargeImageKey = "terraria"
                }
            });
            var presence = client?.CurrentPresence;
            Utils.AddLoggerInfo(Config.Instance.ShowLogger, Config.Instance.ShowLoggerChat, "Rich Presence", $"{presence.Details}");
        }
    }
}