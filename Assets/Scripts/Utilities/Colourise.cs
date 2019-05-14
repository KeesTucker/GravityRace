using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colourise : MonoBehaviour
{
    public Color color;

    public Gradient gradient;

    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    void Start()
    {
        colorKey = new GradientColorKey[2];
        alphaKey = new GradientAlphaKey[2];

        colorKey[0].color = color;
        colorKey[0].time = 0.6f;
        colorKey[1].color = color;
        colorKey[1].time = 1f;
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.6f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;
        gradient.SetKeys(colorKey, alphaKey);
        GetComponent<SpriteRenderer>().color = color;
        GetComponent<TrailRenderer>().startColor = color;
        GetComponent<TrailRenderer>().endColor = color;
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
        colorOverLifetime.color = gradient;
        colorOverLifetime = transform.GetChild(1).GetComponent<ParticleSystem>().colorOverLifetime;
        colorOverLifetime.color = gradient;
        colorOverLifetime = transform.GetChild(3).GetComponent<ParticleSystem>().colorOverLifetime;
        colorOverLifetime.color = gradient;
        transform.GetChild(2).GetComponent<LineRenderer>().colorGradient = gradient;
    }
}
