using HarmonyLib;
using UnityEngine;
using System.Linq;
using TMPro;
using HotPins.Core;
using System.Collections.Generic;

namespace HotPins.Patch {
    [HarmonyPatch(typeof(Minimap), "Awake")]
    internal static class PatchMinimapAwake {
        private static void Postfix(ref List<Minimap.PinData> ___m_pins) {
            /* Get the large minimap object */
            GameObject largeRoot = Minimap.instance.m_largeRoot;

            /* Create the filter object */
            GameObject filter = new GameObject("Filter",
                                               typeof(RectTransform),
                                               typeof(TextMeshProUGUI));
            filter.transform.SetParent(largeRoot.transform);

            /* Set the position and size */
            RectTransform rect = filter.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.zero;
            rect.pivot = Vector2.zero;
            rect.anchoredPosition = new Vector2(12.5f, 15);
            rect.sizeDelta = new Vector2(0, 24);

            /* Setting the text component */
            TextMeshProUGUI text = filter.GetComponent<TextMeshProUGUI>();
            text.fontSize = 24;
            text.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>()
                .FirstOrDefault(f => f.name == "Valheim-Norse");
            text.fontStyle = FontStyles.Bold;
            text.textWrappingMode = TextWrappingModes.NoWrap;

            /* Set the text component for the Filter class */
            Filter.SetText(text);
        }
    }
}
