using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisePlayer : MonoBehaviour
{
    public int shipIndex;
    public int colourIndex;

    public Color[] colors;
    public Sprite[] ships;

    void Start()
    {
        if (PlayerPrefs.HasKey("shipIndex"))
        {
            GetComponent<SpriteRenderer>().sprite = ships[PlayerPrefs.GetInt("shipIndex")];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ships[0];
        }
        if (PlayerPrefs.HasKey("colorIndex"))
        {
            GetComponent<Colourise>().color = colors[PlayerPrefs.GetInt("colorIndex")];
        }
        else
        {
            GetComponent<Colourise>().color = colors[0];
        }
        GetComponent<Colourise>().Start();
    }
}
