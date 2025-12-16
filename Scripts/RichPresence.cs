using Terraria;
using Terraria.ModLoader;
using DiscordRPC;
using Microsoft.Xna.Framework;
using Discraria.Scripts.Tools;

namespace Discraria.Scripts
{
    public class RichPresence : ModSystem
    {
        private string lastState = "";
        private bool wasDead = false;

        public override void OnWorldUnload()
        {
            Discraria.SetMenuStatus();
            lastState = Tools.Utils.GetTranslation("RichPresence", "Menu");
            wasDead = false;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            UpdatePresence();
        }

        private void UpdatePresence()
        {
            if (Main.gameMenu)
            {
                if (lastState != Tools.Utils.GetTranslation("RichPresence", "Menu"))
                {
                    Discraria.SetMenuStatus();
                    lastState = Tools.Utils.GetTranslation("RichPresence", "Menu");
                    wasDead = false;
                }
                return;
            }

            Player player = Main.LocalPlayer;
            if (player == null)
                return;

            if (player.dead)
            {
                if (!wasDead)
                {
                    wasDead = true;

                    string biome = GetWorldUtils.GetPlayerBiome(player);
                    string deathState =
                        $"{Tools.Utils.GetTranslation("RichPresence", "Died")} " +
                        $"{Tools.Utils.GetTranslation("RichPresence", "in")} " +
                        $"{Tools.Utils.GetTranslation("RichPresence", biome)}...";

                    Discraria.client?.SetPresence(new DiscordRPC.RichPresence()
                    {
                        Details = GetWorldUtils.GetWorldType(),
                        State = deathState,
                        Assets = new Assets()
                        {
                            LargeImageKey = biome,
                            SmallImageKey = "death"
                        }
                    });

                    lastState = deathState;
                }

                return;
            }
            else if (wasDead)
            {
                wasDead = false;
                lastState = "";
            }

            NPC boss = GetWorldUtils.GetActiveBoss();
            if (boss != null)
            {
                string biome = GetWorldUtils.GetPlayerBiome(player);
                string bossName = boss.FullName;

                string bossState =
                    $"{Tools.Utils.GetTranslation("RichPresence", "Fighting")} " +
                    $"{bossName} {Tools.Utils.GetTranslation("RichPresence", "in")} " +
                    $"{Tools.Utils.GetTranslation("RichPresence", biome)}";

                if (bossState != lastState)
                {
                    lastState = bossState;

                    Discraria.client?.SetPresence(new DiscordRPC.RichPresence()
                    {
                        Details = GetWorldUtils.GetWorldType(),
                        State = bossState,
                        Assets = new Assets()
                        {
                            LargeImageKey = biome,
                            SmallImageKey = GetWorldUtils.GetBossIcon(bossName),
                            SmallImageText = bossName
                        }
                    });
                }

                return;
            }

            string biome2 = GetWorldUtils.GetPlayerBiome(player);
            string newState = $"{Tools.Utils.GetTranslation("RichPresence", "Explores")} {Tools.Utils.GetTranslation("RichPresence", biome2)}";

            if (newState == lastState)
                return;

            lastState = newState;

            Discraria.client?.SetPresence(new DiscordRPC.RichPresence()
            {
                Details = GetWorldUtils.GetWorldType(),
                State = newState,
                Assets = new Assets()
                {
                    LargeImageKey = biome2,
                    SmallImageKey = GetWorldUtils.GetWorldIcon(),
                    SmallImageText = 
                    $"{Tools.Utils.GetTranslation("RichPresence", "HP")}: {player.statLifeMax2} | " +
                    $"{Tools.Utils.GetTranslation("RichPresence", "Mana")}: {player.statManaMax2} | " +
                    $"{Tools.Utils.GetTranslation("RichPresence", "Defense")}: {player.statDefense}"
                }
            });
            var presence = Discraria.client?.CurrentPresence;
            ModContent.GetInstance<Discraria>().Logger.Info($"[DiscordRPC] Presence set: {presence.Details}, {presence.State}");
        }
    }
}
