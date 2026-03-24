using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using HotPins.Core;

namespace HotPins {
    [BepInPlugin(ModInfo.GUID, ModInfo.MODNAME, ModInfo.VERSION)]
    internal class Main : BaseUnityPlugin {
        /* General settings */
        private static ConfigEntry<string> _hotkey;

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
        private static ConfigEntry<string> _blueberry_bush_name;
        private static ConfigEntry<string> _carrot_seeds_name;
        private static ConfigEntry<string> _turnip_seeds_name;
        private static ConfigEntry<string> _dragon_egg_name;
        private static ConfigEntry<string> _cloudberry_bush_name;

        /* Dictionary to store 'prefab.name -> pin name' pairs */
        public static Dictionary<string, ConfigEntry<string>> prefabNames;

        /* Dictionary to store 'locationProxy -> pin name' pairs */
        public static Dictionary<string, ConfigEntry<string>> locationNames;

        /* Dictionary to store 'pin name -> pin type' pairs */
        public static Dictionary<ConfigEntry<string>, Minimap.PinType> types;

        /* Dictionary to store 'pin name -> square distance' pairs */
        public static Dictionary<ConfigEntry<string>, uint> sqrtDistance;

        /* Actions */
        public static InputAction mainAction;
        public static InputAction filterAction;

        private void Awake() {
            /* Patch all the patches */
            Harmony harmony = new Harmony(ModInfo.GUID);
            harmony.PatchAll();

            /* Bind config fields */
            _hotkey = Config.Bind(
                "General",
                "Hotkey",
                "<Keyboard>/g",
                "A hotkey to auto mark pins on the map"
            );

            /* Dungeon names */
            const string hint_for_empty_value =
                "\nLeave empty to not mark it on the map";
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
            const string hint_for_pickable =
                "\nTIP: This item is renewable";
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
            _blueberry_bush_name = Config.Bind(
                "Pickable",
                "Blueberry Bush",
                "Blueberry",
                "Blueberry Bushes' names on the map" +
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

            /* Fill dictionaries */
            prefabNames = new Dictionary<string, ConfigEntry<string>> {
                { "Oak1", _oak_name },
                { "rock4_copper", _copper_ore_name },
                { "MineRock_Tin", _tin_ore_name },
                { "GuckSack", _guck_name },
                { "MineRock_Obsidian", _obsidian_name },
                { "silvervein", _silver_ore_name },
                { "Leviathan", _leviathan_name },
                { "Pickable_Dandelion", _dandelion_name },
                { "RaspberryBush", _raspberry_bush_name },
                { "Pickable_Mushroom", _mushroom_name },
                { "Pickable_Thistle", _thistle_name },
                { "BlueberryBush", _blueberry_bush_name },
                { "Pickable_SeedCarrot", _carrot_seeds_name },
                { "Pickable_SeedTurnip", _turnip_seeds_name },
                { "Pickable_DragonEgg", _dragon_egg_name },
                { "CloudberryBush", _cloudberry_bush_name }
            };
            locationNames = new Dictionary<string, ConfigEntry<string>> {
                { "Crypt2", _burial_chamber_name },
                { "Crypt3", _burial_chamber_name },
                { "Crypt4", _burial_chamber_name },
                { "TrollCave02", _troll_cave_name },
                { "SunkenCrypt4", _sunken_crypt_name },
                { "MountainCave02", _frozen_cave_name },
                { "GoblinCamp2", _fuling_village_name },
                { "ShipSetting01", _viking_graveyard_name },
                { "TarPit1", _tar_pit_name },
                { "TarPit2", _tar_pit_name },
                { "TarPit3", _tar_pit_name },
            };
            types = new Dictionary<ConfigEntry<string>, Minimap.PinType> {
                { _burial_chamber_name, Minimap.PinType.Icon2 },
                { _troll_cave_name, Minimap.PinType.Icon2 },
                { _sunken_crypt_name, Minimap.PinType.Icon2 },
                { _frozen_cave_name, Minimap.PinType.Icon2 },
                { _fuling_village_name, Minimap.PinType.Icon2 },
                { _viking_graveyard_name, Minimap.PinType.Icon3 },
                { _oak_name, Minimap.PinType.Icon3 },
                { _copper_ore_name, Minimap.PinType.Icon3 },
                { _tin_ore_name, Minimap.PinType.Icon3 },
                { _guck_name, Minimap.PinType.Icon3 },
                { _obsidian_name, Minimap.PinType.Icon3 },
                { _silver_ore_name, Minimap.PinType.Icon3 },
                { _tar_pit_name, Minimap.PinType.Icon3 },
                { _leviathan_name, Minimap.PinType.Icon3 },
                { _dandelion_name, Minimap.PinType.Icon3 },
                { _raspberry_bush_name, Minimap.PinType.Icon3 },
                { _mushroom_name, Minimap.PinType.Icon3 },
                { _thistle_name, Minimap.PinType.Icon3 },
                { _blueberry_bush_name, Minimap.PinType.Icon3 },
                { _carrot_seeds_name, Minimap.PinType.Icon3 },
                { _turnip_seeds_name, Minimap.PinType.Icon3 },
                { _dragon_egg_name, Minimap.PinType.Icon3 },
                { _cloudberry_bush_name, Minimap.PinType.Icon3 },
            };
            sqrtDistance = new Dictionary<ConfigEntry<string>, uint> {
                { _burial_chamber_name, 20 * 20 },
                { _troll_cave_name, 20 * 20 },
                { _sunken_crypt_name, 20 * 20 },
                { _frozen_cave_name, 25 * 25 },
                { _fuling_village_name, 150 * 150 },
                { _viking_graveyard_name, 20 * 20 },
                { _oak_name, 20 * 20 },
                { _copper_ore_name, 20 * 20 },
                { _tin_ore_name, 10 * 10 },
                { _guck_name, 20 * 20 },
                { _obsidian_name, 10 * 10 },
                { _silver_ore_name, 15 * 15 },
                { _tar_pit_name, 50 * 50 },
                { _leviathan_name, 150 * 150 },
                { _dandelion_name, 10 * 10 },
                { _raspberry_bush_name, 10 * 10 },
                { _mushroom_name, 10 * 10 },
                { _thistle_name, 10 * 10 },
                { _blueberry_bush_name, 10 * 10 },
                { _carrot_seeds_name, 10 * 10 },
                { _turnip_seeds_name, 10 * 10 },
                { _dragon_egg_name, 15 * 15 },
                { _cloudberry_bush_name, 10 * 10 },
            };

            /* Bind the main action to the hotkey */
            mainAction = new InputAction(
                type: InputActionType.Button,
                binding: _hotkey.Value
            );
            mainAction.started += _ => {
                /* Find all markable objects */
                foreach (Markable markable in FindObjectsByType<Markable>(
                            FindObjectsInactive.Exclude,
                            FindObjectsSortMode.None
                        )) {
                    /* Check the minimap for null */
                    if (Minimap.instance == null) return;

                    /* If the player is near the object
                     * And if there is no such a ping yet */
                    if (markable.IsPlayerNear() && !markable.HasSuchPin())
                        markable.MarkPin();
                }
            };
            mainAction.Enable();

            /* Bind the filter action to the hotkey */
            filterAction = new InputAction(
                type: InputActionType.Button,
                binding: "<Keyboard>/f"
            );
            filterAction.canceled += _ => Filter.Enable();
        }
    }
}
