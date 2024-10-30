using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TDA<T>
{
    public abstract bool Add(T element);
    public abstract void Remove(T element);
    public abstract bool Contains(T elemnt);
    public abstract T Show();
    public abstract int Cardinality();
    public abstract bool IsEmpty();
    public abstract T GetElement(int index);

    public abstract TDA<T> Union(TDA<T> otherSet);
    public abstract TDA<T> Intersection(TDA<T> otherSet);
    public abstract TDA<T> Difference(TDA<T> otherSet);
}
