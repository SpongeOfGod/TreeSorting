using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    [Header("Quest Controller")]
    [SerializeField] Transform parentOfQuest;
    [SerializeField] List<GameObject> quests;
    [SerializeField] Queue<Quest> questsToDo = new Queue<Quest>();
    [SerializeField] Button QuestDone;
    [SerializeField] GameObject button;

    [Header("Decoration")]
    [SerializeField] TextMeshProUGUI textToShow;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RawImage imageToShow;
    [SerializeField] Animator confettiAnimator;
    [SerializeField] Texture endImage;
    [SerializeField] string endText;

    private void Awake()
    {
        QuestDone.onClick.AddListener(CheckAvailableQuests);
    }
    private void Start()
    {
        foreach (GameObject objQuest in quests) 
        {
            Quest newQuest = Instantiate(objQuest).GetComponent<Quest>();
            questsToDo.Enqueue(newQuest);
        }

        questsToDo.TryPeek(out Quest quest);
        DecorationReset(quest);
    }

    private void CheckAvailableQuests() 
    {
        questsToDo.TryDequeue(out Quest DequeuedQuest);

        if (questsToDo.Count > 0) 
        {
            questsToDo.TryPeek(out Quest currentQuest);
            DecorationReset(currentQuest);
            return;
        }
        else 
        {
            imageToShow.texture = endImage;
            textToShow.text = endText;
            confettiAnimator.SetBool("Celebration", true);
            button.SetActive(false);
        }
    }

    private void DecorationReset(Quest currentQuest) 
    {
        imageToShow.texture = currentQuest.ImageToShow;
        imageToShow.SetNativeSize();
        textToShow.text = currentQuest.QuestName;
        buttonText.text = currentQuest.ButtonText;
    }
}
