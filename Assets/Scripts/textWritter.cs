using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textWritter : MonoBehaviour
{
    ConjuntoEstatico conjuntoEstatico;
    TextMeshProUGUI textMeshProUGUI;
    Stack<string> strings = new Stack<string>();

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        conjuntoEstatico = GetComponent<ConjuntoEstatico>();
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
            conjuntoEstatico.Add(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.R) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int intString);
            conjuntoEstatico.Remove(intString);
            clearText();
        }

        if (Input.GetKeyDown(KeyCode.C) && textMeshProUGUI.text != string.Empty)
        {
            int.TryParse(textMeshProUGUI.text, out int textNumber);

            if (conjuntoEstatico.Contains(textNumber)) 
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
            Debug.Log($"Random Item: {conjuntoEstatico.Show()}");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Longitud del conjunto: {conjuntoEstatico.Cardinality()}");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (conjuntoEstatico.isEmpty()) 
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
    }

    public void clearText() 
    {
        textMeshProUGUI.text = string.Empty;
    }
}
