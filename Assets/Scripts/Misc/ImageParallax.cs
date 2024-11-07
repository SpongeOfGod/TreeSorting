using UnityEngine;
using UnityEngine.UI;

public class ImageParallax : MonoBehaviour
{
    [SerializeField] float x, y;
    RawImage image;

    private void Start()
    {
        image = GetComponent<RawImage>();
    }
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
