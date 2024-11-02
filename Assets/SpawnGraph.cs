using UnityEngine;

public class SpawnGraph : MonoBehaviour
{
    [SerializeField] VisualVertice[] visualVertices;
    DynamicGraph<Vertice> Graph;
    void Start()
    {
        Graph = new DynamicGraph<Vertice>();

        Graph.InitializeGraph();

        foreach (var vertice in visualVertices) 
        {
            Graph.AddVertice(vertice.Vertice);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            var VerticeA = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));
            var VerticeB = Graph.verticesData.GetElement(Random.Range(0, Graph.verticesData.Cardinality()));

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
}
