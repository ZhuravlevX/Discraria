using Terraria.ModLoader;
using DiscordRPC;
using Discraria.Script;
using Discraria.Script.Tools;
using System.Threading.Tasks;

namespace Discraria
{
    public class Discraria : Mod
    {
        public static DiscordRpcClient client;

        public override async void Load()
        {
            Logger.Info("[DiscordRPC] Load called");

            string appId = Config.Instance?.ApplicationID;

            if (string.IsNullOrWhiteSpace(appId))
                appId = "731023188930199662";

            client = new DiscordRpcClient(appId);
            client.Initialize();

            Logger.Info("[DiscordRPC] Using AppID: " + appId);

            await Task.Delay(4000);

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
            client?.SetPresence(new RichPresence()
            {
                Details = Utils.GetTranslation("RichPresence", "Menu"),
                Assets = new Assets()
                {
                    LargeImageKey = "terraria"
                }
            });

            ModContent.GetInstance<Discraria>().Logger.Info("[DiscordRPC] Presence set: In main menu");
        }
    }
}