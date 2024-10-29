using System.Linq;
using UnityEngine;

public class ConjuntoDinamico : ConjuntoTDA<int> // Hacerlo Genérico
{
    public int indexTemp = 0;

    public override void Add(int item)
    {
        if (!Contains(item) && (indexTemp < 10 || isDinamic))
        {
            if (indexTemp < 10) 
            {
                ints[indexTemp] = item;
                intList = ints.ToList();
                indexTemp++;

                if (intList.Count - 1 >= indexTemp) 
                {
                    intList[indexTemp] = item;
                }
            }
            else if (indexTemp >= 10 && isDinamic) 
            {
                intList.Add(item);
                ints = intList.ToArray();
            }
        }

        if (indexTemp! >= ints.Length && !isDinamic)
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
                if (i == item)
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
            intList = ints.ToList();
        }
    }
    public override bool Contains(int item)
    {
        bool contain = intList.Contains(item);
        return contain;
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

    public override ConjuntoTDA<int> Union(ConjuntoTDA<int> otherSet)
    {
        ConjuntoDinamico conjuntoNuevo = new ConjuntoDinamico();
        conjuntoNuevo.isDinamic = isDinamic;

        for (int i = 0; i < otherSet.intList.Count; i++)
        {
            if (!conjuntoNuevo.isDinamic && i >= 10) 
            {
                break;
            }

            int itemA = otherSet.intList[i];
            int itemB = 0;

            for (int j = i; j < intList.Count; j++)
            {
                itemB = ints[j];
                break;
            }

            if (itemB > itemA)
            {
                conjuntoNuevo.Add(itemB);
            }
            else
            {
                conjuntoNuevo.Add(itemA);
            }
        }

        return conjuntoNuevo;
    }
    public override ConjuntoTDA<int> Intersection(ConjuntoTDA<int> otherSet)
    {
        ConjuntoDinamico conjuntoNuevo = new ConjuntoDinamico();
        conjuntoNuevo.isDinamic = isDinamic;

        foreach (int item in intList)
        {
            if (intList.IndexOf(item) >= 10 && !conjuntoNuevo.isDinamic)
            {
                break;
            }

            if (otherSet.Contains(item))
            {
                conjuntoNuevo.Add(item);
            }
        }

        return conjuntoNuevo;
    }
    public override ConjuntoTDA<int> Difference(ConjuntoTDA<int> otherSet)
    {
        ConjuntoDinamico conjuntoNuevo = new ConjuntoDinamico();
        conjuntoNuevo.isDinamic = isDinamic;

        foreach (int item in intList)
        {
            if (intList.IndexOf(item) >= 10 && !conjuntoNuevo.isDinamic)
            {
                break;
            }

            if (!otherSet.Contains(item))
            {
                conjuntoNuevo.Add(item);
            }
        }
        foreach (int item in otherSet.intList)
        {
            if (otherSet.intList.IndexOf(item) >= 10 && !conjuntoNuevo.isDinamic)
            {
                break;
            }
            if (!Contains(item))
            {
                conjuntoNuevo.Add(item);
            }
        }

        return conjuntoNuevo;
    }

    public void SetToArray() 
    {
        if(intList.Count > 10) 
        {
            intList.RemoveRange(11, intList.Count - 1);
        }

        for (int i = 0; i < 10; i++) 
        {
            if (i > intList.Count) 
            {
                ints[i] = 0;
            }
        }
        isDinamic = false;
    }

    public void SetToList() 
    {
        isDinamic = true;

    }
}
