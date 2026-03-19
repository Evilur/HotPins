using UnityEngine.InputSystem;
using TMPro;
using HarmonyLib;
using UnityEngine;
using System.Reflection;

namespace HotPins.Core {
    internal static class Filter {
        public static bool isFiltering = false;

        private static TextMeshProUGUI _text = null;

        private static MethodInfo _updatePins =
            AccessTools.Method(typeof(Minimap), "UpdatePins");

        public static void ResetUserInput() {
            _text.text = "";
        }

        public static void GetUserInput(char c) {
            /* Check the text for null */
            if (_text == null) return;

            /* If there is an Enter char, exit */
            if (c == '\r' || c == '\n') {
                Keyboard.current.onTextInput -= Filter.GetUserInput;
                isFiltering = false;
                return;
            }

            /* If there is a backspace */
            if (c == '\b') {
                if (_text.text.Length > 0)
                    _text.text =
                        _text.text.Substring(0, _text.text.Length - 1);
            /* If there is a default char, appent it to the string */
            } else {
                _text.text += c;
            }

            /* Force update the pins on the map */
            _updatePins.Invoke(Minimap.instance, null);
        }

        public static void SetText(TextMeshProUGUI text) => _text = text;

        public static bool IsFiltered(Minimap.PinData pin) {
            if (_text.text == null) return false;
            return !pin.m_name.ToLower().Contains(_text.text.ToLower());
        }
    }
}
