using UnityEngine.InputSystem;
using TMPro;
using HarmonyLib;
using System.Reflection;

namespace HotPins.Core {
    internal static class Filter {
        private static bool _isEnabled = false;

        private static TextMeshProUGUI _text = null;

        private static MethodInfo _updatePins =
            AccessTools.Method(typeof(Minimap), "UpdatePins");

        private static InputAction _disable = new InputAction(
                    type: InputActionType.Button,
                    binding: "<Keyboard>/escape"
                );

        static Filter() => _disable.started += _ => Disable();

        private static void ResetUserInput() {
            _text.text = "";
        }

        private static void GetUserInput(char c) {
            /* Check the text for null */
            if (_text == null) return;

            /* If there is an Enter char, exit */
            if (c == '\r' || c == '\n') {
                Keyboard.current.onTextInput -= Filter.GetUserInput;
                _isEnabled = false;
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

        public static void Enable() {
            /* If we are already filtering */
            if (_isEnabled) return;

            /* If the minimap is not opened */
            if (!Minimap.IsOpen()) return;

            /* If the user is wirting the pin name */
            if (Minimap.instance.m_nameInput != null &&
                Minimap.instance.m_nameInput.gameObject.activeSelf) return;

            /* Reset the user input */
            ResetUserInput();

            /* Add the event handler */
            Keyboard.current.onTextInput += Filter.GetUserInput;

            /* Update the state */
            _isEnabled = true;

            /* Enable the disabling hotkey */
            _disable.Enable();
        }

        public static void Disable() {
            /* Remove the event handler */
            Keyboard.current.onTextInput -= GetUserInput;

            /* Update the state */
            _isEnabled = false;

            /* Disable the disabling hotkey */
            _disable.Disable();
        }

        public static void SetText(TextMeshProUGUI text) => _text = text;

        public static bool IsEnabled() => _isEnabled;

        public static bool HasFilter() => _text.text != null;

        public static bool IsFiltered(Minimap.PinData pin) {
            if (pin.m_type != Minimap.PinType.Icon0 &&
                pin.m_type != Minimap.PinType.Icon1 &&
                pin.m_type != Minimap.PinType.Icon2 &&
                pin.m_type != Minimap.PinType.Icon3 &&
                pin.m_type != Minimap.PinType.Icon4) return false;
            else return !pin.m_name.ToLower().Contains(_text.text.ToLower());
        }
    }
}
