using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNumbers : MonoBehaviour
{
    [SerializeField] ConjuntoDinamico conjuntoEstatico;
    public TextWritter textWritter;
    TextMeshProUGUI textUI;
    public bool deattach;
    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        if (textWritter != null) 
        {
            conjuntoEstatico = textWritter.Conjunto;
        }
        textWritter.Enter += Deattachment;
        textWritter.NuevoConjunto += NuevoConjunto;
    }

    void Update()
    {
        if (!deattach) 
        {
            string textToShow = string.Empty;

            foreach(int number in conjuntoEstatico.ints) 
            {
                textToShow += $"{number}\n";
            }

            textUI.text = textToShow;
        }

        if (deattach && textWritter.ConjuntoPrevio == null) 
        {
            Destroy(gameObject);
        }
    }

    public void Deattachment() 
    {
        deattach = true;
        textUI.color = Color.red;
        transform.parent = textWritter.PreviousParentShow;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void NuevoConjunto() 
    {
        conjuntoEstatico = textWritter.Conjunto;
    }

    private void OnDestroy()
    {
        textWritter.NuevoConjunto -= NuevoConjunto;
        textWritter.Enter -= Deattachment;
    }
}
