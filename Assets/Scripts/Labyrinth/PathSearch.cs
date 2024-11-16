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
        if (graphManager.Graph == null) return;
        if (graphManager.PlayerVertice != null && !once)
        {
            once = true; // Inicia el chequeo de aristas salientes, aristas que tienen como origen al nodo específico.
            verticesPath = CheckVerticeSaliente(graphManager.PlayerVertice.Vertice);
        }

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

    public List<VisualVertice> CheckVerticeSaliente(Vertice startVertice)
    {
        if (graphManager.ExitVertice == null) return null;

        // Si estamos en el modo laberinto, utilizamos BFS sin priorización por distancia y peso
        if (graphManager.Labyrinth && graphManager.Graph != null)
        {
            return PerformBFS(startVertice);
        }

        // Cola de prioridad donde el primer criterio es la distancia y el segundo es el peso acumulado
        var priorityQueue = new SortedDictionary<(int, int), List<Vertice>>(); // (distancia, peso acumulado)
        Dictionary<Vertice, (int distance, int weight)> minMetrics = new Dictionary<Vertice, (int, int)>(); // Para rastrear la distancia y peso hasta cada vértice
        Dictionary<Vertice, List<VisualVertice>> paths = new Dictionary<Vertice, List<VisualVertice>>(); // Para rastrear el camino

        // Inicializamos la cola con el vértice de inicio, distancia 0 y peso 0
        priorityQueue[(0, 0)] = new List<Vertice> { startVertice };
        minMetrics[startVertice] = (0, 0); // Distancia 0, peso 0
        paths[startVertice] = new List<VisualVertice> { startVertice.VerticeVisual };

        while (priorityQueue.Count > 0)
        {
            var currentMetrics = priorityQueue.First().Key; // (distancia, peso acumulado)
            var currentPathVertices = priorityQueue.First().Value;
            var currentVertice = currentPathVertices.Last(); // Último vértice en el camino actual
            priorityQueue.Remove(currentMetrics);

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
                int newDistance = currentMetrics.Item1 + 1; // La distancia es el número de aristas
                int newWeight = currentMetrics.Item2 + edge.Item2.Weight; // Peso acumulado

                // Si encontramos un camino más corto (en distancia) o un camino con la misma distancia pero menor peso
                if (!minMetrics.ContainsKey(neighbor) ||
                    newDistance < minMetrics[neighbor].distance ||
                    (newDistance == minMetrics[neighbor].distance && newWeight < minMetrics[neighbor].weight))
                {
                    minMetrics[neighbor] = (newDistance, newWeight); // Actualizamos distancia y peso

                    var newPath = new List<VisualVertice>(paths[currentVertice]) { neighbor.VerticeVisual };
                    paths[neighbor] = newPath;

                    // Agregamos el vecino a la cola de prioridad
                    var priorityKey = (newDistance, newWeight); // La clave es primero la distancia y luego el peso
                    if (!priorityQueue.ContainsKey(priorityKey))
                        priorityQueue[priorityKey] = new List<Vertice>();

                    priorityQueue[priorityKey].Add(neighbor);
                }
            }
        }

        // Si no se encontró un camino, devolvemos null
        return null;
    }

    // Método BFS normal para laberintos
    private List<VisualVertice> PerformBFS(Vertice startVertice)
    {
        Queue<Vertice> queue = new Queue<Vertice>();
        Dictionary<Vertice, List<VisualVertice>> paths = new Dictionary<Vertice, List<VisualVertice>>();

        // Inicializamos la cola con el vértice de inicio
        queue.Enqueue(startVertice);
        paths[startVertice] = new List<VisualVertice> { startVertice.VerticeVisual };

        while (queue.Count > 0)
        {
            var currentVertice = queue.Dequeue();

            // Si encontramos el vértice de salida, devolvemos el camino
            if (currentVertice == graphManager.ExitVertice.Vertice)
            {
                return paths[currentVertice];
            }

            // Recorremos los vecinos (aristas salientes)
            var adjacentEdges = graphManager.Graph.adyacentList.GetValueOrDefault(currentVertice, new List<(Vertice, Arista)>());

            foreach (var edge in adjacentEdges)
            {
                Vertice neighbor = edge.Item2.DestinationVert;

                // Si el vecino no ha sido visitado
                if (!paths.ContainsKey(neighbor))
                {
                    // Marcamos al vecino y agregamos su camino
                    List<VisualVertice> newPath = new List<VisualVertice>(paths[currentVertice]) { neighbor.VerticeVisual };

                    paths[neighbor] = newPath;

                    // Agregamos el vecino a la cola
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Si no se encontró un camino, devolvemos null
        return null;
    }
}
