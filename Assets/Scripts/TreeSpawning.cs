using EnClase;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawning : MonoBehaviour
{
    [SerializeField] List<int> dataList;
    [SerializeField] List<Nodo> nodeList = new List<Nodo>();
    [SerializeField] VisualNode NodoPrefab;
    [SerializeField] Queue<Nodo> nodos = new Queue<Nodo>();
    bool rootAssigned = false;
    Nodo rootNodo;
    public Nodo RootNodo => rootNodo;
    [SerializeField] float posX, posXm;
    [SerializeField] float posY;
    [SerializeField] int depth = 0;
    [SerializeField] float offsetMultiplier;
    RotationManager rotationManager;

    private void Awake()
    {
        rotationManager = GetComponent<RotationManager>();
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
                VisualNode visualnode = Instantiate(NodoPrefab, transform);
                Nodo root = visualnode.Nodo;
                visualnode.gameObject.name = "Root";
                root.AssignData(dataList[0], null, null, null, 0);
                nodos.Enqueue(root);
                rootNodo = root;
            }
        }
        rotationManager.rotationOcurred = true;
        while (rotationManager.rotationOcurred) 
        {
            Debug.Log("Rotation");
            rotationManager.rotationOcurred = false;

            foreach (Nodo node in nodeList) 
            {
                rotationManager.CheckRotations(node);
            }
        }
    }

    private void SearchInside(int data, Nodo nodo) 
    {
        if(data < nodo.dato) 
        {
            //Debug.Log($"{data} < {nodo.dato}");
            if (nodo.izq == null) 
            {
                VisualNode visualnode = Instantiate(NodoPrefab, nodo.visualNode.transform);
                nodo.izq = visualnode.Nodo;
                visualnode.name = data.ToString();
                nodo.izq.AssignData(data, null, null, nodo, depth);
                visualnode.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX * (offsetMultiplier/ (depth + 1)), posY);
                nodeList.Add(nodo.izq);
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
            //Debug.Log($"{data} > {nodo.dato}");
            if (nodo.der == null)
            {
                VisualNode visualnode = Instantiate(NodoPrefab, nodo.visualNode.transform);

                nodo.der = visualnode.Nodo;
                visualnode.name = data.ToString();
                nodo.der.AssignData(data, null, null, nodo, depth);
                visualnode.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXm * (offsetMultiplier / (depth + 1)), posY);
                nodeList.Add(nodo.der);
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
