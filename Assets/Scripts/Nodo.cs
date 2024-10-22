using UnityEngine;
using TMPro;

public class Nodo : MonoBehaviour
{
    private TextMeshProUGUI dataText;
    private SpriteRenderer sprite;

    public int dato;
    public Nodo izq;
    public Nodo der;
    public Nodo parent;
    public float depth;

    private void Awake()
    {
        dataText = GetComponent<TextMeshProUGUI>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        int randomR = Random.Range(50, 250);
        int randomG = Random.Range(50, 250);
        int randomB = Random.Range(50, 250);

        sprite.color = (Color)(new Color32((byte)randomR, (byte)randomG, (byte)randomB, 255));
    }

    public void AssignData(int dato, Nodo izq, Nodo der, Nodo parent, float depth)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        this.parent = parent;
        this.depth = depth;
        dataText.text = dato.ToString();
    }
}