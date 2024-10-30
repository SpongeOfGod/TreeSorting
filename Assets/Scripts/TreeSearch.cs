using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EnClase;

public class TreeSearch : MonoBehaviour
{
    public TreeStructure testSpawnTree;
    public SpawnABBTree SpawnABBTree;
    public SpawnAVLTree SpawnAVLTree;
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

        testSpawnTree = SpawnABBTree == null ? SpawnAVLTree.AVLTree : SpawnABBTree.ABBTree;
    }

    private void Start()
    {
        int depth = CheckDepth(testSpawnTree.root);
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
        CheckPreOrder(testSpawnTree.root);
        string newText = dataDisplay.text.Remove(dataDisplay.text.Length - 1);
        dataDisplay.text = newText;
    }

    void SearchByInOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckInOrder(testSpawnTree.root);
        string newText = dataDisplay.text.Remove(dataDisplay.text.Length - 1);
        dataDisplay.text = newText;
    }

    void SearchByPostOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckPostOrder(testSpawnTree.root);
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
