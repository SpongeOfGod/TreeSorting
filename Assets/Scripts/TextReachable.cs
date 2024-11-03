using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReachable : MonoBehaviour
{
    [SerializeField] private string reachable = "ALCANZABLE";
    [SerializeField] private string noReachable = "NO ALCANZABLE";
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GraphManager graphManager;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = noReachable;
    }

    void Update()
    {
        textMeshProUGUI.text = graphManager.CanArrive ? reachable : noReachable;
    }
}
