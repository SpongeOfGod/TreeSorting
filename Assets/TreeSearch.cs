using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSearch : MonoBehaviour
{
    public TreeSpawning treeSpawning;
    public Button preOrderButton;

    private void Awake()
    {
        preOrderButton.onClick.AddListener(SearchByPreOrder);
    }

    void SearchByPreOrder() 
    {
        checkPreOrder(treeSpawning.RootNodo);
    }

    void checkPreOrder(Nodo nodo) 
    {
        if (nodo == null) return;

        Debug.Log(nodo.dato);
        checkPreOrder(nodo.izq);
        checkPreOrder(nodo.der);
    }
}
