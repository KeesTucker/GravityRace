using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool down = true;
    public float maxSize = 1.3f;

    public int run;

    void Start()
    {
        if (transform.parent.GetComponent<PlanetGravity>().bodyType == PlanetGravity.type.BlackHole)
        {
            maxSize = 3f;
            float scale = Random.Range(1f, maxSize);
            transform.localScale = new Vector3(scale, scale, 0);
            run = 0;
        }
        else if (transform.parent.GetComponent<PlanetGravity>().bodyType == PlanetGravity.type.WhiteHole)
        {
            maxSize = 2f;
            float scale = Random.Range(1f, maxSize);
            transform.localScale = new Vector3(scale, scale, 0);
            run = 1;
        }
        else
        {
            run = 2;
        }
    }
    void Update()
    {
        if (run == 0)
        {
            if (down)
            {
                transform.localScale -= new Vector3(Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize, Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize, 0);
                if (transform.localScale.x - Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize <= 1)
                {
                    down = false;
                }
            }
            else
            {
                transform.localScale = new Vector3(maxSize, maxSize);
                down = true;
            }
        }
        else if (run == 1)
        {
            if (down)
            {
                transform.localScale = new Vector3(1, 1);
                down = false;
            }
            else
            {
                
                transform.localScale += new Vector3(Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize, Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize, 0);
                if (transform.localScale.x - Time.deltaTime * (1 / transform.parent.localScale.x) * 1 * maxSize >= maxSize)
                {
                    down = true;
                }
            }
        }
    }
}
