using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class VisualNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI DataText;
    public SpriteRenderer Sprite;
    public Nodo Nodo;
    [SerializeField] private Color originalColor;

    private void Awake()
    {
        Nodo = new Nodo(this);
        DataText = GetComponent<TextMeshProUGUI>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        int randomR = Random.Range(50, 250);
        int randomG = Random.Range(50, 250);
        int randomB = Random.Range(50, 250);

        originalColor = (Color)(new Color32((byte)randomR, (byte)randomG, (byte)randomB, 255));
        Sprite.color = originalColor;
    }


    public void OnPointerEnter(PointerEventData data)
    {
        if (Sprite != null)
        {
            Sprite.color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (Sprite != null)
        {
            Sprite.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        Sprite.color = Color.green;
    }
}
