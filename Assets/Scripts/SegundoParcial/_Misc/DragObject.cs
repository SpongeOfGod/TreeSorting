using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Vector3 offset;
    private bool isDragging = false;
    private float scaleSpeed = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = GetMouseWorldPosition();
            offset = transform.position - dragOrigin;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            transform.position = currentMousePosition + offset;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            Vector3 newScale = transform.localScale + new Vector3(scrollInput * scaleSpeed, scrollInput * scaleSpeed, 0);
            transform.localScale = new Vector3(
                Mathf.Max(newScale.x, 0.1f),
                Mathf.Max(newScale.y, 0.1f),
                transform.localScale.z
            );
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
