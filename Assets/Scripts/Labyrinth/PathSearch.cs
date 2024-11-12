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
        }                // Se utiliza Breadth First Search (BFS).

        if (verticesPath != null && verticesPath.Count > 0)
        {
            TravelPath(verticesPath);
        }
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

        // Usamos una cola para BFS
        Queue<Vertice> queue = new Queue<Vertice>();
        Dictionary<Vertice, List<VisualVertice>> paths = new Dictionary<Vertice, List<VisualVertice>>(); // Para rastrear el camino recorrido

        // Agregamos el vértice inicial a la cola
        queue.Enqueue(vertice);
        paths[vertice] = new List<VisualVertice> { vertice.VerticeVisual };

        while (queue.Count > 0)
        {
            var currentVertice = queue.Dequeue();

            // Si encontramos el vértice de salida, devolvemos el camino
            if (currentVertice == graphManager.ExitVertice.Vertice)
            {

                var finalPath = paths[currentVertice];

                if (!graphManager.Labyrinth)
                    finalPath.Reverse();

                return finalPath;
            }

            // Recorremos los vecinos (aristas salientes)
            var adjacentEdges = graphManager.Graph.adyacentList.GetValueOrDefault(currentVertice, new List<(Vertice, Arista)>());

            foreach (var edge in adjacentEdges)
            {
                Vertice neighbor = edge.Item2.DestinationVert;

                // Si el vecino no ha sido visitado
                if (!paths.ContainsKey(neighbor) && !neighbor.visited)
                {
                    // Marcamos el vecino como visitado (para evitar ciclos)
                    neighbor.visited = true;

                    // Creamos un nuevo camino añadiendo el vértice vecino
                    List<VisualVertice> newPath = new List<VisualVertice>(paths[currentVertice]) { neighbor.VerticeVisual };
                    paths[neighbor] = newPath;

                    // Agregamos el vecino a la cola para explorarlo
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Si no se encontró un camino, devolvemos null
        return null;
    }
}
