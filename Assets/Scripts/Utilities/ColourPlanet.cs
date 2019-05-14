using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPlanet : MonoBehaviour
{
    public SpriteRenderer planet;
    public SpriteRenderer[] glow;

    public Sprite[] sprites;

    public Sprite circle;

    public PlanetGravity planetGrav;

    public GameObject shadow;

    public void Colour()
    {
        if (planetGrav.bodyType == PlanetGravity.type.Planet)
        {
            Color color = Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0.2f, 0.6f), Random.Range(0.5f, 1f));
            planet.color = color;
            //planet.color = Color.white;
            Color.RGBToHSV(color, out float h, out float s, out float v);
            //planet.sprite = sprites[Random.Range(0, sprites.Length)];
            s = Mathf.Clamp(s - 0.2f, 0, 1f);
            v = Mathf.Clamp(v + 0.2f, 0, 1f);
            color = Color.HSVToRGB(h, s, v);
            //glow.color = color;
            planet.transform.GetChild(1).gameObject.SetActive(true);
            shadow.SetActive(true);
        }
        else if (planetGrav.bodyType == PlanetGravity.type.BlackHole)
        {
            Color color = Color.black;
            planet.color = color;
            planet.sprite = circle;
            color = Color.black;

            for (int i = 0; i < glow.Length; i++)
            {
                glow[i].color = color;
            }
            
            planet.transform.GetChild(1).gameObject.SetActive(false);
            shadow.SetActive(false);
        }
        else if (planetGrav.bodyType == PlanetGravity.type.WhiteHole)
        {
            Color color = Color.white;
            planet.color = color;
            planet.sprite = circle;
            color = Color.grey;
            for (int i = 0; i < glow.Length; i++)
            {
                glow[i].color = color;
            }
            planet.transform.GetChild(1).gameObject.SetActive(false);
            shadow.SetActive(false);
        }
    }
}
