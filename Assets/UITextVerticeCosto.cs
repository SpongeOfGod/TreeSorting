using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

public class UITextVerticeCosto : MonoBehaviour
{
    TextMeshProUGUI UIText;
    public PathSearch pathSearch;
    public GraphManager graphManager;
    public int weight = 0;
    private VisualVertice currentExit = null;
    [SerializeField] bool onceChange;
    [SerializeField] bool weightAddUp;
    // Start is called before the first frame update
    void Start()
    {
        UIText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (graphManager.ExitVertice != null && !graphManager.InSearch) 
        {
            if (currentExit != graphManager.ExitVertice) 
            {
                List<VisualVertice> ListpathSearch = pathSearch.CheckVerticeSaliente(graphManager.PlayerVertice.Vertice);
                if (ListpathSearch != null) 
                {
                    ListpathSearch.Reverse();
                    currentExit = graphManager.ExitVertice;
                    if (!weightAddUp) 
                    {
                        for (int i = 0; i < ListpathSearch.Count; i++) 
                        {
                            if (i + 1 >= ListpathSearch.Count) break;
                            weight += graphManager.Graph.ConnectionWeight(ListpathSearch[i].Vertice, ListpathSearch[i + 1].Vertice);
                        }
                        UIText.text = weight.ToString();
                        weightAddUp = true;
                    }
                }
                else 
                {
                    UIText.text = "INALCANZABLE";
                }
            } 
        }
        else if(graphManager.ExitVertice == null) 
        {
            weightAddUp = false;
            weight = 0;
        }
    }
}
