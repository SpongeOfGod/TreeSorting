using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public TextMeshProUGUI TypingDecorationText;
    public string StringTyping = string.Empty;
    public string StringTutorial = string.Empty;
    public int maxNumbersOfChars = 18;

    [SerializeField] Stack<string> currentText = new Stack<string>();
    [SerializeField] private GameObject TextPrefab;
    [SerializeField] TextMeshProUGUI writtingText;
    [SerializeField] private GameObject currentTextItem;
    [SerializeField] private GameObject writtingTextParent;
    [SerializeField] private GameObject notesTextParent;
    [SerializeField] private float offsetMultiplier;
    
    private bool writting = false;

    private void Start()
    {
        createTextItem();
    }
    private void Update()
    {
        if (writting && !Input.GetKey(KeyCode.Y) && !Input.GetKeyUp(KeyCode.Y)) 
        {
            CheckInputText();
            TypingDecorationText.text = StringTyping;
        }
        else 
        {
            TypingDecorationText.text = StringTutorial;
        }
        
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            writting = !writting;
        }
    }

    private bool checkInputString(string a) 
    {
        if(a == string.Empty || a == "\r" || a == null) 
        {
            return true;
        }

        return false;
    }
    private void CheckInputText() 
    {
        if (!Input.GetKey(KeyCode.Backspace) && !Input.GetKeyUp(KeyCode.Backspace))
        {
            if (!checkInputString(Input.inputString)) 
            {
                currentText.TryPeek(out string peekString);
                peekString += Input.inputString;
                if (peekString != null && peekString.Length < maxNumbersOfChars) 
                {
                    writtingText.text = peekString;
                    currentText.Push(writtingText.text);
                }
            }
        }
        else if (Input.GetKey(KeyCode.Backspace))
        {
            currentText.TryPop(out string popText);
            currentText.TryPeek(out string peekText);
            writtingText.text = peekText;  
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (writtingText.text != null && writtingText.text != string.Empty)
            {
                currentText.Clear();
                currentTextItem.transform.parent = notesTextParent.transform;
                RectTransform RectT = notesTextParent.GetComponent<RectTransform>();
                int childLength = notesTextParent.transform.childCount;
                Debug.Log(childLength);
                createTextItem();

                RectTransform childRectT = notesTextParent.transform.GetChild(childLength - 1).gameObject.GetComponent<RectTransform>();
                RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, RectT.sizeDelta.y - childRectT.localPosition.y * offsetMultiplier);
            }
        }
    }
    private void createTextItem() 
    {
        currentTextItem = Instantiate(TextPrefab, writtingTextParent.transform);
        foreach (Transform t in currentTextItem.transform) 
        {
            if(t.gameObject.name == "Name") 
            {
                TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
                text.color = RandomColor();
            }

            if(t.gameObject.name == "WriteInput") 
            {
                writtingText = t.gameObject.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    private Color RandomColor() 
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        return color;
    }
}
