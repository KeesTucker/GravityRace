using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForward : MonoBehaviour
{
    public Rigidbody2D r;

    void Start()
    {

    }

    void Update()
    {
        transform.up = r.velocity.normalized;
    }
}
