using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph : MonoBehaviour
{
    [SerializeField] List<VerticeVisual> VerticeVisuals = new List<VerticeVisual>();
    [SerializeField] Dictionary<Vertice, List<(Vertice, int)>> adyacentList = new Dictionary<Vertice, List<(Vertice, int)>>();
    [SerializeField] SerializableDictionary<Arista, List<(Arista, int)>> SerializableDictionary;
    private void Start()
    {
        SerializableDictionary = new SerializableDictionary<Arista, List<(Arista, int)>>();
        //SerializableDictionary.ToDictionary()
    }
    public void InitNodos()
    {
        for (int i = 0; i < 12; i ++)
        {
            //adyacentList.Add(, i);
        }
    }
}
