using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStringText : MonoBehaviour
{
    [SerializeField] private GraphManager graphManager;
    private TextMeshProUGUI textUI;
    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (textUI != null && graphManager != null)
            textUI.text = graphManager.textShow;
    }
}
