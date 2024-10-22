using System.Linq;
using UnityEngine;

public class ConjuntoEstatico : ConjuntoTDA<int>
{
    public int indexTemp = 0;
    public override void Add(int item) 
    {
        if (!Contains(item) && indexTemp < ints.Length) 
        {
            ints[indexTemp] = item;
            indexTemp++;
        }

        if (indexTemp >= ints.Length) 
        {
            indexTemp = 0;
        }
    }

    public int GetRandomNumber() 
    {
        return Random.Range(0, Cardinality() - 1);
    }
    public override void Remove(int item) 
    {
        int index = 0;

        if (!isEmpty()) 
        {
            foreach (int i in ints) 
            {
                if(i == item) 
                {
                    break;
                }
                index++;
            }

            if (index >= ints.Length) 
            {
                index = 0;
            } 

            (ints[index], ints[ints.Length - 1]) = (ints[ints.Length - 1], ints[index]);

            ints[ints.Length - 1] = 0;
        }
    }
    public override bool Contains(int item) 
    {
        Debug.Log(ints.Contains(item));
        return ints.Contains(item);
    }
    public override int Show() 
    {
        int index = Random.Range(0, ints.Length - 1);
        return ints[index];
    }
    public override int Cardinality() 
    {
        int size = 0;
        foreach (int item in ints)
        {
            size++;
        }

        return size;
    }
    public override bool isEmpty() 
    {
        return ints.Length <= 0;
    }

    //public override ConjuntoTDA<int> Union(ConjuntoTDA<int> otherSet) 
    //{
    //    int[] tempArray = new int[otherSet.Cardinality()];

    //    int index = 0;

    //    while (index < tempArray.Length) 
    //    {
    //        ints.
    //        tempArray[index] = (otherSet.Show());
    //    }

    //    ints.Union(otherSet)
    //}
    //public override ConjuntoTDA<int> Intersection(ConjuntoTDA<int> otherSet) 
    //{

    //}
    //public override ConjuntoTDA<int> Difference(ConjuntoTDA<int> otherSet)
    //{
    //    ConjuntoEstatico conjuntoTemp = new ConjuntoEstatico();
    //    if (conjuntoTemp.Contains())
    //}
}
