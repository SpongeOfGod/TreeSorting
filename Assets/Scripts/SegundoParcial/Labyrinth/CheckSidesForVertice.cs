using UnityEngine;

public class CheckSidesForVertice : MonoBehaviour
{
    public bool CheckSidesOnce;
    int numberOfPossibilities = 1;
    [SerializeField] GraphManager graphManager;
    [SerializeField] float rayLength = 200f;
    [SerializeField] int connectionWeight = 1;
    void Update()
    {
        if (graphManager.Graph != null && (!CheckSidesOnce || graphManager.Maker)) 
        {
            CheckSidesOnce = true;
            numberOfPossibilities--;
            foreach (VisualVertice visualVertice in graphManager.VisualVertices) 
            {
                VisualVertice upVertice = SpawnRays(visualVertice, transform.up, rayLength);
                VisualVertice downVertice = SpawnRays(visualVertice, transform.up * -1, rayLength);
                VisualVertice leftVertice = SpawnRays(visualVertice, transform.right * -1, rayLength);
                VisualVertice rightVertice = SpawnRays(visualVertice, transform.right, rayLength);

                if (upVertice != null)
                    graphManager.AddConnectionBetweenPoints(visualVertice.Vertice, upVertice.Vertice, connectionWeight);

                if (downVertice != null)
                    graphManager.AddConnectionBetweenPoints(visualVertice.Vertice, downVertice.Vertice, connectionWeight);

                if (leftVertice != null)
                    graphManager.AddConnectionBetweenPoints(visualVertice.Vertice, leftVertice.Vertice, connectionWeight);

                if (rightVertice != null)
                    graphManager.AddConnectionBetweenPoints(visualVertice.Vertice, rightVertice.Vertice, connectionWeight);
            }
        }
    }

    public VisualVertice SpawnRays(VisualVertice origin, Vector3 direction, float maxDistance) 
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin.transform.position, direction, maxDistance);

        bool hitWall = false;
        VisualVertice verticeToConnect = null;
        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.transform.gameObject.CompareTag("Wall")) 
            {
                if (hit.transform.gameObject.TryGetComponent<VisualVertice>(out VisualVertice vertice)) 
                {
                    graphManager.Graph.RemoveConnection(origin.Vertice, vertice.Vertice);
                }
                hitWall = true;
            }

            if (hit.transform.gameObject.CompareTag("Vertice")) 
            {
                verticeToConnect = hit.transform.gameObject.GetComponent<VisualVertice>();
            }
        }

        if (!hitWall) 
        {
            Debug.DrawRay(origin.transform.position, direction * maxDistance, Color.red, 99f);
            return verticeToConnect;
        }
        else 
        {
            return null;
        }
    }
}
