using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisualVertice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
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
    }

    private void Start()
    {
        Color generatedColor;

        if (spawnGraph.Labyrinth)
        {
            generatedColor = new Color32(30, 30, 30, 255);
        }
        else
        {
            do
            {
                int randomR = Random.Range(50, 200);
                int randomG = Random.Range(50, 200);
                int randomB = Random.Range(50, 200);
                generatedColor = new Color32((byte)randomR, (byte)randomG, (byte)randomB, 255);
            }
            while (IsTooLight(generatedColor) || IsPrimaryColor(generatedColor));
        }

        if (spawnGraph.Maker)
        {
            generatedColor = Color.gray;
        }
        originalColor = generatedColor;
        Sprite.color = originalColor;

        string verticeText = spawnGraph.Labyrinth ? gameObject.name : Vertice.Value.ToString();
        DataText.text = verticeText;
    }

    private bool IsTooLight(Color color) // Comprueba si los valores (r)ed, (g)reen, (b)lue pasan cierto limite.
    {
        float brightness = 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
        return brightness > 0.7f;
    }

    private bool IsPrimaryColor(Color color) // Comprueba si los valores (r)ed, (g)reen, (b)lue est�n cerca del color rojo o verde.
    {
        return (color.r > 0.8f && color.g < 0.2f && color.b < 0.2f) ||
               (color.g > 0.8f && color.r < 0.2f && color.b < 0.2f);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (Sprite != null && !Vertice.spawnGraph.InSearch && !Vertice.spawnGraph.Labyrinth)
        {
            Sprite.color = Color.red;
            Vertice.spawnGraph.ExitVertice = this;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (Sprite != null && !Vertice.spawnGraph.InSearch && !Vertice.spawnGraph.Labyrinth)
        {
            Sprite.color = originalColor;
            Vertice.spawnGraph.ExitVertice = null;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (!Vertice.spawnGraph.Maker) 
        {
            if (Vertice.spawnGraph.Labyrinth) 
            {
                if (!Vertice.spawnGraph.InSearch) 
                {
                    Vertice.spawnGraph.PlayerVertice = this;
                }
            }
            else 
            {
                Vertice.spawnGraph.ExitVertice = this;
            }
        }
        else if (Vertice.spawnGraph.Maker && !Vertice.spawnGraph.InSearch)
        {
            ChangeVerticeByType(MazeTagManager.Instance.Input);
        }
    }

    public void ChangeVerticeByType(string type) 
    {
        switch (type) 
        {
            case "Wall":
                if (Vertice.spawnGraph.PlayerVertice == Vertice.VerticeVisual || Vertice.spawnGraph.ExitVertice == Vertice.VerticeVisual) 
                    return;
                else 
                {
                    Vertice.VerticeVisual.gameObject.tag = type;
                    Sprite.color = Color.black;
                    break;
                }

            case "Vertice":
                if (Vertice.spawnGraph.PlayerVertice == Vertice.VerticeVisual || Vertice.spawnGraph.ExitVertice == Vertice.VerticeVisual) 
                    return;
                else 
                {
                    Vertice.VerticeVisual.gameObject.tag = type;
                    Sprite.color = Color.gray;
                    break;
                }

            case "Player":
                if (gameObject.CompareTag("Wall") || Vertice.spawnGraph.ExitVertice == Vertice.VerticeVisual) 
                    return;
                else 
                {
                    if (Vertice.spawnGraph.PlayerVertice != null)
                        Vertice.spawnGraph.PlayerVertice.Sprite.color = Color.gray;

                    Vertice.spawnGraph.PlayerVertice = Vertice.VerticeVisual;
                    Vertice.spawnGraph.StartVertice = this;
                    Sprite.color = Color.blue;
                    break;
                }

            case "Exit":
                if (gameObject.CompareTag("Wall") || Vertice.spawnGraph.PlayerVertice == Vertice.VerticeVisual) 
                    return;
                else 
                {
                    if (Vertice.spawnGraph.ExitVertice != null)
                        Vertice.spawnGraph.ExitVertice.Sprite.color = Color.gray;

                    Vertice.spawnGraph.ExitVertice = Vertice.VerticeVisual;
                    Sprite.color = Color.red;
                    break;
                }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!spawnGraph.Maker)
            Sprite.color = originalColor;
    }
}
