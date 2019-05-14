using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float modifier;
    void Start()
    {
        modifier = Random.Range(-200f, 200f);
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, modifier * Time.deltaTime));
    }
}
