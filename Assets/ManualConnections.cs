using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualConnections : MonoBehaviour
{
    [SerializeField] List<TypeTwoVertices> verticesConnections;
    [SerializeField] GraphManager graphManager;
    bool once;

    void Update()
    {
        if (graphManager != null && !once)
        {
            once = true;
            foreach (TypeTwoVertices verticesConnections in verticesConnections)
            {
                graphManager.AddConnectionBetweenPoints(verticesConnections.StartVertice.Vertice, verticesConnections.EndVertice.Vertice, 0);
            }
        }
    }
}

