using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ConjuntoEstatico/* : TDA<int>*/
{
    //public int indexTemp = 0;
    //public override void Add(int item) 
    //{
    //    if (!Contains(item) && indexTemp < ints.Length) 
    //    {
    //        ints[indexTemp] = item;
    //        indexTemp++;
    //    }

    //    if (indexTemp !>= ints.Length) 
    //    {
    //        indexTemp = 0;
    //    }
    //}

    //public int GetRandomNumber() 
    //{
    //    return Random.Range(0, Cardinality() - 1);
    //}
    //public override void Remove(int item) 
    //{
    //    int index = 0;

    //    if (!IsEmpty()) 
    //    {
    //        foreach (int i in ints) 
    //        {
    //            if(i == item) 
    //            {
    //                break;
    //            }
    //            index++;
    //        }

    //        if (index >= ints.Length) 
    //        {
    //            index = 0;
    //        } 

    //        (ints[index], ints[ints.Length - 1]) = (ints[ints.Length - 1], ints[index]);

    //        ints[ints.Length - 1] = 0;
    //    }
    //}
    //public override bool Contains(int item) 
    //{
    //    Debug.Log(ints.Contains(item));
    //    return ints.Contains(item);
    //}
    //public override int Show() 
    //{
    //    int index = Random.Range(0, ints.Length - 1);
    //    return ints[index];
    //}
    //public override int Cardinality() 
    //{
    //    int size = 0;
    //    foreach (int item in ints)
    //    {
    //        size++;
    //    }

    //    return size;
    //}
    //public override bool IsEmpty() 
    //{
    //    return ints.Length <= 0;
    //}

    //public override TDA<int> Union(TDA<int> otherSet)
    //{

    //    ConjuntoEstatico conjuntoNuevo = new ConjuntoEstatico();

    //    for (int i = 0; i < ints.Length; i++)
    //    {
    //        int itemA = ints[i];
    //        int itemB = 0;

    //        for (int j = i; j < otherSet.ints.Length; j++) 
    //        {
    //            itemB = otherSet.ints[j];
    //            if (itemA > itemB) 
    //            {
    //                conjuntoNuevo.Add(itemA);
    //                break;
    //            }
    //            else 
    //            {
    //                conjuntoNuevo.Add(itemB);
    //                break;
    //            }
    //        }
    //    }

    //    return conjuntoNuevo;
    //}
    //public override TDA<int> Intersection(TDA<int> otherSet)
    //{

    //    ConjuntoEstatico conjuntoNuevo = new ConjuntoEstatico();

    //    foreach (int item in ints)
    //    {
    //        if (otherSet.Contains(item))
    //        {
    //            conjuntoNuevo.Add(item);
    //        }
    //    }

    //    return conjuntoNuevo;
    //}
    //public override TDA<int> Difference(TDA<int> otherSet)
    //{
    //    ConjuntoEstatico conjuntoNuevo = new ConjuntoEstatico();

    //    foreach (int item in ints)
    //    {
    //        if (!otherSet.Contains(item))
    //        {
    //            conjuntoNuevo.Add(item);
    //        }
    //    }
    //    foreach (int item in otherSet.ints) 
    //    {
    //        if (!Contains(item))
    //        {
    //            conjuntoNuevo.Add(item);
    //        }
    //    }

    //    return conjuntoNuevo;
    //}
}
