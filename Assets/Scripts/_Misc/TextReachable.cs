using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReachable : MonoBehaviour
{
    [SerializeField] private string reachable = "TIENE SOLUCI�N";
    [SerializeField] private string noReachable = "NO TIENE SOLUCI�N";
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GraphManager graphManager;
    public PathSearch pathSearch;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = noReachable;
    }

    void Update()
    {
        if (graphManager.PlayerVertice != null && graphManager.ExitVertice != null && graphManager.PlayerVertice != graphManager.ExitVertice) 
        {
            if(pathSearch.CheckVerticeSaliente(graphManager.PlayerVertice.Vertice) != null) 
            {
                textMeshProUGUI.text = reachable;
            }
            else 
            {
                textMeshProUGUI.text = noReachable;
            }
        }
    }
}
