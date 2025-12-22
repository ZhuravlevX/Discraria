using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Discraria.Scripts
{
    [Label("Rich Presence")]
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Main")]

        [Label("Показывать статистику персонажа")]
        [Tooltip("Показывать здоровье, ману и защиту в активности.")]
        [DefaultValue(true)]
        public bool ShowPlayerStats;

        [Label("Показывать сражения с боссами")]
        [Tooltip("Показывать информацию о текущем бою с боссом.")]
        [DefaultValue(true)]
        public bool ShowBossFights;

        [Label("Показывать маленькую иконку мира")]
        [Tooltip("Показывать тип мира в зависимости от багрянца, порчи, хардмода или секретного сида мира.")]
        [DefaultValue(true)]
        public bool ShowWorldType;

        [Label("Показывать смерть")]
        [Tooltip("Показывать свою смерть в активности.")]
        [DefaultValue(true)]
        public bool ShowPlayerDied;

        [Header("Debug")]

        [Label("Отображение в логах")]
        [Tooltip("Отображает логи модификации в консоли или в файле.")]
        [DefaultValue(false)]
        public bool ShowLogger;

        [Label("Отображение логов в чате")]
        [Tooltip("Отображает логи модификации в чате мира.")]
        [DefaultValue(false)]
        public bool ShowLoggerChat;

        public static Config Instance => ModContent.GetInstance<Config>();
    }
}
