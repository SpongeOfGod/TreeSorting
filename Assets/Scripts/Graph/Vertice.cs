using System.Collections.Generic;

[System.Serializable]
public class Vertice
{
    public List<Arista> AristasEntrantes; // Aristas entrantes: Utilizan al vertice como destino.
    public List<Arista> AristasSalientes; // Aristas saliente: Utilizan al vertice como origen.
    public int Value; 
    public VisualVertice VerticeVisual;
    public Vertice(int value, VisualVertice visualVertice) 
    {
        this.Value = value;

        if (visualVertice == null) 
            VerticeVisual = ParentManager.Instance.GetVerticeInstance();
        else 
            VerticeVisual = visualVertice;
        
        AristasEntrantes = new List<Arista>();
        AristasSalientes = new List<Arista>();
        VerticeVisual.DataText.text = value.ToString();
    }
}
