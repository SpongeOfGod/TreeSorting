using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadSceneButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(ReloadScene);        
    }

    private void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
