﻿using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour // Manager de Grafo.
{
    [SerializeField] List<VisualVertice> visualVertices = new List<VisualVertice>(); // Contiene todos los vertices iniciales.
    public List<VisualVertice> VisualVertices => visualVertices;
    public List<VisualVertice> PathToFollow = new List<VisualVertice>(); // Vertices que forman el camino que se debe tomar.
    public VisualVertice PlayerVertice; // Vertice actual en el que se encuentra el jugador.
    public VisualVertice HoverVertice;
    public VisualVertice ExitVertice;
    public DynamicGraph<Vertice> Graph; // Grafo dinámico, guarda los vertices.
    private int Weight;
    public bool CanArrive;
    [SerializeField] private float elapsedTime = 0;
    [SerializeField] float delayTime = 5;
    [SerializeField] public bool Labyrinth = false;
    [SerializeField] PathSearch PathSearch;
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

    bool CheckCanArrive(Vertice vertice, int currentindexPath)
    {
        int indexPath = currentindexPath;

        if (PathToFollow[indexPath] == PlayerVertice) return true;

        indexPath--;
        Vertice verticeToGo = default(Vertice);

        if (indexPath < 0) return false;

        foreach (var arista in vertice.AristasEntrantes)
        {
            if (arista.OriginVert == PathToFollow[indexPath].Vertice)
            {
                verticeToGo = arista.OriginVert;
            }
        }

        if (verticeToGo == default(Vertice))
        {
            return false;
        }
        return CheckCanArrive(verticeToGo, indexPath);
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
    }

    private void GraphTravel()
    {
        if (PathToFollow.Count > 0 && HoverVertice != null)
        {
            CanArrive = CheckCanArrive(HoverVertice.Vertice, PathToFollow.Count - 1);
        }

        if (PlayerVertice == HoverVertice)
        {
            CanArrive = true;
        }

        if (Input.GetKeyDown(KeyCode.V) && PathToFollow.Count > 0) // Se le envia al grafo 2 vertices a elección.
        {
            Vertice secondVert = PathToFollow.Count > 1 ? PathToFollow[1].Vertice : PathToFollow[0].Vertice;
            AddConnectionBetweenPoints(PathToFollow[0].Vertice, secondVert, 0);
            PathToFollow.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Se le envia al grafo 2 vertices aleatorios.
        {
            var VerticeA = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));
            var VerticeB = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));

            AddConnectionBetweenPoints(VerticeA, VerticeB, 0);
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
