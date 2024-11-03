using System.Collections.Generic;
using UnityEngine;

public class DynamicGraph<T> : GraphTDA<T> // Grafo dinámico que hereda del conjunto grafo.
{
    public DynamicTDA<T> verticesData;
    GraphManager spawnGraph;
    [SerializeField] Dictionary<T, List<(T, Arista)>> adyacentList = new Dictionary<T, List<(T, Arista)>>();

    // ↑
    // Diccionario de vertices, cada uno contiene listas de (vertices, y aristas), simulando filas y columnas.

    public override void InitializeGraph(GraphManager spawnGraph)
    {
        verticesData = new DynamicTDA<T>(); //Conjunto dinamico de vertices que guarda los vertices creados.
        this.spawnGraph = spawnGraph;
    }
    public override bool AddVertice(T element) // Ve si el vertice recibido ya se encuentra guardado.
    {
        if (verticesData.Contains(element))
            return false;

        verticesData.Add(element);
        adyacentList.TryAdd(element, new List<(T, Arista)>(1));
        return true;
    }
    public override bool RemoveVertice(T element) // Ve si el vertice recibido se encuentra en el conjunto, y lo borra.
    {
        if (verticesData.Contains(element))
            for (int i = 0; i < verticesData.Cardinality(); i++)
                if (Equals(verticesData.GetElement(i), element))
                {
                    verticesData.Remove(element);
                    adyacentList.Remove(element);
                    return true;
                }
        return false;
    }
    public override bool VerticeExists(T element) // Ve si el vertice recibido se encuentra en el conjunto.
    { 
        for (int i = 0; i < verticesData.Cardinality(); i++)
            if (Equals(verticesData.GetElement(i), element))
                return true;

        return false;
    }
    public override TDA<T> Vertices() // Devuelve el conjunto de vertices.
    {
        return verticesData;
    }
    public override bool AddConnection(T verticeOrigen, T VerticeDestino, int weight) // Genera una conexión de ida entre un vertice origen y un vertice destino.
    {
        // Ve si el diccionario contiene los elementos necesarios para agregar las conexiones.
         if (adyacentList.TryGetValue(verticeOrigen, out List<(T, Arista)> listA) && adyacentList.TryGetValue(VerticeDestino, out List<(T, Arista)> listB))
         {
            bool DoesNotContain = true;
            foreach (var item in listA) // Inspecciona si el verticeA ya contiene una conexión con el verticeB.
            {
                if (Equals(item.Item1, VerticeDestino) && !Equals(item.Item2, null)) 
                {
                    DoesNotContain = false;
                }
            }
            if (DoesNotContain) 
            {
                Arista connectionBetween = null;
                if (weight == 0) 
                {
                    connectionBetween = new Arista(Random.Range(1, 100)); // Arista: Contiene un punto de origen y un punto final.
                }
                else 
                {
                    connectionBetween = new Arista(weight); // Arista: Contiene un punto de origen y un punto final.
                }
                connectionBetween.SetPoints(verticeOrigen, VerticeDestino); //Asigna el punto inicial y final de la arista respectivamente.

                listA.Add((VerticeDestino, connectionBetween)); // Añade la conexión al VerticeA.
                return true;
            }
            else 
            {
                return false;
            }
         }
        return false;
    }
    public override bool RemoveConnection(T verticeOrigen, T verticeDestino) // Si el vertice origen cuenta con una conexión de ida, si la hay, la borra.
    {
        if (adyacentList.TryGetValue(verticeOrigen, out List<(T, Arista)> listA) && adyacentList.TryGetValue(verticeDestino, out List<(T, Arista)> listB))
        {
            foreach (var vertice in listA) 
            {
                if (Equals(vertice.Item1, verticeDestino))
                    listA.Remove(vertice);
            }
            return true;
        }
        return false;
    }
    public override bool ConnectionExists(T verticeA, T verticeB) // Comprueba que el vertice origen cuente con una conexión de ida.
    {
        if (adyacentList.TryGetValue(verticeA, out List<(T, Arista)> listA) && adyacentList.TryGetValue(verticeB, out List<(T, Arista)> listB))
        {
            foreach (var vertice in listA)
            {
                if (Equals(vertice.Item1, verticeB))
                    if(vertice.Item2 != null)
                        return true;
                    else
                        return false;
            }
        }
        return false;
    }
    public override int ConnectionWeight(T verticeA, T verticeB) // Devuelve el peso de la conexión de ida entre un vertice origen y un vertice destino.
    {
        if (adyacentList.TryGetValue(verticeA, out List<(T, Arista)> listA) && adyacentList.TryGetValue(verticeB, out List<(T, Arista)> listB))
        {
            foreach (var vertice in listA)
            {
                if (Equals(vertice.Item1, verticeB))
                    if (vertice.Item2 != null)
                        return vertice.Item2.Weight;
            }
        }
        return 0;
    }
}
