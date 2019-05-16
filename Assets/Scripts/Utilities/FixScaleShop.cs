using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScaleShop : MonoBehaviour
{
    public Transform container;
    void Start()
    {
        float scale = (Mathf.Pow((1080f / 1920f), 0.5f) / Mathf.Pow(((float)Screen.width / (float)Screen.height), 0.5f)) * (Mathf.Sqrt((float)Screen.height * (float)Screen.width) / Mathf.Sqrt(1920f * 1080f));
        container.localScale = new Vector3(scale, scale, 1);
    }
}
