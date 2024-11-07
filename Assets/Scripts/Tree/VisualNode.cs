using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisualNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI DataText;
    public SpriteRenderer Sprite;
    public Nodo Nodo;
    public string ParentNode;
    [SerializeField] private Color originalColor;
    private RectTransform rectTransform;
    private void Awake()
    {
        DataText = GetComponent<TextMeshProUGUI>();
        Sprite = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Color generatedColor;
        do
        {
            int randomR = Random.Range(50, 200);
            int randomG = Random.Range(50, 200);
            int randomB = Random.Range(50, 200);
            generatedColor = new Color32((byte)randomR, (byte)randomG, (byte)randomB, 255);
        }
        while (IsTooLight(generatedColor) || IsPrimaryColor(generatedColor));

        originalColor = generatedColor;
        Sprite.color = originalColor;
    }

    private bool IsTooLight(Color color)
    {
        float brightness = 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
        return brightness > 0.7f;
    }

    private bool IsPrimaryColor(Color color)
    {
        return (color.r > 0.8f && color.g < 0.2f && color.b < 0.2f) ||
               (color.g > 0.8f && color.r < 0.2f && color.b < 0.2f);
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

    public void SetVisualPosition(int posY, int offsetMultiplier)
    {
        rectTransform.anchoredPosition = new Vector2(Nodo.positionX, posY);
    }
}
