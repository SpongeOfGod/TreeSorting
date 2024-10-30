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
    }
}