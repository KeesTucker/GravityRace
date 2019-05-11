using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SoundToggle();
    }

    public void SoundToggle()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                GetComponent<AudioSource>().volume = 0f;
            }
            else
            {
                GetComponent<AudioSource>().volume = 0.8f;
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.8f;
        }
    }
}
