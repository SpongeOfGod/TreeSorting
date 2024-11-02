using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour // Manager de Grafo.
{
    [SerializeField] VisualVertice[] visualVertices; // Contiene todos los vertices iniciales.
    public List<VisualVertice> PathToFollow = new List<VisualVertice>(); // Vertices que forman el camino que se debe tomar.
    public VisualVertice PlayerVertice; // Vertice actual en el que se encuentra el jugador.
    public bool once;
    DynamicGraph<Vertice> Graph; // Grafo dinámico, guarda los vertices.
    private int Weight;
    void Start()
    {
        Graph = new DynamicGraph<Vertice>();

        Graph.InitializeGraph(this);

        foreach (var vertice in visualVertices) 
        {
            Graph.AddVertice(vertice.Vertice);
        }

        PlayerVertice = visualVertices[0];
    }

    string CheckDepth(Vertice vertice, string text, int currentindexPath) // Comprueba el camino que el jugador debe tomar.
    {
        int indexPath = currentindexPath;
        indexPath++;
        if (!PathToFollow.Contains(vertice.VerticeVisual) || indexPath >= PathToFollow.Count) // Si ´se llega al final del camino, se termina el chequeo.
            return text + $"{vertice.Value} - Destino";

        Vertice verticeToGo = default(Vertice);
        foreach (var arista in vertice.AristasSalientes)
        {
            if (arista.DestinationVert == PathToFollow[indexPath].Vertice) // En cada arista, verifica si el vertice destino es igual al siguiente vertice al que debe ir.
            {
                Weight += arista.Weight;
                verticeToGo = arista.DestinationVert;
                PlayerVertice = verticeToGo.VerticeVisual; // Se asigna el vertice del jugador en cada nuevo vertice, sino, se quedaria atrás.
            }
        }

        if (verticeToGo == default(Vertice))
        {
            return text + $"{vertice.Value} - No se pudo avanzar por ese camino"; // Si el camino a seguir no cambia, no se pudo avanzar.
        }
        return CheckDepth(verticeToGo, text + vertice.Value + $" → ", indexPath);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && PathToFollow.Count > 0) // Se inicia el chequeo del camino desde la posición del jugador.
        {
            string text = CheckDepth(PlayerVertice.Vertice, "Origen - ", 0);
            if (Weight > 0) 
            {
                Debug.Log(text + $" ...Costo ${Weight} llegar hasta aquí.");
                Weight = 0;
            }
            else if (Weight == 0) 
            {
                Debug.Log(text + $" ... No hubo movimiento, sigues en {PlayerVertice.Vertice.Value}, perdiste valioso tiempo.");
            }
            PathToFollow.Clear();
        }

        if (Input.GetKeyDown(KeyCode.V) && PathToFollow.Count > 0) // Se le envia al grafo 2 vertices a elección.
        { 
            Vertice secondVert = PathToFollow.Count > 1 ? PathToFollow[1].Vertice : PathToFollow[0].Vertice;
            AddConnectionBetweenPoints(PathToFollow[0].Vertice, secondVert);
            PathToFollow.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Se le envia al grafo 2 vertices aleatorios.
        {
            var VerticeA = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));
            var VerticeB = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));

            AddConnectionBetweenPoints(VerticeA, VerticeB);
        }
    }

    private void AddConnectionBetweenPoints(Vertice VerticeA, Vertice VerticeB)
    {
        if (Graph.AddConnection(VerticeA, VerticeB))
        {
            Debug.Log($"Added a connection between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
        }
        else
        {
            Debug.Log($"Couldn't add connection - Connection already exits between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
        }
    }
}
