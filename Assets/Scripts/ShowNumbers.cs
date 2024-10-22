using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNumbers : MonoBehaviour
{
    [SerializeField] ConjuntoEstatico conjuntoEstatico;
    TextMeshProUGUI textUI;

    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string textToShow = string.Empty;

        foreach(int number in conjuntoEstatico.ints) 
        {
            textToShow += $"{number}\n";
        }

        textUI.text = textToShow;
    }
}
