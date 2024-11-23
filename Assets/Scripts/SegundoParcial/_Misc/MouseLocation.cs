using UnityEngine;

public class AnchorFollowMouse : MonoBehaviour
{
    private RectTransform rectTransform;
    public Camera uiCamera;
    public Vector2 offset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (uiCamera == null) return;

        Vector2 viewportPosition = uiCamera.ScreenToViewportPoint(Input.mousePosition);

        rectTransform.anchorMin = viewportPosition;
        rectTransform.anchorMax = viewportPosition;

        rectTransform.anchoredPosition = offset;
    }
}
