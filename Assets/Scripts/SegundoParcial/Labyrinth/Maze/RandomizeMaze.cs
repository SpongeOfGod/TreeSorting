using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class RandomizeMaze : MonoBehaviour
{
    public GraphManager manager;
    public PathSearch pathSearch;
    public bool isRandomizing;

    public void Randomize() 
    {
        isRandomizing = false;
        manager.CanArrive = false;
    }
    public void Update()
    {
        if (!isRandomizing && !manager.CanArrive) 
        {
            StartCoroutine(RandomizeDelay());
        } 
        else if (manager.CanArrive) 
        {
            isRandomizing = true;
        }
    }

    IEnumerator RandomizeDelay() 
    {
        isRandomizing = true;
        manager.ReOrder();
        string input = string.Empty;
        manager.PlayerVertice = null;
        manager.ExitVertice = null;

        foreach (var vertice in manager.VisualVertices)
        {
            switch (Random.Range(0, 4))
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
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.4f);
        isRandomizing = false;
    }
}
