using TMPro;
using UnityEngine;

public class ItemScore : MonoBehaviour
{
    public TextMeshProUGUI IdTMPro;
    public TextMeshProUGUI playerNameTMPro;
    [SerializeField] private TextMeshProUGUI score;

    public int Score = 0;

    private void Awake()
    {
        Score = Random.Range(1, 999);
        score.text = Score.ToString();
    }
}
