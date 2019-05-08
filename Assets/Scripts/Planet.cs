using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool down = true;
    void Start()
    {
        float scale = Random.Range(1f, 1.3f);
        transform.localScale = new Vector3(scale, scale, 0);
    }
    void Update()
    {
        if (down)
        {
            transform.localScale -= new Vector3(Time.deltaTime * (1 / transform.parent.localScale.x) * 0.6f, Time.deltaTime * (1 / transform.parent.localScale.x) * 0.6f, 0);
            if (transform.localScale.x - Time.deltaTime * (1 / transform.parent.localScale.x) * 0.6f <= 1)
            {
                down = false;
            }
        }
        else
        {
            transform.localScale += new Vector3(Time.deltaTime * (1 / transform.parent.localScale.x) * 1.2f, Time.deltaTime * (1 / transform.parent.localScale.x) * 1.2f, 0);
            if (transform.localScale.x + Time.deltaTime * (1 / transform.parent.localScale.x) * 1.2f >= 1.3f)
            {
                down = true;
            }
        }
    }
}
