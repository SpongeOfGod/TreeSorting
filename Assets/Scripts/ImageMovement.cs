using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ImageMovement : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GraphManager graphManager;
    [SerializeField] float timeToReach = 5;
    [SerializeField] float UpOffsetMultiplier = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (graphManager != null) 
        {
            rectTransform.anchoredPosition = Vector3.Slerp(rectTransform.anchoredPosition, graphManager.PlayerVertice.transform.localPosition + Vector3.up * UpOffsetMultiplier, timeToReach * Time.deltaTime);
        }
    }
}
