using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// MVP
/// MVC

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
    public VisualVertice StartVertice;
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
        {
            PlayerVertice = visualVertices[0];
        }
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
        if (Input.GetMouseButtonDown(0) && ExitVertice != PlayerVertice && PathToFollow.Count == 0 && !InSearch) // Se inicia el chequeo del camino desde la posición del jugador.
        {
            if (PlayerVertice == null || ExitVertice == null) return;

            InSearch = true;

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
            LineArrowGenerator(VerticeA, VerticeB);
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

    public void ReOrder() 
    {
        if (SceneManager.GetActiveScene().name == "Maze Maker")
            Maker = true;

        PathToFollow.Clear();
        resolveLabyrinth = false;
        PlayerVertice = StartVertice;
        PathSearch.once = false;
        InSearch = false;
        if (PathSearch.VerticesPath != null)
            PathSearch.VerticesPath.Clear();
        PathSearch.currentIndex = 0;
    }
}
