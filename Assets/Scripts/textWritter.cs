using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWritter : MonoBehaviour
{
    public ConjuntoDinamico Conjunto;
    public ConjuntoDinamico ConjuntoPrevio;
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
        Conjunto = new ConjuntoDinamico();
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
            if (Conjunto.isEmpty())
            {
                Debug.Log("El conjunto esta vacío");
            }
            else
            {
                Debug.Log("El conjunto no esta vacío");
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && ConjuntoPrevio != null)
        {
            ConjuntoDinamico conjunto = (ConjuntoDinamico)Conjunto.Union(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.I) && ConjuntoPrevio != null)
        {
            ConjuntoDinamico conjunto = (ConjuntoDinamico)Conjunto.Intersection(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) && ConjuntoPrevio != null)
        {
            ConjuntoDinamico conjunto = (ConjuntoDinamico)Conjunto.Difference(ConjuntoPrevio);
            Conjunto = conjunto;
            ConjuntoPrevio = null;
            NuevoConjunto.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Return) && ConjuntoPrevio == null)
        {
            Enter.Invoke();
            ConjuntoPrevio = Conjunto;
            Conjunto = new ConjuntoDinamico();
            Conjunto.isDinamic = ConjuntoPrevio.isDinamic;
            ShowNumbers showObject = Instantiate(showNumbersPrefab, CurrentParentShow);
            showObject.textWritter = this;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (Conjunto.isDinamic) 
            {
                Conjunto.SetToArray();
            }
            else 
            {
                Conjunto.SetToList();
            }
            changeGroupingType.Invoke();
        }
    }

    public void clearText()
    {
        textMeshProUGUI.text = string.Empty;
    }
}
