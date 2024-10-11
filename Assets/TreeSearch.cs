using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeSearch : MonoBehaviour
{
    public TreeSpawning treeSpawning;
    public Button preOrderButton;
    public Button inOrderButton;
    public Button postOrderButton;
    public TextMeshProUGUI dataDisplay;
    public TextMeshProUGUI depthDisplay;
    private void Awake()
    {
        preOrderButton.onClick.AddListener(SearchByPreOrder);
        inOrderButton.onClick.AddListener(SearchByInOrder);
        postOrderButton.onClick.AddListener(SearchByPostOrder);
    }

    private void Start()
    {
        int depth = CheckDepth(treeSpawning.RootNodo);
        depthDisplay.text = depth.ToString();    
    }

    int CheckDepth(Nodo nodo) 
    {
        if (nodo == null) return -1;

        return (1 + Math.Max(CheckDepth(nodo.izq), CheckDepth(nodo.der)));
    }
    void SearchByPreOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckPreOrder(treeSpawning.RootNodo);
        string newText = dataDisplay.text.Remove(dataDisplay.text.Length - 1);
        dataDisplay.text = newText;
    }

    void SearchByInOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckInOrder(treeSpawning.RootNodo);
        string newText = dataDisplay.text.Remove(dataDisplay.text.Length - 1);
        dataDisplay.text = newText;
    }

    void SearchByPostOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckPostOrder(treeSpawning.RootNodo);
        string newText = dataDisplay.text.Remove(dataDisplay.text.Length - 1);
        dataDisplay.text = newText;
    }

    void CheckPreOrder(Nodo nodo) 
    {
        if (nodo == null) return;

        dataDisplay.text += $" {nodo.dato},";
        CheckPreOrder(nodo.izq);
        CheckPreOrder(nodo.der);
    }

    void CheckInOrder(Nodo nodo) 
    {
        if (nodo == null) return;

        CheckInOrder(nodo.izq);
        dataDisplay.text += $" {nodo.dato},";
        CheckInOrder(nodo.der);
    }
    void CheckPostOrder(Nodo nodo) 
    {
        if (nodo == null) return;
        CheckPostOrder(nodo.izq);
        CheckPostOrder(nodo.der);
        dataDisplay.text += $" {nodo.dato},";
    }

}
