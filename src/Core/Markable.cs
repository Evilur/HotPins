using UnityEngine;
using System.Reflection;
using HarmonyLib;

namespace HotPins.Core {
    internal class Markable : MonoBehaviour {
        [Header("Main fields")]
        [SerializeField] private string _pinName;
        [SerializeField] private Minimap.PinType _pinType;
        [SerializeField] private float _maxSqrtDistance;

        [Header("Reflections")]
        private static MethodInfo _havePinInRange =
            AccessTools.Method(typeof(Minimap), "HavePinInRange");

        public void SetPinName(string pinName) =>
            _pinName = pinName;

        public void SetPinType(Minimap.PinType pinType) =>
            _pinType = pinType;

        public void SetMaxSqrtDistance(float maxSqrtDistance) =>
            _maxSqrtDistance = maxSqrtDistance;

        public bool IsPlayerNear() {
            /* If the player is not initialized yet */
            if (Player.m_localPlayer == null) return false;

            /* Get the delta between the local player and this object */
            Vector3 delta = Player.m_localPlayer.transform.position -
                            transform.position;

            /* Compare the sqrt magnitude and max sqrt distance
             * (Ignore the Y axis) */
            return (new Vector2(delta.x, delta.z)).sqrMagnitude <=
                   _maxSqrtDistance;
        }

        public bool HasSuchPin() =>
            (bool)_havePinInRange.Invoke(
                    Minimap.instance,
                    new object[] { transform.position, 5f }
            );

        public void MarkPin() =>
            Minimap.instance.AddPin(transform.position,
                                    _pinType,
                                    _pinName,
                                    true,
                                    false);
    }
}
