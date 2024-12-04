using UnityEngine;

namespace Source.Code
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private bool _ignoreX;
        [SerializeField] private bool _ignoreY;

        private RectTransform _rectTransform;
        private Rect _lastSafeArea = new(0, 0, 0, 0);
        private Vector2Int _lastScreenSize = new(0, 0);
        private ScreenOrientation _lastOrientation = ScreenOrientation.AutoRotation;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_rectTransform == null)
            {
                Debug.LogError($"No found rectTransform on: <{gameObject.name}>");
                Destroy(gameObject);
            }

            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            Rect safeArea = Screen.safeArea;

            if
            (
                safeArea != _lastSafeArea
                || Screen.width != _lastScreenSize.x
                || Screen.height != _lastScreenSize.y
                || Screen.orientation != _lastOrientation
            )
            {
                _lastScreenSize.x = Screen.width;
                _lastScreenSize.y = Screen.height;
                _lastOrientation = Screen.orientation;

                ApplySafeArea(safeArea);
            }
        }

        private void ApplySafeArea(Rect rect)
        {
            _lastSafeArea = rect;

            if (_ignoreX)
            {
                rect.x = 0;
                rect.width = Screen.width;
            }

            if (_ignoreY)
            {
                rect.y = 0;
                rect.height = Screen.height;
            }

            if (Screen.width > 0 && Screen.height > 0)
            {
                Vector2 anchorMin = rect.position;
                Vector2 anchorMax = rect.position + rect.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;

                if (anchorMin is { x: >= 0, y: >= 0 } && anchorMax is { x: >= 0, y: >= 0 })
                {
                    _rectTransform.anchorMin = anchorMin;
                    _rectTransform.anchorMax = anchorMax;
                }
            }
        }
    }
}