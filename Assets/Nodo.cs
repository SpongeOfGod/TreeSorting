using TMPro;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public Nodo izq, der;
    public int dato;
    public Nodo parent;
    [SerializeField] private TextMeshProUGUI dataText;
    public float depth;
    private void Start()
    {
        dataText = GetComponent<TextMeshProUGUI>();
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
