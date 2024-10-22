using UnityEngine;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public List<Transform> points = new List<Transform>();

    private LineRenderer lineInstance;
    private GameObject parentObject = null;

    void Start()
    {
        if (linePrefab != null)
        {
            GameObject lineRendererObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            lineInstance = lineRendererObject.GetComponent<LineRenderer>();
        }
    }
    void Update()
    {
        if (parentObject != gameObject.transform.parent && lineInstance != null)
        {
            parentObject = gameObject.transform.parent?.gameObject;
            lineInstance.positionCount = points.Count;
            points.Clear();

            points.Add(parentObject.transform);
            points.Add(gameObject.transform);

            for (int i = 0; i < points.Count; i++)
            {
                lineInstance.SetPosition(i, points[i].position);
            }
        }
        else
        {
            if (parentObject == null)
            {
                Debug.Log(gameObject + " es la raíz o sucedió un error.");
                Destroy(this);
            }
        }
    }
}