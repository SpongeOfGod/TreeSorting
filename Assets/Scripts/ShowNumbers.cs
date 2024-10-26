using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNumbers : MonoBehaviour
{
    [SerializeField] ConjuntoEstatico conjuntoEstatico;
    public TextWritter textWritter;
    TextMeshProUGUI textUI;
    public bool deattach;
    [SerializeField] Transform parent;
    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        if (textWritter != null) 
        {
            conjuntoEstatico = textWritter.ConjuntoEstatico;
        }
        textWritter.Enter += Deattachment;
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

        if (conjuntoEstatico == null) 
        {
            Destroy(gameObject);
        }
    }

    public void Deattachment() 
    {
        deattach = true;
        textUI.color = Color.red;
        transform.parent = parent;
        transform.position = Vector3.zero;
    }
}
