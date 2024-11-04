using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour // Manager de Grafo.
{
    [SerializeField] List<VisualVertice> visualVertices = new List<VisualVertice>(); // Contiene todos los vertices iniciales.
    [SerializeField] private float elapsedTime = 0;
    [SerializeField] float delayTime = 5;
    [SerializeField] public bool Labyrinth = false;
    [SerializeField] PathSearch PathSearch;
    public List<VisualVertice> VisualVertices => visualVertices;
    public List<VisualVertice> PathToFollow = new List<VisualVertice>(); // Vertices que forman el camino que se debe tomar.
    public VisualVertice PlayerVertice; // Vertice actual en el que se encuentra el jugador.
    public VisualVertice HoverVertice;
    public VisualVertice ExitVertice;
    public DynamicGraph<Vertice> Graph; // Grafo dinámico, guarda los vertices.
    public bool CanArrive;
    public string textShow = string.Empty;

    private int Weight;
    void Start()
    {
        Graph = new DynamicGraph<Vertice>();

        Graph.InitializeGraph(this);

        foreach (var vertice in visualVertices) 
        {
            Graph.AddVertice(vertice.Vertice);
        }

        if (!Labyrinth)
            PlayerVertice = visualVertices[0];
    }
    void Update()
    {
        if (!Labyrinth)
        {
            GraphTravel();
        }
        else 
        {
            PathSearch.RunUpdate();
        }
    }

    private void GraphTravel()
    {
        if (PlayerVertice == HoverVertice)
        {
            CanArrive = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && (ExitVertice != null || ExitVertice != PlayerVertice) && PathToFollow.Count == 0) // Se inicia el chequeo del camino desde la posición del jugador.
        {

            foreach (VisualVertice visualVertice in visualVertices)
            {
                visualVertice.Vertice.visited = false;
            }
            PlayerVertice.Vertice.visited = false;

            PathToFollow = PathSearch.CheckVerticeSaliente(PlayerVertice.Vertice, PathToFollow);

            textShow = string.Empty;
            if (PathToFollow.Count > 1)
                foreach (var vertice in PathToFollow)
                {
                    foreach (Arista arista in vertice.Vertice.AristasSalientes)
                    {
                        Weight += arista.Weight;
                    }
                }
            if (Weight > 0)
            {
                textShow = $" ...Costo ${Weight} llegar hasta aquí.";
                Weight = 0;
            }
            else if (Weight == 0)
            {
                textShow = $" ... No hubo movimiento, sigues en {PlayerVertice.Vertice.Value}.";
            }
        }

        if (PathToFollow.Count > 0)
        {

            foreach (VisualVertice visualVertice in visualVertices)
            {
                visualVertice.Vertice.visited = false;
            }

            PathSearch.TravelPath(PathToFollow);
        }

        if (PlayerVertice == ExitVertice)
        {
            PathToFollow.Clear();
        }
    }

    public void AddConnectionBetweenPoints(Vertice VerticeA, Vertice VerticeB, int weight)
    {
        if (Graph.AddConnection(VerticeA, VerticeB, weight))
        {
            Debug.Log($"Added a connection between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
        }
        else
        {
            Debug.Log($"Couldn't add connection - Connection already exits between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
        }
    }
}
