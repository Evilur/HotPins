using HarmonyLib;
using UnityEngine.InputSystem;
using HotPins.Core;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(Minimap), "SetMapMode")]
    internal static class PatchMinimapSetMapMode {
        private static void Prefix(Minimap.MapMode mode) {
            /* Disable the filter input */
            Keyboard.current.onTextInput -= Filter.GetUserInput;
            Filter.isFiltering = false;

            /* If there is a large map */
            if (mode == Minimap.MapMode.Large) Main.filterAction.Enable();
            /* If there is a small map */
            else Main.filterAction.Disable();
        }
    }
}
