using HarmonyLib;
using HotPins.Core;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(Minimap), "InTextInput")]
    internal static class PatchMinimapInTextInput {
        private static bool Prefix(ref bool __result) {
            /* If the filter input is not enabled, run the main method */
            if (!Filter.IsEnabled()) return true;

            /* Otherwise */
            __result = true;
            return false;
        }
    }
}
