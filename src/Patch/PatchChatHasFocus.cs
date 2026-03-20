using HarmonyLib;
using HotPins.Core;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(Chat), "HasFocus")]
    internal static class PatchChatHasFocus {
        private static bool Prefix(ref bool __result) {
            /* If the filter input is not enabled, run the main method */
            if (!Filter.IsEnabled()) return true;

            /* Otherwise */
            __result = true;
            return false;
        }
    }
}
