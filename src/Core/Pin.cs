using UnityEngine;
using HarmonyLib;

namespace HotPins.Core {
    internal static class Pin {
        public static void Add(Vector3 pos,
                               Minimap.PinType type,
                               string name) {
            /* Check the minimap for null */
            if (Minimap.instance == null) return;

            /* If there is already such a pin */
            if ((bool)AccessTools.Method(typeof(Minimap), "HavePinInRange")
                                 .Invoke(Minimap.instance,
                                         new object[] { pos, 2.5f })) return;

            /* Add the pin to the map */
            Minimap.instance.AddPin(pos, type, name, true, false);
        }
    }
}
