using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocity : MonoBehaviour
{
    public Vector3 velocity;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
