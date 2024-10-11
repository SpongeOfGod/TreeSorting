using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public Nodo izq, der;
    public int dato;
    public Nodo parent;
    [SerializeField] private TextMeshProUGUI dataText;
    private void Start()
    {
        dataText = GetComponent<TextMeshProUGUI>();
    }
    public Nodo(Nodo izq, Nodo der, int dato)
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        parent = null;
    }

    public void AssignData(int dato, Nodo izq, Nodo der, Nodo parent) 
    {
        this.izq = izq;
        this.der = der;
        this.dato = dato;
        this.parent = parent;
        dataText.text = dato.ToString();
    }
}
