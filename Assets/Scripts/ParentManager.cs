using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentManager : MonoBehaviour
{
    public static ParentManager Instance;
    public Transform Parent;
    public VisualNode visualNode;
    public VisualVertice verticeVisual;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        if (Instance != this) 
        {
            Destroy(gameObject);
        }
    }

    public VisualNode GetNodeInstance()
    {
        return Instantiate(visualNode, Parent);
    }

    public VisualVertice GetVerticeInstance() 
    {
        return Instantiate(verticeVisual, Parent);
    }
}
