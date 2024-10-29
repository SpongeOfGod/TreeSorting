using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroupingTypeText : MonoBehaviour
{
    private string staticGroup = "ESTATICO";
    private string dynamicGroup = "DINAMICO";
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextWritter textWritter;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = staticGroup;
        textWritter.changeGroupingType += ChangeType;
    }

    void ChangeType()
    {
        textMeshProUGUI.text = textWritter.Conjunto.isDinamic ? dynamicGroup : staticGroup;
    }
}
