using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Stack<string> textStack = new Stack<string>();
    private bool moving = false;

    private Animator animatorDance;
    private Stack<Vector3> positions = new Stack<Vector3>();
    private KeyCode up = KeyCode.UpArrow;
    private KeyCode down = KeyCode.DownArrow;
    private KeyCode left = KeyCode.LeftArrow;
    private KeyCode right = KeyCode.RightArrow;
    private KeyCode back = KeyCode.Z;
    private KeyCode lCtrl = KeyCode.LeftControl;
    private void Start()
    {
        animatorDance = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(up)) 
            {
                MovePosition(Vector3.up);
            }

            if (Input.GetKeyDown(down)) 
            {
                MovePosition(Vector3.down);
            }

            if (Input.GetKeyDown(right)) 
            {
                MovePosition(Vector3.right);
            }

            if (Input.GetKeyDown(left)) 
            {
                MovePosition(Vector3.left);
            }

            if (Input.GetKey(lCtrl))
            {
                if (Input.GetKeyDown(back)) 
                {
                    if(positions.TryPop(out Vector3 previousPosition) && textStack.TryPop(out string previousString)) 
                    {
                        transform.position = previousPosition;
                        textMeshProUGUI.text = previousString;
                    }
                }
            }
        }
    }

    private void MovePosition(Vector3 direction) 
    {
        positions.Push(transform.position);
        textStack.Push(textMeshProUGUI.text);
        animatorDance.SetBool("Jumpy", true);

        moving = true;
        StartCoroutine(smoothDamp(direction, 0.30f));
        StartCoroutine(waitAnimationFinish(direction));
    }

    IEnumerator waitAnimationFinish(Vector3 direction) 
    {
        yield return new WaitForSeconds(0.30f);
        animatorDance.SetBool("Jumpy", false);

        string text = direction.ToString();
        textMeshProUGUI.text += text + "\n";

    }

    IEnumerator smoothDamp(Vector3 direction, float duration) 
    {
        float elapsedTime = 0;
        Vector3 initialPos = transform.position;
        Vector3 desiredPos = initialPos + direction * speed;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, desiredPos, elapsedTime / duration);
            yield return null;
        }

        Debug.Log(desiredPos);
        moving = false;
        transform.position = desiredPos;
    }
}
