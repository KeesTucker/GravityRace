using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public bool onOff = true;
    public Sprite on;
    public Sprite off;
    public Image image;

    void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                onOff = false;
                image.sprite = off;
            }
            else
            {
                onOff = true;
                image.sprite = on;
            }
        }
        FindObjectOfType<Audio>().SoundToggle();
    }

    public void Sound()
    {
        if (onOff == true)
        {
            PlayerPrefs.SetInt("Sound", 0);
            onOff = false;
            image.sprite = off;
        }
        else if (onOff == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
            onOff = true;
            image.sprite = on;
        }
        FindObjectOfType<Audio>().SoundToggle();
    }
}
