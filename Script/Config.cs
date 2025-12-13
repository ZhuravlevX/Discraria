using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Discraria.Script
{
    [Label("Rich Presence")]
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Discord Application ID")]
        [Tooltip("ID приложения Discord Rich Presence. По умолчанию 731023188930199662")]
        [DefaultValue("731023188930199662")]
        public string ApplicationID;

        public static Config Instance => ModContent.GetInstance<Config>();
    }
}
