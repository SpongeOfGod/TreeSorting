using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerticeVisual : MonoBehaviour
{
    public TextMeshProUGUI DataText;
    public SpriteRenderer Sprite;
    public Vertice Vertice;
    [SerializeField] List<(GameObject, int)> values = new List<(GameObject, int)>();
    [SerializeField] private Color originalColor;

    public void Start()
    {
        Vertice = new Vertice(this);
        values.Add((gameObject, 1));

        
    }
}
