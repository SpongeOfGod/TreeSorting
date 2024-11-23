using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public int buildIndex;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") 
        {
            instance = null;
            Destroy(this);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeScene(7);
            buildIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeScene(8);
            buildIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeScene(9);
            buildIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeScene(10);
            buildIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeScene(11);
            buildIndex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeScene(12);
            buildIndex = 5;
        }
    }

    void ChangeScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (SceneManager.GetActiveScene().buildIndex != sceneIndex)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
