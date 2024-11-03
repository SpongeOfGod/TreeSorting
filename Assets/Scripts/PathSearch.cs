using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSearch : MonoBehaviour
{
    [SerializeField] GraphManager graphManager;
    [SerializeField] private float elapsedTime = 0;
    [SerializeField] float delayTime = 5;
    bool once;
    [SerializeField] List<VisualVertice> verticesPath;
    int currentIndex = 0;
    public void RunUpdate()
    {
        if (graphManager.PlayerVertice != null && !once)
        {
            once = true;
            verticesPath = CheckVerticeSaliente(graphManager.PlayerVertice.Vertice, new List<VisualVertice>());
        }


        elapsedTime += Time.deltaTime;
        if (elapsedTime > delayTime && verticesPath != null)
        {
            elapsedTime = 0;
            
            if (currentIndex < verticesPath.Count) 
            {
                graphManager.PlayerVertice = verticesPath[currentIndex];
                currentIndex++;
            }
        }
    }
    public List<VisualVertice> CheckVerticeSaliente(Vertice vertice, List<VisualVertice> verticesPath)
    {
        if (vertice == graphManager.ExitVertice.Vertice) 
        {
            this.enabled = false;
            graphManager.enabled = false;
        }

        if (vertice.visited)
        {
            verticesPath.Remove(vertice.VerticeVisual);
            return verticesPath;
        }

        vertice.visited = true;
        verticesPath.Add(vertice.VerticeVisual);
        VisualVertice currentVert = vertice.VerticeVisual;
        if (vertice.AristasSalientes.Count > 0)
        {
            for (int i = 0; i < vertice.AristasSalientes.Count; i++)
            {
                if (!vertice.AristasSalientes[i].DestinationVert.visited) 
                {
                    currentVert = vertice.AristasSalientes[i].DestinationVert.VerticeVisual;
                    verticesPath.Add(currentVert);
                    break;
                }
            }
        }

        if (vertice == graphManager.ExitVertice.Vertice)
        {
            graphManager.PlayerVertice = graphManager.ExitVertice;
            this.enabled = false;
            graphManager.enabled = false;
        }

        return CheckVerticeSaliente(currentVert.Vertice, verticesPath);
    }
}
