using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForward : MonoBehaviour
{
    public Rigidbody2D r;

    void Update()
    {
        if (r.velocity.normalized != new Vector2(0, 0))
        {
            transform.up = r.velocity.normalized;
        }
    }
}
