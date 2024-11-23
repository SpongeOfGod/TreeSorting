using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTagManager : MonoBehaviour
{
    public static MazeTagManager Instance;
    public string Input;
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else if (Instance != this) 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Input = "Player";
    }
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1) || UnityEngine.Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            Input = "Player";
            Debug.Log($"Now you're selecting {Input}");
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad2) || UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            Input = "Exit";
            Debug.Log($"Now you're selecting {Input}");
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad3) || UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            Input = "Vertice";
            Debug.Log($"Now you're selecting {Input}");
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad4) || UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
        {
            Input = "Wall";
            Debug.Log($"Now you're selecting {Input}");
        }
    }
}
