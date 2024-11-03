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
    [SerializeField] Vector3 UpOffset = new Vector3();
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (graphManager != null && graphManager.PlayerVertice != null) 
        {
            rectTransform.anchoredPosition = Vector3.Slerp(rectTransform.anchoredPosition, graphManager.PlayerVertice.transform.localPosition + UpOffset, timeToReach * Time.deltaTime);
        }
    }
}
