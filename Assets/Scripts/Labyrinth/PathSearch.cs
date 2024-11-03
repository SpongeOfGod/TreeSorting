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
            once = true; // Inicia el chequeo de aristas salientes, aristas que tienen como origen al nodo especifico.
            verticesPath = CheckVerticeSaliente(graphManager.PlayerVertice.Vertice, new List<VisualVertice>());
        }                // Se utiliza Deep First Search, avanzando lo más posible hasta llegar a un bloqueo.
                         // Al chocar con el bloqueo (un nodo vacio o un nodo que ya se ha visitado), se retrocede hasta un nodo
                         // Que tenga caminos por explorar.

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
        if (verticesPath.Contains(graphManager.ExitVertice.Vertice.VerticeVisual))
        {
            return verticesPath;
        }

        if (vertice.visited) // Si se ha visitado el nodo, se lo remueve del camino
        {
            verticesPath.RemoveAt(verticesPath.Count - 1);
            return verticesPath;
        }

        vertice.visited = true;
        verticesPath.Add(vertice.VerticeVisual);

        VisualVertice currentVert = vertice.VerticeVisual;

        if (vertice.AristasSalientes.Count > 0) // Si tiene más de una arista que lleve a otro nodo
        {
            for (int i = 0; i < vertice.AristasSalientes.Count; i++) // Se comprueba que no sea un nodo ya visitado
            {
                if (!vertice.AristasSalientes[i].DestinationVert.visited && !verticesPath.Contains(graphManager.ExitVertice.Vertice.VerticeVisual))
                {
                    currentVert = vertice.AristasSalientes[i].DestinationVert.VerticeVisual;
                    verticesPath = CheckVerticeSaliente(currentVert.Vertice, verticesPath); // Recursividad - Crea una lista partir del vertice elegido.
                }
            }
        }

        if (vertice == graphManager.ExitVertice.Vertice)
        {
            return verticesPath;
        }

        return CheckVerticeSaliente(currentVert.Vertice, verticesPath); // Cuando se llega a un camino sin saluda, se regresa.
    }
}