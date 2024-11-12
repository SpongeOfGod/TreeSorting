using System.Collections.Generic;
using System.Linq;
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

        TravelPath(verticesPath);
    }

    public void TravelPath(List<VisualVertice> verticesPath)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > delayTime && verticesPath != null)
        {
            elapsedTime = 0;

            if (currentIndex < verticesPath.Count)
            {
                graphManager.PlayerVertice = verticesPath[currentIndex];
                currentIndex++;
            }
            else if (currentIndex >= verticesPath.Count && !graphManager.Labyrinth)
            {
                verticesPath = null;
                currentIndex = 0;
            }
        }
    }

    public List<VisualVertice> CheckVerticeSaliente(Vertice vertice, List<VisualVertice> verticesPath)
    {
        if (graphManager.ExitVertice == null) return null;
        List<VisualVertice> currentPath = new List<VisualVertice>();

        if (verticesPath.Contains(graphManager.ExitVertice.Vertice.VerticeVisual) && !graphManager.Labyrinth)
        {
            return verticesPath;
        }

        if (vertice.visited) // Si se ha visitado el nodo, se lo remueve del camino
        {
            if (verticesPath.Count > 0)
                verticesPath.RemoveAt(verticesPath.Count - 1);
            return verticesPath;
        }

        if (graphManager.Labyrinth) 
        {
            vertice.visited = true;
        }
        currentPath.Add(vertice.VerticeVisual);

        VisualVertice currentVert = vertice.VerticeVisual;
        graphManager.Graph.adyacentList.TryGetValue(vertice, out List<(Vertice, Arista)> AristasSalientes);

        List<List<VisualVertice>> Paths = new List<List<VisualVertice>>();
        if (AristasSalientes.Count > 0) // Si tiene más de una arista que lleve a otro nodo
        {
            for (int i = 0; i < AristasSalientes.Count; i++) // Se comprueba que no sea un nodo ya visitado
            {
                if (!AristasSalientes[i].Item2.DestinationVert.visited && !verticesPath.Contains(graphManager.ExitVertice.Vertice.VerticeVisual))
                {
                    currentVert = AristasSalientes[i].Item2.DestinationVert.VerticeVisual;
                    List<VisualVertice> vertices = new List<VisualVertice>();
                    currentPath.Clear();
                    currentPath.Add(vertice.VerticeVisual);
                    vertices = currentPath;
                    vertices.AddRange(verticesPath);
                    vertices = CheckVerticeSaliente(currentVert.Vertice, vertices);
                    Paths.Add(vertices); // Recursividad - Crea una lista partir del vertice elegido.
                }
            }

            if (Paths.Count > 0) 
            {
                currentPath = Paths[0];
                foreach (var item in Paths)
                {
                    if (currentPath.Count > item.Count && item.Contains(graphManager.ExitVertice)) 
                    {
                        currentPath = item;
                    }
                }
            }
        }

        if (vertice == graphManager.ExitVertice.Vertice)
        {
            currentPath.AddRange(verticesPath);
            return currentPath;
        }
        else
        {
            vertice.visited = true;
        }

        return CheckVerticeSaliente(currentVert.Vertice, currentPath); // Cuando se llega a un camino sin saluda, se regresa.
    }
}