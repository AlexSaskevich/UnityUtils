using UnityEditor;
using UnityEngine;

namespace UnityUtils.Editor
{
    public class GUITools
    {
        [MenuItem("GUITools/Anchors to corners")]
        private static void AnchorsToCorners()
        {
            foreach (Transform transform in Selection.transforms)
            {
                var rectTransform = transform as RectTransform;

                var parentTransform = Selection.activeTransform.parent as RectTransform;

                if (rectTransform == null || parentTransform == null)
                {
                    return;
                }

                var newAnchorsMin = new Vector2(
                    rectTransform.anchorMin.x + rectTransform.offsetMin.x / parentTransform.rect.width,
                    rectTransform.anchorMin.y + rectTransform.offsetMin.y / parentTransform.rect.height);
                var newAnchorsMax = new Vector2(
                    rectTransform.anchorMax.x + rectTransform.offsetMax.x / parentTransform.rect.width,
                    rectTransform.anchorMax.y + rectTransform.offsetMax.y / parentTransform.rect.height);

                rectTransform.anchorMin = newAnchorsMin;
                rectTransform.anchorMax = newAnchorsMax;
                rectTransform.offsetMin = rectTransform.offsetMax = new Vector2(0, 0);
                EditorUtility.SetDirty(transform.gameObject);
            }
        }

        [MenuItem("GUITools/Corners to anchors")]
        private static void CornersToAnchors()
        {
            foreach (Transform transform in Selection.transforms)
            {
                var rectTransform = transform as RectTransform;

                if (rectTransform == null)
                {
                    return;
                }

                rectTransform.offsetMin = rectTransform.offsetMax = new Vector2(0, 0);
                EditorUtility.SetDirty(transform.gameObject);
            }
        }
    }
}