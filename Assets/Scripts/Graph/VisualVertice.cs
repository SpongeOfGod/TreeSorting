using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisualVertice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GraphManager spawnGraph;
    public TextMeshProUGUI DataText;
    public SpriteRenderer Sprite;
    public Vertice Vertice;
    public RectTransform RectTransform;
    [SerializeField] List<(GameObject, int)> values = new List<(GameObject, int)>();
    [SerializeField] private Color originalColor;

    public int initialLimit = 0;
    private void Awake()
    {
        DataText = GetComponent<TextMeshProUGUI>();
        Sprite = GetComponent<SpriteRenderer>();

        GameObject gameObject = GameObject.FindGameObjectWithTag("Graph");
        spawnGraph = gameObject.GetComponent<GraphManager>();
        int VerticeData = Random.Range(1, 100);
        Vertice = new Vertice(VerticeData, this, spawnGraph);
        
        DataText.text = Vertice.Value.ToString();
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

    private bool IsTooLight(Color color) // Comprueba si los valores (r)ed, (g)reen, (b)lue pasan cierto limite.
    {
        float brightness = 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
        return brightness > 0.7f;
    }

    private bool IsPrimaryColor(Color color) // Comprueba si los valores (r)ed, (g)reen, (b)lue están cerca del color rojo o verde.
    {
        return (color.r > 0.8f && color.g < 0.2f && color.b < 0.2f) ||
               (color.g > 0.8f && color.r < 0.2f && color.b < 0.2f);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (Sprite != null)
        {
            Sprite.color = Color.red;
            Vertice.spawnGraph.HoverVertice = this;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (Sprite != null)
        {
            Sprite.color = originalColor;
            Vertice.spawnGraph.HoverVertice = null;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        Sprite.color = Color.green;
        if (!Vertice.spawnGraph.PathToFollow.Contains(this)) 
        {
            Vertice.spawnGraph.PathToFollow.Add(this);
        }
    }
}
