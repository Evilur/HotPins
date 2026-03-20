using HarmonyLib;
using System.Collections.Generic;
using HotPins.Core;
using System.Reflection;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(Minimap), "UpdatePins")]
    internal static class PatchMinimapUpdatePins {
        private static MethodInfo _destroyPinMarker =
            AccessTools.Method(typeof(Minimap), "DestroyPinMarker");

        private static void Postfix(ref List<Minimap.PinData> ___m_pins) {
            /* If we has not the filter, skip this patch */
            if (!Filter.HasFilter()) return;

            /* Destroy all filtered pins */
            foreach (Minimap.PinData pin in ___m_pins)
                if (pin.m_uiElement != null && Filter.IsFiltered(pin))
                    _destroyPinMarker.Invoke(Minimap.instance,
                                             new object[] { pin });
        }
    }
}
