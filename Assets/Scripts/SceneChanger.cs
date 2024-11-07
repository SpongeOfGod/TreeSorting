using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string sceneToGo;
    public void OnPointerClick(PointerEventData data)
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
