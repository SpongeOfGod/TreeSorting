using System;

[System.Serializable]
    public class TreeStructure
    {
    public Nodo root { get; set; }
    public int posY = -144;
    public int offsetMultiplier = 3;
    public int depth = 0;
    public int posX = 10;

    public virtual void Initialize() { }
    public virtual Nodo InsertNode(Nodo node, int value)
    {
        return null;
    }

    public int CheckDepth(Nodo nodo)
    {
        if (nodo == null) return -1;

        return (1 + Math.Max(CheckDepth(nodo.izq), CheckDepth(nodo.der)));
    }

    // public  -> 4 algoritmos de order
    }
