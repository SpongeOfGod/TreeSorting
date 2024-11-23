using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer lineInstance;
    private Transform parentTransform;
    private GameObject line;

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
                UpdateLineName();
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
                Destroy(this);
                return;
            }

            UpdateLinePositions();
            UpdateLineName();
        }
        else if (parentTransform.gameObject.name == "Parent")
        {
            Destroy(lineInstance.gameObject);
            Destroy(this);
            return;
        }
        UpdateLinePositions();
        UpdateLineName();
    }

    void UpdateLinePositions()
    {
        if (lineInstance != null && parentTransform != null)
        {
            if (lineInstance.positionCount != 2)
            {
                lineInstance.positionCount = 2;
            }
            lineInstance.SetPosition(0, parentTransform.GetComponent<RectTransform>().position);
            lineInstance.SetPosition(1, transform.GetComponent<RectTransform>().position);
        }
    }

    void UpdateLineName()
    {
        if (lineInstance != null)
        {
            string name1 = parentTransform.gameObject.name;
            string name2 = transform.gameObject.name;
            lineInstance.gameObject.name = $"Line ({name1} -> {name2})";
        }
    }
}
