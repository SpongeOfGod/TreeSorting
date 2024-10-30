using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private VisualNode visualNode;
    [SerializeField] private Transform parent;
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

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public VisualNode GetNodeInstance() 
    {
        return Instantiate(visualNode, parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
