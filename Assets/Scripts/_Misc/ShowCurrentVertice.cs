using TMPro;
using UnityEngine;

public class ShowCurrentVertice : MonoBehaviour
{
    TextMeshProUGUI textUI;
    [SerializeField] GraphManager graphManager;
    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (textUI != null && graphManager != null)
            textUI.text = graphManager.PlayerVertice.Vertice.Value.ToString();
    }
}
