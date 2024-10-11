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
    private void Awake()
    {
        preOrderButton.onClick.AddListener(SearchByPreOrder);
        inOrderButton.onClick.AddListener(SearchByInOrder);
        postOrderButton.onClick.AddListener(SearchByPostOrder);
    }

    void SearchByPreOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckPreOrder(treeSpawning.RootNodo);
        dataDisplay.text.Remove(dataDisplay.text.Length - 1);
    }

    void SearchByInOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckInOrder(treeSpawning.RootNodo);
        dataDisplay.text.Remove(dataDisplay.text.Length - 1);
    }

    void SearchByPostOrder() 
    {
        dataDisplay.text = string.Empty;
        CheckPostOrder(treeSpawning.RootNodo);
        dataDisplay.text.Remove(dataDisplay.text.Length - 1);
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
