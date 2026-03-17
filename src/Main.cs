using BepInEx;
using HarmonyLib;
using BepInEx.Configuration;

namespace HotPins {
    [BepInPlugin(ModInfo.GUID, ModInfo.MODNAME, ModInfo.VERSION)]
    internal class Main : BaseUnityPlugin {
        /* Dungeon names */
        private static ConfigEntry<string> _burial_chamber_name;
        private static ConfigEntry<string> _troll_cave_name;
        private static ConfigEntry<string> _sunken_crypt_name;
        private static ConfigEntry<string> _frozen_cave_name;
        private static ConfigEntry<string> _fuling_village_name;

        /* Mineable names */
        private static ConfigEntry<string> _viking_graveyard_name;
        private static ConfigEntry<string> _oak_name;
        private static ConfigEntry<string> _copper_ore_name;
        private static ConfigEntry<string> _tin_ore_name;
        private static ConfigEntry<string> _guck_name;
        private static ConfigEntry<string> _obsidian_name;
        private static ConfigEntry<string> _silver_ore_name;
        private static ConfigEntry<string> _tar_pit_name;
        private static ConfigEntry<string> _leviathan_name;

        /* Pickable names */
        private static ConfigEntry<string> _dandelion_name;
        private static ConfigEntry<string> _raspberry_bush_name;
        private static ConfigEntry<string> _mushroom_name;
        private static ConfigEntry<string> _thistle_name;
        private static ConfigEntry<string> _blackberry_bush_name;
        private static ConfigEntry<string> _carrot_seeds_name;
        private static ConfigEntry<string> _turnip_seeds_name;
        private static ConfigEntry<string> _onion_seed_name;
        private static ConfigEntry<string> _dragon_egg_name;
        private static ConfigEntry<string> _cloudberry_bush_name;

        /** Execute once */
        private void Awake() {
            /* Patch all the patches */
            Harmony harmony = new Harmony(ModInfo.GUID);
            harmony.PatchAll();

            /* Bind config fields */

        }
    }
}
