using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private float Seconds = 1f;
    [SerializeField] float leftPosX, rightPosX;
    [SerializeField] float posY;

    [SerializeField] float offsetMultiplier;
    private Nodo parent;
    [SerializeField] Nodo p;
    [SerializeField] Nodo q;
    public bool rotationOcurred;

    bool doubleRotation;

    int CheckDepth(Nodo nodo)
    {
        if (nodo == null) return -1;

        return (1 + Math.Max(CheckDepth(nodo.izq), CheckDepth(nodo.der)));
    }

    private void Update()
    {
        
    }
    public void CheckRotations (Nodo nodo)
    {
        if (nodo == null) return;
        ChequearDerecha(nodo);
        ChequearIzquierda(nodo);
    }

    void ChequearIzquierda(Nodo nodo)
    {
        p = nodo;
        q = p.der;

        int balance = CheckDepth(nodo.der) - CheckDepth(nodo.izq);

        Debug.Log(balance);

        int balanceSubRight = 0;
        if (nodo.der != null)
        {
            balanceSubRight = CheckDepth(nodo.der.der) - CheckDepth(nodo.der.izq);
        }

        if (balance > 1 && balanceSubRight >= 0)
        {
            Debug.Log("Rotación Simple Izquierda");
            rotationOcurred = true;
            RotacionIzquierda(q, p);
        }
        else if (balance > 1 && balanceSubRight < 0)
        {
            Debug.Log("Doble Rotación Izquierda");
            rotationOcurred = true;
            RotacionDerecha(q.izq, q);
            RotacionIzquierda(q, p);
            //StartCoroutine(DelayOrderLeft(p.der, p));
        }
    }

    void ChequearDerecha(Nodo nodo)
    {
        int balance = CheckDepth(nodo.der) - CheckDepth(nodo.izq);

        int balanceSubLeft = 0;

        if (nodo.izq != null)
        {
            balanceSubLeft = CheckDepth(nodo.izq.der) - CheckDepth(nodo.izq.izq);
        }

        Debug.Log(balance);

        p = nodo;
        q = p.izq;


        if (balance < -1 && balanceSubLeft <= 0)
        {
            Debug.Log("Rotación Simple Derecha");
            rotationOcurred = true;
            RotacionDerecha(q, p);
        }
        else if (balance < -1 && balanceSubLeft > 0)
        {
            Debug.Log("Doble Rotación Derecha");
            rotationOcurred = true;
            RotacionIzquierda(q.der, q);
            RotacionDerecha(q, p);
            //StartCoroutine(DelayOrderRight(p.izq, p));
        }
    }

    public void EraseReferenceOfNode(Nodo parentNode, Nodo nodo) 
    {
        if (parentNode.izq == nodo)
        {
            parentNode.izq = null;
        }

        if (parentNode.der == nodo)
        {
            parentNode.der = null;
        }
    }
    public void newPos(Nodo nodoGrab, float posX, float depth) 
    {
        if (nodoGrab != null)
        {
            nodoGrab.visualNode.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX * (offsetMultiplier / (depth + 1)), posY);
        }
    }

    public void RotacionDerecha(Nodo tempQ, Nodo tempP)
    {
        bool assigned = false;

        if (tempQ.der != null) // Si el nodo derecho de Q no es nulo, se lo asigna a P como nodo izquierdo
        {
            ReOrderNodeToLeft(tempQ, tempQ.der);
        }

        if (tempP.parent.der == tempP && !assigned)
        {
            ReOrderNodeToRight(tempP, tempQ);
            assigned = true;
        }

        if (tempP.parent.izq == tempP && !assigned)
        {
            ReOrderNodeToLeft(tempP, tempQ);
            assigned = true;
        }

        tempQ.der = tempP;
        tempQ.der.parent = tempQ;
        tempQ.der.visualNode.transform.parent = tempQ.visualNode.transform;
        tempQ.der.depth = tempQ.depth + 1;
        
        if (tempQ.izq != null) 
        {
            tempQ.izq.depth = tempQ.depth + 1;
        }
        newPos(tempQ.der, rightPosX, tempQ.der.depth);

        EraseReferenceOfNode(tempQ.der, tempQ);

        q = tempQ;
    }

    public void RotacionIzquierda(Nodo tempQ, Nodo tempP) 
    {
        bool assigned = false;

        if (tempQ.izq != null)
        {
            ReOrderNodeToRight(tempQ, tempQ.izq);
        }

        if (tempP.parent.izq == tempP && !assigned)
        {
            ReOrderNodeToLeft(tempP, tempQ);
            assigned = true;
        }

        if (tempP.parent.der == tempP && !assigned)
        {
            ReOrderNodeToRight(tempP, tempQ);
            assigned = true;
        }

        tempQ.izq = tempP;
        tempQ.izq.parent = tempQ;
        tempQ.izq.visualNode.transform.parent = tempQ.visualNode.transform;
        tempQ.izq.depth = tempQ.depth + 1;
        newPos(tempQ.izq, leftPosX, tempQ.izq.depth);


        if (tempQ.der != null)
        {
            tempQ.der.depth = tempQ.depth + 1;
        }
        EraseReferenceOfNode(tempQ.izq, tempQ);

        q = tempQ;
    }

    public void ReOrderNodeToRight(Nodo parentNode, Nodo node) 
    {
        parentNode.parent.der = node;
        parentNode.parent.der.parent = parentNode.parent;
        parentNode.parent.der.visualNode.transform.parent = parentNode.parent.visualNode.transform;
        parentNode.parent.der.depth = parentNode.parent.depth + 1;
        newPos(parentNode.parent.der, rightPosX, parentNode.parent.der.depth);
    }

    public void ReOrderNodeToLeft(Nodo parentNode, Nodo node)
    {
        parentNode.parent.izq = node;
        parentNode.parent.izq.visualNode.transform.parent = parentNode.parent.visualNode.transform;
        parentNode.parent.izq.depth = parentNode.parent.depth + 1;
        newPos(parentNode.parent.izq, leftPosX, parentNode.parent.izq.depth);
    }
    IEnumerator DelayOrderLeft(Nodo q, Nodo p) 
    {
        doubleRotation = true;
        yield return new WaitForSeconds(Seconds);
        RotacionIzquierda(q, p);
        doubleRotation = false;
    }

    IEnumerator DelayOrderRight(Nodo q, Nodo p)
    {
        doubleRotation = true;
        yield return new WaitForSeconds(Seconds);
        RotacionDerecha(q, p);
        doubleRotation = false;
    }
}
