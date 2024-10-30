using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Test.Muestra;
using System.IO;

public class TextWritter : MonoBehaviour
{
    public TDA<int> Conjunto;
    public TDA<int> ConjuntoPrevio;
    TextMeshProUGUI textMeshProUGUI;
    Stack<string> strings = new Stack<string>();
    public ShowNumbers showNumbersPrefab;
    public event Action Enter;
    public event Action NuevoConjunto;
    public event Action changeGroupingType;
    public Transform CurrentParentShow;
    public Transform PreviousParentShow;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Conjunto = new StaticTDA<int>(10);
    }

    private void Update()
    {
        char charWrite = ' ';

        InputHandler();

        if (Input.inputString != string.Empty)
        {
            charWrite = Input.inputString[0];

            if (charWrite == '\b')
            {
                if (strings.Count > 0)
                {
                    textMeshProUGUI.text = strings.Pop();
                    return;
                }
            }
        }


        if (char.IsDigit(charWrite))
        {
            strings.Push(textMeshProUGUI.text);
            textMeshProUGUI.text += charWrite.ToString();
        }

    }

    public void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.A) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int intString);
            Conjunto.Add(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.R) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int intString);
            Conjunto.Remove(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.C) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int textNumber);

            if (Conjunto.Contains(textNumber))
            {
                Debug.Log($"El conjunto contiene {textNumber}");
            }
            else
            {
                Debug.Log($"No se ha encontrado referencia a {textNumber} en el conjunto");
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && textMeshProUGUI.text != string.Empty)
        {
            Debug.Log($"Random Item: {Conjunto.Show()}");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Longitud del conjunto: {Conjunto.Cardinality()}");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Conjunto.IsEmpty())
            {
                Debug.Log("El conjunto esta vacío");
            }
            else
            {
                Debug.Log("El conjunto no esta vacío");
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (Equals(Conjunto.GetType(), new DynamicTDA<int>()))
            {
                StaticTDA<int> tempStatic = new StaticTDA<int>(10);
                Conjunto = tempStatic.Union(Conjunto);
                Debug.Log("Cambio a Static");
            }
            else 
            {
                DynamicTDA<int> tempDynamic = new DynamicTDA<int>();
                Conjunto = tempDynamic.Union(Conjunto);
                Debug.Log("Cambio a Dynamic");
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && ConjuntoPrevio != null)
        {
            StaticTDA<int> conjunto = (StaticTDA<int>)Conjunto.Union(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.I) && ConjuntoPrevio != null)
        {
            StaticTDA<int> conjunto = (StaticTDA<int>)Conjunto.Intersection(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) && ConjuntoPrevio != null)
        {
            StaticTDA<int> conjunto = (StaticTDA<int>)Conjunto.Difference(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Return) && ConjuntoPrevio == null)
        {
            Enter.Invoke();
            ConjuntoPrevio = Conjunto;
            Conjunto = new StaticTDA<int>(10);
            ShowNumbers showObject = Instantiate(showNumbersPrefab, CurrentParentShow);
            showObject.textWritter = this;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            changeGroupingType.Invoke();
        }
    }

    public void clearText()
    {
        textMeshProUGUI.text = string.Empty;
    }
}
