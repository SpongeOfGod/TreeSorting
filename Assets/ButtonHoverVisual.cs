using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverVisual : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Texture imageToShow;
    [SerializeField] Texture DefaultTexture;
    [SerializeField] string textToShow;

    [Header("PropertiesToChange")]
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] RawImage imageUI;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (imageUI != null) 
        {
            imageUI.texture = imageToShow;
        }

        if (textUI != null) 
        {
            textUI.text = textToShow;
        }
    }
}
