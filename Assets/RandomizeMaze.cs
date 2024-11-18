using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizeMaze : MonoBehaviour
{
    public GraphManager manager;
    public void Randomize() 
    {
        string input = string.Empty;
        manager.PlayerVertice = null;
        manager.ExitVertice = null;
        foreach (var vertice in manager.VisualVertices)
        {
            switch(Random.Range(0, 4)) 
            {
                case 0:
                    if (manager.PlayerVertice == null && vertice != manager.ExitVertice)
                        input = "Player";
                    else
                        input = "Vertice";
                    break;

                case 1:
                    if (manager.ExitVertice == null && vertice != manager.PlayerVertice)
                        input = "Exit";
                    else
                        input = "Wall";
                    break;

                case 2:
                    input = "Vertice";
                    break;

                case 3:
                    input = "Wall";
                    break;
            }
            vertice.ChangeVerticeByType(input);
        }
    }
}
