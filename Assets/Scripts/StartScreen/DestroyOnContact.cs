using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public GameObject explode;

    public Gradient gradient;

    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "End")
        {
            colorKey = new GradientColorKey[2];
            alphaKey = new GradientAlphaKey[2];

            colorKey[0].color = GetComponent<SpriteRenderer>().color;
            colorKey[0].time = 0.6f;
            colorKey[1].color = GetComponent<SpriteRenderer>().color;
            colorKey[1].time = 1f;
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.6f;
            alphaKey[1].alpha = 0f;
            alphaKey[1].time = 1.0f;
            gradient.SetKeys(colorKey, alphaKey);
            GameObject explosion = Instantiate(explode, transform.position, Quaternion.identity);
            explosion.transform.localScale = transform.localScale;
            ParticleSystem.ColorOverLifetimeModule colorOverLifetime = explosion.GetComponent<ParticleSystem>().colorOverLifetime;
            colorOverLifetime.color = gradient;
            Destroy(gameObject);
        }
    }
}
