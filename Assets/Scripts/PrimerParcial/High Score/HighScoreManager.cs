using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Color TopColor;
    public Color BottomColor;

    [SerializeField] GameObject itemScorePrefab;
    [SerializeField] Transform ScoreTransform;
    [SerializeField] int MaxNumberOfPlayers = 10;
    [SerializeField] List<GameObject> itemScores = new List<GameObject>();
    [SerializeField] Button SortHighScore;
    [SerializeField] Button SortLowScore;
    [SerializeField] private int startIndex = 0;
    [SerializeField] private int endIndex = 10;

    private bool Tuppple = true;
    private SortType currentSort = SortType.ByHigh;
    enum SortType 
    {
        ByHigh, ByLow
    }
    private void Awake()
    {
        SortHighScore.onClick.AddListener(SortByHighScore);
        SortLowScore.onClick.AddListener(SortByLowScore);
    }

    private void Start()
    {
        for (int i = 0; i < MaxNumberOfPlayers; i++) 
        {
            GameObject scoreItem = Instantiate(itemScorePrefab, ScoreTransform);
            ItemScore itemScore = scoreItem.GetComponent<ItemScore>();
            itemScore.playerNameTMPro.text = "Player " + (i + 1).ToString();
            itemScores.Add(scoreItem);
            itemScore.IdTMPro.text = (itemScores[i].transform.GetSiblingIndex() + 1).ToString() + ".";
        }
        SortByHighScore();
    }

    private void SortByHighScore() 
    {
        foreach (var itemScore in itemScores)
        {
            if(itemScore == itemScores[0]) 
            {
                ChangeColor(itemScore, Color.white);
            }
        }
        currentSort = SortType.ByHigh;
        SortByScore();

        foreach (var itemScore in itemScores)
        {
            if (itemScore == itemScores[0])
            {
                ChangeColor(itemScore, TopColor);
            }
        }
    }

    private void SortByLowScore() 
    {
        foreach (var itemScore in itemScores)
        {
            if (itemScore == itemScores[0])
            {
                ChangeColor(itemScore, Color.white);
            }
        }

        currentSort = SortType.ByLow;
        SortByScore();

        foreach (var itemScore in itemScores)
        {
            if (itemScore == itemScores[0])
            {
                ChangeColor(itemScore, BottomColor);
            }
        }
    }

    private void SortByScore() 
    {
        Tuppple = true;
        startIndex = 0;
        endIndex = itemScores.Count;

        while (Tuppple) 
        {
            Tuppple = false;

            for (int i = startIndex; i < endIndex - 1; i++)
            {
                ItemScore itemScoreA = itemScores[i].GetComponent<ItemScore>();
                ItemScore itemScoreB = itemScores[i + 1].GetComponent<ItemScore>();

                switch (currentSort) 
                {
                    case SortType.ByHigh:
                        if (itemScoreA.Score < itemScoreB.Score) 
                        {
                            TuppleScores(i, i + 1, itemScoreA, itemScoreB);
                        }
                        break;

                    case SortType.ByLow:
                        if (itemScoreA.Score > itemScoreB.Score) 
                        {
                            TuppleScores(i, i + 1, itemScoreA, itemScoreB);
                        }

                        break;
                }
            }

            if (!Tuppple)
            {
                break;
            }

            Tuppple = false;

            endIndex--;


            for (int j = endIndex - 1; j > startIndex; j--)
            {
                ItemScore itemScoreA = itemScores[j].GetComponent<ItemScore>();
                ItemScore itemScoreB = itemScores[j - 1].GetComponent<ItemScore>();

                switch (currentSort) 
                {
                    case SortType.ByHigh:
                        if (itemScoreA.Score > itemScoreB.Score)
                        {
                            TuppleScores(j, j - 1, itemScoreA, itemScoreB);
                        }
                        break;

                    case SortType.ByLow:
                        if (itemScoreA.Score < itemScoreB.Score) 
                        {
                            TuppleScores(j, j - 1, itemScoreA, itemScoreB);
                        }
                        break;
                }
            }
            startIndex++;
        }
    }

    private void TuppleScores(int indexA, int indexB, ItemScore itemScoreA, ItemScore itemScoreB) 
    {
        (itemScores[indexA], itemScores[indexB]) = (itemScores[indexB], itemScores[indexA]);
        SetSiblingIndex(indexA, indexB);
        itemScoreA.IdTMPro.text = (itemScores[indexB].transform.GetSiblingIndex() + 1).ToString();
        itemScoreB.IdTMPro.text = (itemScores[indexA].transform.GetSiblingIndex() + 1).ToString();
        Tuppple = true;
    }

    private void SetSiblingIndex(int firstIndex, int secondIndex)
    {
        itemScores[firstIndex].transform.SetSiblingIndex(firstIndex);
        itemScores[secondIndex].transform.SetSiblingIndex(secondIndex);
    }

    private void ChangeColor(GameObject itemScore, Color desiredColor) 
    {
        TextMeshProUGUI[] texts = itemScore.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in texts)
        {
            text.color = desiredColor;
        }
    }
}
