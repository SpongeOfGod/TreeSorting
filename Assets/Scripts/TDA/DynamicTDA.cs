using System.Collections.Generic;
using UnityEngine;

public class DynamicTDA<T> : TDA<T>
{
    List<T> datas = new List<T>();

    public override int Cardinality() => datas.Count;

    public override bool Add(T element)
    {
        if (Contains(element))
            return false;

        datas.Add(element);
        return true;
    }

    public override void Remove(T element)
    {
        if (Contains(element))
            for (int i = 0; i < datas.Count; i++)
                if (Equals(datas[i], element)) 
                {
                    datas.RemoveAt(i);
                }
    }


    public override bool IsEmpty()
    {
        if (Cardinality() < 1) return true;

        return false;
    }

    public override T Show()
    {
        int index = Random.Range(0, Cardinality());
        return datas[index];
    }
    public override bool Contains(T element)
    {
        for (int i = 0; i < datas.Count; i++)
            if (Equals(datas[i], element))
                return true;

        return false;
    }

    public override TDA<T> Intersection(TDA<T> other)
    {
        DynamicTDA<T> conjuntoNuevo = new DynamicTDA<T>();

        for (int i = 0; i < datas.Count; i++)
            if (other.Contains(datas[i]))
                conjuntoNuevo.Add(datas[i]);

        return conjuntoNuevo;
    }

    public override TDA<T> Union(TDA<T> other)
    {
        DynamicTDA<T> conjuntoNuevo = new DynamicTDA<T>();

        for (int i = 0; i < datas.Count; i++)
            conjuntoNuevo.Add(datas[i]);

        for (int i = 0; i < other.Cardinality(); i++)
            conjuntoNuevo.Add(other.GetElement(i));

        return conjuntoNuevo;
    }

    public override TDA<T> Difference(TDA<T> other)
    {
        DynamicTDA<T> conjuntoNuevo = new DynamicTDA<T>();

        for (int i = 0; i < datas.Count; i++)
            if (!other.Contains(datas[i]))
                conjuntoNuevo.Add(datas[i]);

        for (int i = 0; i < other.Cardinality(); i++)
            if (!Contains(other.GetElement(i)))
                conjuntoNuevo.Add(other.GetElement(i));

        return conjuntoNuevo;
    }

    public override T GetElement(int index)
    {
        if (index < Cardinality())
            return datas[index];

        return default;
    }
}
    