using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public bool released;

    public float orthoZoomSpeed = 0.2f;        // The rate of change of the orthographic size in orthographic mode.

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the orthographic size based on the change in distance between the touches.
            cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
        }
        transform.position = player.position + new Vector3(0, 0, -10);
        if (!released)
        {
            cam.orthographicSize -= Input.mouseScrollDelta.y;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5f, 60f);
        }
    }

    public void Release()
    {
        StartCoroutine(ZoomIn());
        released = true;
    }

    IEnumerator ZoomIn()
    {
        float oldSize = cam.orthographicSize;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.005f);
            cam.orthographicSize = Mathf.Lerp(oldSize, 20f, i / 100f);
        }
    }
}
