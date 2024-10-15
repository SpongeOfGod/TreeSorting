using UnityEngine;
using UnityEngine.EventSystems;

public class RotationManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Nodo nodo;
    private Nodo parent;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nodo = GetComponent<Nodo>();
    }

    public void OnPointerEnter(PointerEventData data) 
    {
        spriteRenderer.color = Color.red;
    }

    public void OnPointerExit(PointerEventData data)
    {
        spriteRenderer.color = Color.white;
    }

    public void OnPointerClick(PointerEventData data) 
    {
        spriteRenderer.color = Color.green;
        if (data.button.ToString() == "Left") 
        {
            
        }
    }
}
