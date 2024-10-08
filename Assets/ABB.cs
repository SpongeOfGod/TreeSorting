using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB : MonoBehaviour
{
    [SerializeField] List<int> dataList;
    [SerializeField] Nodo NodoPrefab;
    [SerializeField] Queue<Nodo> nodos = new Queue<Nodo>();
    bool rootAssigned = false;
    Nodo rootNodo;
    private void Awake()
    {

    }
    void Start()
    {
        for (int i = 0; i < dataList.Count; i++) 
        {
            if (rootAssigned) 
            {
                SearchInside(dataList[i], rootNodo, rootNodo.transform);
            }
            else 
            {
                rootAssigned = true;
                Nodo root = Instantiate(NodoPrefab, transform);
                root.AssignData(dataList[0], null, null);
                nodos.Enqueue(root);
                rootNodo = root;
            }
        }
    }

    void Update()
    {
        
    }


    private void SearchInside(int data, Nodo nodo, Transform parent) 
    {
        if(data < nodo.dato) 
        {
            if (nodo.izq == null) 
            {
                Debug.Log(parent);
                nodo.izq = Instantiate(NodoPrefab, parent);
                nodo.izq.AssignData(data, null, null);
                nodo.parent = parent;
                return;
            }
            else
            {
                SearchInside(data, nodo.izq, nodo.izq.transform);
                return;
            }        
        }

        if (data > nodo.dato)
        {
            if (nodo.der == null)
            {
                Debug.Log(parent);
                nodo.der = Instantiate(NodoPrefab, parent);
                nodo.der.AssignData(data, null, null);
                nodo.parent = parent;
                return;
            }
            else
            {
                SearchInside(data, nodo.der, nodo.der.transform);
                return;
            }
        }
    }
}
