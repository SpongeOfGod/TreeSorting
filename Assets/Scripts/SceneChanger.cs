using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneChanger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string sceneToGo;
    public void OnPointerClick(PointerEventData data)
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
