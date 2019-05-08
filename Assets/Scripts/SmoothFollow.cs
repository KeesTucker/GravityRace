using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public bool released;

    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, -10);
        if (!released)
        {
            cam.orthographicSize -= Input.mouseScrollDelta.y;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5f, 60f);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ZoomIn());
            released = true;
        }
    }

    IEnumerator ZoomIn()
    {
        float oldSize = cam.orthographicSize;
        Debug.Log(oldSize);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.005f);
            cam.orthographicSize = Mathf.Lerp(oldSize, 20f, i / 100f);
            Debug.Log(Mathf.Lerp(oldSize, 20, i / 100));
        }
    }
}
