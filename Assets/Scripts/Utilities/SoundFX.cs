using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource source;
    public AudioSource sourceRocket;
    public AudioClip death;
    public AudioClip win;

    void Start()
    {
        sourceRocket.volume = 0;
    }

    public void Death()
    {
        source.clip = death;
        source.Play();
    }
    public void Win()
    {
        source.clip = win;
        source.Play();
    }

    public IEnumerator RocketOn()
    {
        if (sourceRocket.volume != 1)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForEndOfFrame();
                sourceRocket.volume += 0.05f;
            }
            sourceRocket.volume = 1;
        }
    }
    public IEnumerator RocketOff()
    {
        if (sourceRocket.volume != 0)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForEndOfFrame();
                sourceRocket.volume -= 0.05f;
            }
            sourceRocket.volume = 0;
        }
    }
}
