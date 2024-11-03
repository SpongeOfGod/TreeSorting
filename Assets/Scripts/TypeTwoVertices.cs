using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeTwoVertices
{
    public VisualVertice StartVertice;
    public VisualVertice EndVertice;

    public TypeTwoVertices(VisualVertice startVertice, VisualVertice endVertice)
    {
        StartVertice = startVertice;
        EndVertice = endVertice;
    }
}
