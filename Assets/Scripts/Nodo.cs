using UnityEngine;
using TMPro;

public class Nodo
{
    public VisualNode visualNode;
    public int dato;
    public Nodo izq;
    public Nodo der;
    public Nodo parent;
    public float depth;

    public Nodo(VisualNode visualNode) 
    {
        this.visualNode = visualNode;
    }


    public void AssignData(int dato, Nodo izq, Nodo der, Nodo parent, float depth)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        this.parent = parent;
        this.depth = depth;
        visualNode.DataText.text = dato.ToString();
    }
}