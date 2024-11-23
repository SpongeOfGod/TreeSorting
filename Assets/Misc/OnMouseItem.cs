using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 initialScale;
    private Vector3 targetScale;
    private float scaleSpeed = 5f;
    private bool isHovered = false;

    private float rotationSpeed = 100f;
    private Transform icon;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float rotationLerpSpeed = 5f;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale;

        icon = transform.Find("Icon");
        if (icon != null)
        {
            initialRotation = icon.rotation;
            targetRotation = initialRotation;
        }
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        if (isHovered && icon != null)
        {
            icon.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (icon != null)
        {
            icon.rotation = Quaternion.Lerp(icon.rotation, initialRotation, Time.deltaTime * rotationLerpSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = initialScale * 1.1f;
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = initialScale;
        isHovered = false;
    }
}
