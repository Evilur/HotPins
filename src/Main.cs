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
        private static ConfigEntry<string> _dragon_egg_name;
        private static ConfigEntry<string> _cloudberry_bush_name;

        /** Execute once */
        private void Awake() {
            /* Patch all the patches */
            Harmony harmony = new Harmony(ModInfo.GUID);
            harmony.PatchAll();

            /* Bind config fields */
            const string hint_for_empty_value =
                "\nLeave empty to not mark it on the map";
            const string hint_for_pickable = "\nNOTE: This item is renewable";

            /* Dungeon names */
            _burial_chamber_name = Config.Bind(
                "Dungeons",
                "Burial Chamber",
                "Chamber",
                "Burial Chambers' names on the map" + hint_for_empty_value
            );
            _troll_cave_name = Config.Bind(
                "Dungeons",
                "Troll Cave",
                "Troll",
                "Troll Caves' names on the map" + hint_for_empty_value
            );
            _sunken_crypt_name = Config.Bind(
                "Dungeons",
                "Sunken Crypt",
                "Crypt",
                "Sunken Crypts' names on the map" + hint_for_empty_value
            );
            _frozen_cave_name = Config.Bind(
                "Dungeons",
                "Frozen Cave",
                "Cave",
                "Frozen Caves' names on the map" + hint_for_empty_value
            );
            _fuling_village_name = Config.Bind(
                "Dungeons",
                "Fuling Village",
                "Village",
                "Fuling Villages' names on the map" + hint_for_empty_value
            );

            /* Mineable names */
            _viking_graveyard_name = Config.Bind(
                "Minable",
                "Viking Graveyard",
                "Graveyard",
                "Viking Graveyards' names on the map" + hint_for_empty_value
            );
            _oak_name = Config.Bind(
                "Minable",
                "Oak",
                "Oak",
                "Oaks' names on the map" + hint_for_empty_value
            );
            _copper_ore_name = Config.Bind(
                "Minable",
                "Copper Ore",
                "Copper",
                "Copper Ores' names on the map" + hint_for_empty_value
            );
            _tin_ore_name = Config.Bind(
                "Minable",
                "Tin Ore",
                "Tin",
                "Tin Ores' names on the map" + hint_for_empty_value
            );
            _guck_name = Config.Bind(
                "Minable",
                "Guck",
                "Guck",
                "Gucks' names on the map" + hint_for_empty_value
            );
            _obsidian_name = Config.Bind(
                "Minable",
                "Obsidian",
                "Obsidian",
                "Obsidians' names on the map" + hint_for_empty_value
            );
            _silver_ore_name = Config.Bind(
                "Minable",
                "Silver Ore",
                "Silver",
                "Silver Ores' names on the map" + hint_for_empty_value
            );
            _tar_pit_name = Config.Bind(
                "Minable",
                "Tar Pit",
                "Tar",
                "Tar Pits' names on the map" + hint_for_empty_value
            );
            _leviathan_name = Config.Bind(
                "Minable",
                "Leviathan",
                "Leviathan",
                "Leviathans' names on the map" + hint_for_empty_value
            );

            /* Pickable names */
            _dandelion_name = Config.Bind(
                "Pickable",
                "Dandelion",
                "Dandelion",
                "Dandelions' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _raspberry_bush_name = Config.Bind(
                "Pickable",
                "Raspberry Bush",
                "Raspberry",
                "Raspberry Bushes' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _mushroom_name = Config.Bind(
                "Pickable",
                "Mushroom",
                "Mushroom",
                "Mushrooms' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _thistle_name = Config.Bind(
                "Pickable",
                "Thistle",
                "Thistle",
                "Thistles' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _blackberry_bush_name = Config.Bind(
                "Pickable",
                "Blackberry Bush",
                "Blackberry",
                "Blackberry Bushes' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _carrot_seeds_name = Config.Bind(
                "Pickable",
                "Carrot Seeds",
                "Carrot Seeds",
                "Carrot Seeds' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _turnip_seeds_name = Config.Bind(
                "Pickable",
                "Turnip Seeds",
                "Turnip Seeds",
                "Turnip Seeds' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _dragon_egg_name = Config.Bind(
                "Pickable",
                "Dragon Egg",
                "Dragon Egg",
                "Dragon Eggs' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
            _cloudberry_bush_name = Config.Bind(
                "Pickable",
                "Cloudberry Bush",
                "Cloudberry",
                "Cloudberry Bushes' names on the map" +
                    hint_for_empty_value + hint_for_pickable
            );
        }
    }
}
