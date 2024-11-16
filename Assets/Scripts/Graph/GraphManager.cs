using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GraphManager : MonoBehaviour // Manager de Grafo.
{
    [SerializeField] List<VisualVertice> visualVertices = new List<VisualVertice>(); // Contiene todos los vertices iniciales.
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float elapsedTime = 0;
    [SerializeField] float delayTime = 5;
    [SerializeField] PathSearch PathSearch;
    public List<VisualVertice> VisualVertices => visualVertices;
    public List<VisualVertice> PathToFollow = new List<VisualVertice>(); // Vertices que forman el camino que se debe tomar.
    public VisualVertice PlayerVertice; // Vertice actual en el que se encuentra el jugador.
    public VisualVertice HoverVertice;
    public VisualVertice ExitVertice;
    public DynamicGraph<Vertice> Graph; // Grafo dinámico, guarda los vertices.
    public bool CanArrive;
    public bool InSearch = false;
    public string textShow = string.Empty;
    public UITextVerticeCosto travelCost;
    public bool Labyrinth = false;
    public bool Maker = false;
    private bool resolveLabyrinth;

    private int Weight;

    private void Start()
    {
        if (!Labyrinth) 
        {
            Initialize();
        }

        Graph = new DynamicGraph<Vertice>();

        Graph.InitializeGraph(this);

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Vertice");
        foreach (var vertice in gameObjects)
        {
            vertice.TryGetComponent<VisualVertice>(out VisualVertice visualVertice);
            if (visualVertice != null)
            {
                Graph.AddVertice(visualVertice.Vertice);
            }
        }
    }
    public void Initialize()
    {
        if (!Labyrinth)
            PlayerVertice = visualVertices[0];

        Maker = false;
        resolveLabyrinth = true;
    }

    void Update()
    {
        if (Maker) return;
        if (!Labyrinth)
        {
            GraphTravel();
        }
        else
        {
            if (!resolveLabyrinth) return;

            PathSearch.RunUpdate();
        }
    }

    private void GraphTravel()
    {
        if (HoverVertice != null)
            foreach (var arista in PlayerVertice.Vertice.AristasSalientes)
                if (arista.DestinationVert == HoverVertice.Vertice)
                    CanArrive = true;
                else
                    CanArrive = false;


        if (Input.GetMouseButtonDown(0) && ExitVertice != PlayerVertice && PathToFollow.Count == 0 && !InSearch) // Se inicia el chequeo del camino desde la posición del jugador.
        {
            if (PlayerVertice == null || ExitVertice == null) return;

            InSearch = true;
            foreach (VisualVertice visualVertice in visualVertices)
            {
                visualVertice.Vertice.visited = false;
            }
            PlayerVertice.Vertice.visited = false;
            List<VisualVertice> path =  PathSearch.CheckVerticeSaliente(PlayerVertice.Vertice);

            if (path == null) 
            {
                InSearch = false;
                return;
            }

            PathToFollow.AddRange(path);
            PathToFollow.Reverse();
            
            textShow = string.Empty;
            if (PathToFollow.Count > 1)
                foreach (var vertice in PathToFollow)
                {
                    foreach (Arista arista in vertice.Vertice.AristasSalientes)
                    {
                        Weight += arista.Weight;
                    }
                }
            if (travelCost.weight > 0)
            {
                textShow = $" ...Costo ${travelCost.weight} llegar hasta aquí.";
                travelCost.weight = 0;
            }
            else if (travelCost.weight == 0)
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
            InSearch = false;
        }
    }

    public void AddConnectionBetweenPoints(Vertice VerticeA, Vertice VerticeB, int weight)
    {
        if (Graph.AddConnection(VerticeA, VerticeB, weight) && !Labyrinth)
        {
            Debug.Log($"Added a connection between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
            LineArrowGenerator(VerticeA, VerticeB);
        }
        else
        {
            Debug.Log($"Couldn't add connection - Connection already exists between {VerticeA.Value} (Origin) and {VerticeB.Value} (Destination)");
        }
    }

    private void LineArrowGenerator(Vertice VerticeA, Vertice VerticeB)
    {
        VisualVertice visualVerticeA = visualVertices.Find(v => v.Vertice == VerticeA);
        VisualVertice visualVerticeB = visualVertices.Find(v => v.Vertice == VerticeB);

        if (visualVerticeA != null && visualVerticeB != null)
        {
            GameObject connectionObject = Instantiate(line, canvas.transform);
            LineRenderer lineRenderer = connectionObject.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, visualVerticeA.transform.position);
            lineRenderer.SetPosition(1, visualVerticeB.transform.position);

            Vector3 direction = (visualVerticeB.transform.position - visualVerticeA.transform.position).normalized;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);

            float offsetDistance = 0.56f;
            Vector3 arrowOffset = -direction * offsetDistance;

            GameObject arrowObject = Instantiate(arrow, visualVerticeB.transform.position + arrowOffset, rotation, canvas.transform);
        }
    }
}
