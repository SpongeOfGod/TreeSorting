using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWritter : MonoBehaviour
{
    public ConjuntoEstatico ConjuntoEstatico;
    public ConjuntoEstatico ConjuntoPrevio;
    TextMeshProUGUI textMeshProUGUI;
    Stack<string> strings = new Stack<string>();
    public ShowNumbers showNumbersPrefab;
    public event Action Enter;
    public Transform parent;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        ConjuntoEstatico = new ConjuntoEstatico();
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
            ConjuntoEstatico.Add(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.R) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int intString);
            ConjuntoEstatico.Remove(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.C) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int textNumber);

            if (ConjuntoEstatico.Contains(textNumber))
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
            Debug.Log($"Random Item: {ConjuntoEstatico.Show()}");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Longitud del conjunto: {ConjuntoEstatico.Cardinality()}");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ConjuntoEstatico.isEmpty())
            {
                Debug.Log("El conjunto esta vacío");
            }
            else
            {
                Debug.Log("El conjunto no esta vacío");
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && textMeshProUGUI.text != string.Empty)
        {
            //

        }

        if (Input.GetKeyDown(KeyCode.I) && textMeshProUGUI.text != string.Empty)
        {
            //
        }

        if (Input.GetKeyDown(KeyCode.D) && textMeshProUGUI.text != string.Empty)
        {
            //
        }

        if (Input.GetKeyDown(KeyCode.Return) && ConjuntoPrevio == null)
        {
            Enter.Invoke();
            ConjuntoPrevio = ConjuntoEstatico;
            ConjuntoEstatico = new ConjuntoEstatico();
            ShowNumbers showObject = Instantiate(showNumbersPrefab, parent);
            showObject.textWritter = this;
        }
    }

    public void clearText()
    {
        textMeshProUGUI.text = string.Empty;
    }
}
