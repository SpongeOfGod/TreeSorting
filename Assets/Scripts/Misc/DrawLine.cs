using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer lineInstance;
    private Transform parentTransform;

    void Start()
    {
        if (linePrefab != null)
        {
            GameObject lineRendererObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
            lineInstance = lineRendererObject.GetComponent<LineRenderer>();
            parentTransform = transform.parent;

            if (lineInstance != null)
            {
                lineInstance.positionCount = 2;
                UpdateLinePositions();
            }
        }
    }

    void Update()
    {
        if (transform.parent != parentTransform)
        {
            parentTransform = transform.parent;

            if (parentTransform == null)
            {
                Debug.LogWarning(gameObject + " es la raíz o no tiene padre.");
                Destroy(this);
                return;
            }

            UpdateLinePositions();
        }
    }

    void UpdateLinePositions()
    {
        if (lineInstance != null && parentTransform != null)
        {
            if (lineInstance.positionCount != 2)
            {
                lineInstance.positionCount = 2;
            }
            lineInstance.SetPosition(0, parentTransform.position);
            lineInstance.SetPosition(1, transform.position);
        }
    }
}
