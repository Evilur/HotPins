using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using HotPins.Core;

namespace HotPins {
    [BepInPlugin(ModInfo.GUID, ModInfo.MODNAME, ModInfo.VERSION)]
    internal class Main : BaseUnityPlugin {
        /* General settings */
        private static ConfigEntry<KeyCode> _main_key;

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

        /* Dictionary to store 'obj.name -> pin name' pairs */
        private static Dictionary<string, ConfigEntry<string>> _names;

        /* Dictionary to store 'pin name -> pin type' pairs */
        private static Dictionary<ConfigEntry<string>,
                                  Minimap.PinType> _types;

        /* Dictionary to store 'pin name -> specific distance' pairs */
        private static Dictionary<ConfigEntry<string>, uint> _sdistance;

        private void Awake() {
            /* Patch all the patches */
            Harmony harmony = new Harmony(ModInfo.GUID);
            harmony.PatchAll();

            /* Bind config fields */
            _main_key = Config.Bind(
                "General",
                "Key",
                KeyCode.G,
                "A key to auto mark pins on the map"
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
            _names = new Dictionary<string, ConfigEntry<string>> {
                { "Crypt2(Clone)", _burial_chamber_name },
                { "Crypt3(Clone)", _burial_chamber_name },
                { "Crypt4(Clone)", _burial_chamber_name },
                { "TrollCave02(Clone)", _troll_cave_name },
                { "SunkenCrypt4(Clone)", _sunken_crypt_name },
                { "MountainCave02(Clone)", _frozen_cave_name },
                { "GoblinCamp2(Clone)", _fuling_village_name },
                { "ShipSetting01(Clone)", _viking_graveyard_name },
                { "Oak1(Clone)", _oak_name },
                { "rock4_copper(Clone)", _copper_ore_name },
                { "MineRock_Tin(Clone)", _tin_ore_name },
                { "GuckSack(Clone)", _guck_name },
                { "MineRock_Obsidian(Clone)", _obsidian_name },
                { "silvervein(Clone)", _silver_ore_name },
                { "TarPit1(Clone)", _tar_pit_name },
                { "TarPit2(Clone)", _tar_pit_name },
                { "TarPit3(Clone)", _tar_pit_name },
                { "Leviathan(Clone)", _leviathan_name },
                { "Pickable_Dandelion(Clone)", _dandelion_name },
                { "RaspberryBush(Clone)", _raspberry_bush_name },
                { "Pickable_Mushroom(Clone)", _mushroom_name },
                { "Pickable_Thistle(Clone)", _thistle_name },
                { "BlueberryBush(Clone)", _blueberry_bush_name },
                { "Pickable_SeedCarrot(Clone)", _carrot_seeds_name },
                { "Pickable_SeedTurnip(Clone)", _turnip_seeds_name },
                { "Pickable_DragonEgg(Clone)", _dragon_egg_name },
                { "CloudberryBush(Clone)", _cloudberry_bush_name }
            };
            _types = new Dictionary<ConfigEntry<string>, Minimap.PinType> {
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
            _sdistance = new Dictionary<ConfigEntry<string>, uint> {
                { _burial_chamber_name, 15 * 15 },
                { _troll_cave_name, 15 * 15 },
                { _sunken_crypt_name, 15 * 15 },
                { _frozen_cave_name, 20 * 20 },
                { _fuling_village_name, 50 * 50 },
                { _viking_graveyard_name, 20 * 20 },
                { _oak_name, 15 * 15 },
                { _copper_ore_name, 20 * 20 },
                { _tin_ore_name, 10 * 10 },
                { _guck_name, 15 * 15 },
                { _obsidian_name, 10 * 10 },
                { _silver_ore_name, 15 * 15 },
                { _tar_pit_name, 30 * 30 },
                { _leviathan_name, 70 * 70 },
                { _dandelion_name, 5 * 5 },
                { _raspberry_bush_name, 5 * 5 },
                { _mushroom_name, 5 * 5 },
                { _thistle_name, 5 * 5 },
                { _blueberry_bush_name, 5 * 5 },
                { _carrot_seeds_name, 5 * 5 },
                { _turnip_seeds_name, 5 * 5 },
                { _dragon_egg_name, 10 * 10 },
                { _cloudberry_bush_name, 5 * 5 },
            };
        }

        private void Update() {
            /* If the main key is not pressed, exit the method */
            if (!Input.GetKeyDown(_main_key.Value)) return;

            void TryAddPin(GameObject obj) {
                /* Try to get the name from the dictionary */
                ConfigEntry<string> name;
                if (!_names.TryGetValue(obj.name, out name)) return;

                /* If there is such a name in the dictionary,
                 * Check for distance away from the player */
                {
                    Vector3 player_pos =
                        Player.m_localPlayer.transform.position;
                    Vector3 obj_pos = obj.transform.position;
                    Vector3 delta = player_pos - obj_pos;
                    if ((delta.x * delta.x + delta.z * delta.z) >
                        _sdistance.GetValueSafe(name)) return;
                }

                /* If all is OK, add the pin */
                Pin.Add(obj.transform.position,
                        _types.GetValueSafe(name),
                        name.Value);
            }

            /* Find all neccecary Location objects */
            foreach (Pickable obj in FindObjectsByType<Pickable>(
                        FindObjectsInactive.Exclude,
                        FindObjectsSortMode.None
                     )) TryAddPin(obj.gameObject);
            foreach (Location obj in FindObjectsByType<Location>(
                        FindObjectsInactive.Exclude,
                        FindObjectsSortMode.None
                     )) TryAddPin(obj.gameObject);
            foreach (Destructible obj in FindObjectsByType<Destructible>(
                        FindObjectsInactive.Exclude,
                        FindObjectsSortMode.None
                     )) TryAddPin(obj.gameObject);
            foreach (TreeBase obj in FindObjectsByType<TreeBase>(
                        FindObjectsInactive.Exclude,
                        FindObjectsSortMode.None
                     )) TryAddPin(obj.gameObject);
        }
    }
}
