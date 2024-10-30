using UnityEngine;

public class StaticTDA<T> : TDA<T>
{
    public T[] datas;
    public int maxSize;
    public int currentSize = 0;

    public StaticTDA(int size)
    {
        maxSize = size;
        datas = new T[maxSize];
    }

    public override int Cardinality() => currentSize;

    public override bool Add(T element)
    {
        if (Contains(element) || currentSize >= maxSize)
            return false;

        datas[currentSize] = element;
        currentSize++;
        return true;
    }

    public override void Remove(T element) 
    {
            
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
        for (int i = 0; i < currentSize; i++)
            if (Equals(datas[i], element))
                return true;

        return false;
    }

    public override TDA<T> Intersection(TDA<T> other)
    {
        StaticTDA<T> conjuntoNuevo = new StaticTDA<T>(maxSize);

        for (int i = 0; i < currentSize; i++)
            if (other.Contains(datas[i]))
                conjuntoNuevo.Add(datas[i]);

        return conjuntoNuevo;
    }


    public override TDA<T> Union(TDA<T> other)
    {
        StaticTDA<T> conjuntoNuevo = new StaticTDA<T>(maxSize);

        for (int i = 0; i < currentSize; i++)
            conjuntoNuevo.Add(datas[i]);

        for (int i = 0; i < other.Cardinality(); i++)
            conjuntoNuevo.Add(other.GetElement(i));

        return conjuntoNuevo;
    }

    public override TDA<T> Difference(TDA<T> other)
    {
        StaticTDA<T> conjuntoNuevo = new StaticTDA<T>(maxSize);

        for (int i = 0; i < currentSize; i++)
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
    