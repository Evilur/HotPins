using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace HotPins.Core {
    internal static class Pin {
        private static MethodInfo _havePinInRange =
            AccessTools.Method(typeof(Minimap), "HavePinInRange");

        public static void Add(Vector3 pos,
                               Minimap.PinType type,
                               string name) {
            /* Check the minimap for null */
            if (Minimap.instance == null) return;

            /* If there is already such a pin */
            if ((bool)_havePinInRange.Invoke(Minimap.instance,
                                             new object[] { pos, 2.5f }))
                return;

            /* Add the pin to the map */
            Minimap.instance.AddPin(pos, type, name, true, false);
        }
    }
}
