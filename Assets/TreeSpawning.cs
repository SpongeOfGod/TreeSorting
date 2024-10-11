using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawning : MonoBehaviour
{
    [SerializeField] List<int> dataList;
    [SerializeField] Nodo NodoPrefab;
    [SerializeField] Queue<Nodo> nodos = new Queue<Nodo>();
    bool rootAssigned = false;
    Nodo rootNodo;
    public Nodo RootNodo => rootNodo;
    [SerializeField] float posX, posXm;
    [SerializeField] float posY;
    [SerializeField] int depth = 0;
    [SerializeField] float offsetMultiplier;

    private void Awake()
    {

    }
    void Start()
    {
        for (int i = 0; i < dataList.Count; i++) 
        {
            if (rootAssigned) 
            {
                SearchInside(dataList[i], rootNodo);
                depth = 0;
            }
            else 
            {
                rootAssigned = true;
                Nodo root = Instantiate(NodoPrefab, transform);
                root.AssignData(dataList[0], null, null, null);
                nodos.Enqueue(root);
                rootNodo = root;
            }
        }
    }

    private void SearchInside(int data, Nodo nodo) 
    {
        if(data < nodo.dato) 
        {
            Debug.Log($"{data} < {nodo.dato}");
            if (nodo.izq == null) 
            {
                nodo.izq = Instantiate(NodoPrefab, nodo.transform);
                nodo.izq.AssignData(data, null, null, nodo);
                nodo.izq.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX * (offsetMultiplier/ (depth + 1)), posY);
                ////nodo.izq.transform.position = new Vector2(posX - depth, posY);
                return;
            }
            else
            {
                depth++;
                SearchInside(data, nodo.izq);
                return;
            }        
        }

        if (data > nodo.dato)
        {
            Debug.Log($"{data} > {nodo.dato}");
            if (nodo.der == null)
            {
                nodo.der = Instantiate(NodoPrefab, nodo.transform);
                nodo.der.AssignData(data, null, null, nodo);
                nodo.der.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXm * (offsetMultiplier / (depth + 1)), posY);
                return;
            }
            else
            {
                depth++;
                SearchInside(data, nodo.der);
                return;
            }
        }
    }
}
