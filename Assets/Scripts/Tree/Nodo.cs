using UnityEngine;
using TMPro;
using System.Diagnostics.Contracts;

[System.Serializable]
public class Nodo
{
    public VisualNode visualNode;
    public int dato;
    [SerializeField] public Nodo izq;
    [SerializeField] public Nodo der;
    public Nodo parent;
    public int depth;
    public int positionX = 114;

    public Nodo(int dato) 
    {
        visualNode = GameManager.Instance.GetNodeInstance();
        visualNode.DataText.text = dato.ToString();
        visualNode.Nodo = this;
        this.dato = dato;
    }

    public void SetVisualPosition(int posY, int offsetMultiplier) 
    {
        visualNode.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX * (/*offsetMultiplier / */(depth + 1)), posY);
    }

    public void AssignData(int dato, Nodo izq, Nodo der, Nodo parent, int depth)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        this.parent = parent;
        this.depth = depth;
        visualNode.DataText.text = dato.ToString();
    }
}