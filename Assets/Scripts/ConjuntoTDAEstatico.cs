using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConjuntoTDAEstatico<T>
{
    public T[] ints = new T[10];

    public abstract void Add(T item);
    public abstract void Remove(T item);
    public abstract bool Contains(T item);
    public abstract T Show();
    public abstract int Cardinality();
    public abstract bool isEmpty();

    public abstract ConjuntoTDAEstatico<T> Union(ConjuntoTDAEstatico<T> otherSet);
    public abstract ConjuntoTDAEstatico<T> Intersection(ConjuntoTDAEstatico<T> otherSet);
    public abstract ConjuntoTDAEstatico<T> Difference(ConjuntoTDAEstatico<T> otherSet);
}
