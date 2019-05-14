using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShadow : MonoBehaviour
{
    public Transform sun;
    public float modifier = 1f;
    public float lengthModifier = 1f;
    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.Find("End").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(sun.position, transform.up);
        
        transform.GetChild(0).localScale = new Vector3(1 * modifier, Mathf.Clamp(Vector2.Distance(sun.position, transform.position) / 40f, 0.6f, 1.5f) * modifier * lengthModifier, 1);
    }
}
