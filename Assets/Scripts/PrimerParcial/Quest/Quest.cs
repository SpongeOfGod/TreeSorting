using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private string buttonText;
    [SerializeField] private string questName;
    [SerializeField] private Texture imageToShow;

    public string ButtonText => buttonText;
    public string QuestName => questName;
    public Texture ImageToShow => imageToShow;
}
