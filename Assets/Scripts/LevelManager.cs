using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNumber = 1;
    public bool lastLevel;
    public float[] starTimes = new float[3] { 60f, 40f, 30f };

    public void Seed()
    {
        Random.InitState(levelNumber * 1115);
    }

    void Start()
    {
        Seed();
        foreach (ColourPlanet colour in FindObjectsOfType<ColourPlanet>())
        {
            colour.Colour();
        }
    }
}
