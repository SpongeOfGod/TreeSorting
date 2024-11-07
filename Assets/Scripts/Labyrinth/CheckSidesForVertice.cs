using UnityEngine;

public class CheckSidesForVertice : MonoBehaviour
{
    bool once;
    int numberOfPossibilities = 1;
    [SerializeField] GraphManager graphManager;
    [SerializeField] float rayLength = 200f;
    [SerializeField] int connectionWeight = 1;
    void Update()
    {
        if (!once) 
        {
            once = true;
            numberOfPossibilities--;
            foreach (VisualVertice visualVertice in graphManager.VisualVertices) 
            {
                VisualVertice upVertice = SpawnRays(visualVertice.transform.position, transform.up, rayLength);
                VisualVertice downVertice = SpawnRays(visualVertice.transform.position, transform.up * -1, rayLength);
                VisualVertice leftVertice = SpawnRays(visualVertice.transform.position, transform.right * -1, rayLength);
                VisualVertice rightVertice = SpawnRays(visualVertice.transform.position, transform.right, rayLength);

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

    public VisualVertice SpawnRays(Vector3 origin, Vector3 direction, float maxDistance) 
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, maxDistance);

        bool hitWall = false;
        VisualVertice verticeToConnect = null;
        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.transform.gameObject.CompareTag("Wall")) 
            {
                hitWall = true;
            }

            if (hit.transform.gameObject.CompareTag("Vertice")) 
            {
                verticeToConnect = hit.transform.gameObject.GetComponent<VisualVertice>();
            }
        }

        if (!hitWall) 
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.red, 99f);
            return verticeToConnect;
        }
        else 
        {
            return null;
        }
    }
}
