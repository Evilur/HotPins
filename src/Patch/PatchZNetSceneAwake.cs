using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using BepInEx.Configuration;
using HotPins.Core;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(ZNetScene), "Awake")]
    internal static class PatchZNetSceneAwake {
        static void Postfix(ZNetScene __instance) {
            /* Loop through all the object names */
            foreach(KeyValuePair<string, ConfigEntry<string>> pair in
                    Main.prefabNames) {
                /* Get the object name */
                string objName = pair.Key;

                /* Get the pin name */
                ConfigEntry<string> pinName = pair.Value;
                if (string.IsNullOrWhiteSpace(pinName.Value)) continue;

                /* Get the pin type */
                Minimap.PinType pinType = Main.types[pinName];

                /* Get the max sqrt distance */
                float maxSqrtDisatnce = Main.sqrtDistance[pinName];

                /* Try to get the prefab */
                GameObject prefab = __instance.GetPrefab(objName);
                if (prefab == null) continue;

                /* Add the Markable component to it */
                Markable markable = prefab.AddComponent<Markable>();

                /* Set the pin type, the pin name and the max sqrt distance */
                markable.SetPinName(pinName.Value);
                markable.SetPinType(pinType);
                markable.SetMaxSqrtDistance(maxSqrtDisatnce);
            }
        }
    }
}
