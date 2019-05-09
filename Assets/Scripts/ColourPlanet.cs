using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPlanet : MonoBehaviour
{
    public SpriteRenderer planet;
    public SpriteRenderer glow;

    public PlanetGravity planetGrav;

    public void Start()
    {
        if (planetGrav.bodyType == PlanetGravity.type.Planet)
        {
            Color color = Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            planet.color = color;
            Color.RGBToHSV(color, out float h, out float s, out float v);
            s = Mathf.Clamp(s - 0.2f, 0, 1f);
            v = Mathf.Clamp(v + 0.2f, 0, 1f);
            color = Color.HSVToRGB(h, s, v);
            glow.color = color;
        }
        else if (planetGrav.bodyType == PlanetGravity.type.BlackHole)
        {
            Color color = Color.black;
            planet.color = color;
            color = Color.grey;
            glow.color = color;
        }
        else if (planetGrav.bodyType == PlanetGravity.type.WhiteHole)
        {
            Color color = Color.white;
            planet.color = color;
            color = Color.grey;
            glow.color = color;
        }
    }
}
