using HarmonyLib;
using UnityEngine;
using BepInEx.Configuration;
using HotPins.Core;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(ZoneSystem), "SpawnLocation")]
    internal static class PatchZoneSystemSpawnLocation {
        static void Postfix(ZoneSystem.ZoneLocation location, GameObject __result) {
            /* Check the location prefab name and the result for null */
            if (location == null ||
                string.IsNullOrWhiteSpace(location.m_prefabName) ||
                __result == null) return;

            /* Get the pin name */
            ConfigEntry<string> pinName =
                Main.locationNames.GetValueSafe(location.m_prefabName);
            if (pinName == null || string.IsNullOrWhiteSpace(pinName.Value))
                return;

            /* Get the pin type */
            Minimap.PinType pinType = Main.types[pinName];

            /* Get the max sqrt distance */
            float maxSqrtDisatnce = Main.sqrtDistance[pinName];

            /* Add the Markable component to the object */
            Markable markable = __result.AddComponent<Markable>();

            /* Set the pin type, the pin name and the max sqrt distance */
            markable.SetPinName(pinName.Value);
            markable.SetPinType(pinType);
            markable.SetMaxSqrtDistance(maxSqrtDisatnce);
        }
    }
}
