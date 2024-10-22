using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> : MonoBehaviour
{
    [SerializeField] Dictionary<T, List<(T, int)>> adyacentList = new Dictionary<T, List<(T, int)>>();
}
