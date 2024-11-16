using UnityEngine;
using TMPro;
public class CheckTagUI : MonoBehaviour
{
    private TextMeshProUGUI uiText;
    private string initialText;

    public string textPlayer = " de inicio.";
    public string textExit = " de la salida.";
    public string textWall = " de la pared.";
    public string textVertice = " del camino.";

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        initialText = uiText.text;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MazeTagManager.Instance.Input) 
        {
            case "Player":
                uiText.text = initialText + $" {textPlayer}";
                break;
            case "Exit":
                uiText.text = initialText + $" {textExit}";
                break;
            case "Wall":
                uiText.text = initialText + $" {textWall}";
                break;
            case "Vertice":
                uiText.text = initialText + $" {textVertice}";
                break;
        }
    }
}
