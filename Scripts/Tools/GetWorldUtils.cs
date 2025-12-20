using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Newtonsoft.Json;

namespace Discraria.Scripts.Tools
{
    public static class GetWorldUtils
    {
        static Dictionary<string, string> _seedCache;
        static Dictionary<string, string> _bossCache;

        public static string GetWorldType()
        {
            string type = Main.netMode switch
            {
                0 => "singleplayer",
                1 => "multiplayer",
                2 => "server",
                _ => "unknown"
            };

            string difficulty = Main.GameMode switch
            {
                0 => "classic",
                1 => "expert",
                2 => "master",
                3 => "journey",
                _ => "unknown"
            };

            return $"{Utils.GetTranslation("TypeWorld", type)} ({Utils.GetTranslation("Difficulties", difficulty)})";
        }

        public static string GetWorldIcon()
        {
            bool hardmode = Main.hardMode;
            bool corruption = WorldGen.crimson == false;
            bool crimson = WorldGen.crimson == true;

            if (_seedCache == null)
            {
                try
                {
                    var mod = ModContent.GetInstance<Discraria>();
                    byte[] data = mod.GetFileBytes("Assets/seeds.json");
                    string json = Encoding.UTF8.GetString(data);

                    _seedCache = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<Dictionary<string, string>>(json);
                }
                catch
                {
                    _seedCache = new Dictionary<string, string>();
                }
            }

     
            string seed = Main.ActiveWorldFileData.SeedText;


            if (_seedCache.TryGetValue(seed, out string specialIcon))
            {
                string worldType = corruption ? "corruption" : "crimson";
                return hardmode ? $"{specialIcon}_{worldType}_hard" : $"{specialIcon}_{worldType}_pre";
            }


            if (crimson || corruption)
                return hardmode ? "crimson_corruption_hard" : "crimson_corruption_pre";

            if (corruption)
                return hardmode ? "corruption_hard" : "corruption_pre";

            if (crimson)
                return hardmode ? "crimson_hard" : "crimson_pre";

            return "terraria";
        }


        public static bool IsBoss(NPC npc)
        {
            return npc.boss || npc.type switch
            {
                NPCID.KingSlime => true,
                NPCID.EyeofCthulhu => true,
                NPCID.EaterofWorldsHead => true,
                NPCID.EaterofWorldsBody => true,
                NPCID.EaterofWorldsTail => true,
                NPCID.BrainofCthulhu => true,
                NPCID.Creeper => true,
                NPCID.QueenBee => true,
                NPCID.SkeletronHead => true,
                NPCID.SkeletronHand => true,
                NPCID.WallofFlesh => true,
                NPCID.WallofFleshEye => true,
                NPCID.QueenSlimeBoss => true,
                NPCID.Retinazer => true,
                NPCID.Spazmatism => true,
                NPCID.TheDestroyer => true,
                NPCID.TheDestroyerBody => true,
                NPCID.TheDestroyerTail => true,
                NPCID.SkeletronPrime => true,
                NPCID.PrimeCannon => true,
                NPCID.PrimeLaser => true,
                NPCID.PrimeSaw => true,
                NPCID.PrimeVice => true,
                NPCID.Plantera => true,
                NPCID.PlanterasTentacle => true,
                NPCID.Golem => true,
                NPCID.GolemHead => true,
                NPCID.GolemFistLeft => true,
                NPCID.GolemFistRight => true,
                NPCID.DukeFishron => true,
                NPCID.HallowBoss => true,
                NPCID.CultistBoss => true,
                NPCID.MoonLordHead => true,
                NPCID.Deerclops => true,
                NPCID.Everscream => true,
                NPCID.MartianSaucer => true,
                NPCID.IceQueen => true,
                NPCID.PirateShip => true,
                NPCID.PirateShipCannon => true,
                NPCID.SantaNK1 => true,
                NPCID.Pumpking => true,
                NPCID.MourningWood => true,
                NPCID.DD2DarkMageT1 => true,
                NPCID.DD2DarkMageT3 => true,
                NPCID.DD2Betsy => true,
                NPCID.DD2OgreT2 => true,
                NPCID.DD2OgreT3 => true,
                NPCID.DD2LanePortal => true,
                _ => false
            };
        }

        public static NPC GetActiveBoss()
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && IsBoss(npc))
                    return npc;
            }
            return null;
        }

        public static string GetBossIcon(string bossName)
        {
            if (_bossCache == null)
            {
                try
                {
                    var mod = ModContent.GetInstance<Discraria>();

                    byte[] data = mod.GetFileBytes("Assets/bosses.json");

                    string json = Encoding.UTF8.GetString(data);

                    _bossCache = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                catch
                {
                    _bossCache = new Dictionary<string, string>();
                }
            }

            if (_bossCache.TryGetValue(bossName, out string icon))
                return icon;

            return "boss_unknown";
        }

        public static string GetPlayerBiome(Player player)
        {
            if (player.ZoneForest) return "forest";
            if (player.ZoneDesert) return "desert";
            if (player.ZoneJungle) return "jungle";
            if (player.ZoneSnow) return "snow";
            if (player.ZoneCorrupt) return "corruption";
            if (player.ZoneCrimson) return "crimson";
            if (player.ZoneHallow) return "hallow";
            if (player.ZoneUnderworldHeight) return "underworld";
            if (player.ZoneBeach) return "beach";
            if (player.ZoneDungeon) return "dungeon";
            if (player.ZoneNormalSpace) return "space";
            if (player.ZoneGlowshroom) return "glowing_mushroom";
            if (player.ZoneShimmer) return "shimmer";
            if (player.ZoneGranite) return "granite_cave";
            if (player.ZoneMarble) return "marble_cave";
            if (player.ZoneGraveyard) return "graveyard";
            if (player.ZoneUndergroundDesert) return "underground_desert";
            if (player.ZoneHive) return "bee_hive";
            if (player.ZoneMeteor) return "meteor";
            if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight) return "underground";

            return "unknown";
        }
    }
}
