using UnityEngine;
using TMPro;

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
        visualNode = ParentManager.Instance.GetNodeInstance();
        visualNode.DataText.text = dato.ToString();
        visualNode.Nodo = this;
        this.dato = dato;
        visualNode.gameObject.name = dato.ToString();
    }

    public void setParentNode(Nodo node)
    {
        parent = node;
        depth = parent.depth + 1;
    }

    public void SetVisualPosition(int posY, int offsetMultiplier)
    {
        visualNode.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX, posY);
    }

    public void AssignData(int dato, Nodo izq, Nodo der, Nodo parent, int depth)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        this.parent = parent;
        this.depth = depth;
        visualNode.DataText.text = dato.ToString();
        visualNode.gameObject.name = dato.ToString();
    }
}
