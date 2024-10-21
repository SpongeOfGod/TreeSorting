using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConjuntoTDA<T> : MonoBehaviour
{
    public abstract void Add(T item);
    public abstract void Remove(T item);
    public abstract bool Contains(T item);
    public abstract T Show();
    public abstract int Cardinality();
    public abstract bool isEmpty();

    //public abstract ConjuntoTDA<T> Union(ConjuntoTDA<T> otherSet);
    //public abstract ConjuntoTDA<T> Intersection(ConjuntoTDA<T> otherSet);
    //public abstract ConjuntoTDA<T> Difference(ConjuntoTDA<T> otherSet);
}
