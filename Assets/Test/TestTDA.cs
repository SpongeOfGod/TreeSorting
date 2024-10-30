using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestTDA : MonoBehaviour
    {
        //ConjuntoDinamico c1 = new ConjuntoDinamico();
        //ConjuntoEstatico c2 = new ConjuntoEstatico();

        //Muestra.StaticTDA<float> tda1 = new Muestra.StaticTDA<float>(10);
        //Muestra.DynamicTDA<float> tda2 = new Muestra.DynamicTDA<float>();

        //private void Start()
        //{
        //    for (int i = 0; i < 10; i++)
        //        c1.Add(i);

        //    for (int i = 5; i < 10; i++)
        //        c2.Add(i);

        //    var c3 = new ConjuntoEstatico();
        //}
    }

    namespace Muestra
    {
        public abstract class T_TDA<T>
        {
            // Todas las funciones Abstractas        
            public abstract T_TDA<T> Union(T_TDA<T> other);
            public abstract T_TDA<T> Intersect(T_TDA<T> other);
            public abstract bool Contains(T element);
            public abstract bool Add(T element);
            public abstract T GetElement(int index);
            public abstract int Cardinality();
        }

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

        public class DynamicTDA<T> : TDA<T>
        {
            List<T> datas;

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
    }
}