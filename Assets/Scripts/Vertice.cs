using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice
{
    public List<Arista> AristasEntrantes = new List<Arista>();
    public List<Arista> AristasSalientes = new List<Arista>();

    private VerticeVisual verticeVisual;
    
    public Vertice(VerticeVisual verticeVisual) 
    {
        this.verticeVisual = verticeVisual;
    }
}
