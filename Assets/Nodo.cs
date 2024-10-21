using TMPro;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public Nodo izq, der;
    public int dato;
    public Nodo parent;
    [SerializeField] private TextMeshProUGUI dataText;
    public float depth;

    private SpriteRenderer sprite;

    private int randomR;
    private int randomG;
    private int randomB;

    private void Awake()
    {
        dataText = GetComponent<TextMeshProUGUI>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        randomR = Random.Range(50, 250);
        randomG = Random.Range(50, 250);
        randomB = Random.Range(50, 250);

        sprite.color = (Color)(new Color32((byte)randomR, (byte)randomG, (byte)randomB, 255));
    }

    public Nodo(Nodo izq, Nodo der, int dato)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        parent = null;
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
