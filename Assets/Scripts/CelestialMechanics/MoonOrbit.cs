using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public float modifier;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, modifier * Time.deltaTime));
    }
}
