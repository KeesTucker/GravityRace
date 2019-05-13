using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public bool onOff = true;
    public Image image;
    public bool isSound;
    public GameObject about;

    void Start()
    {
        if (isSound)
        {
            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 0)
                {
                    onOff = false;
                    image.color = Color.gray;
                }
                else
                {
                    onOff = true;
                    image.color = Color.white;
                }
            }
            FindObjectOfType<Audio>().SoundToggle();
        }
    }

    public void Sound()
    {
        if (onOff == true)
        {
            PlayerPrefs.SetInt("Sound", 0);
            onOff = false;
            image.color = Color.gray;
        }
        else if (onOff == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
            onOff = true;
            image.color = Color.white;
        }
        FindObjectOfType<Audio>().SoundToggle();
    }

    public void OpenAbout()
    {
        about.SetActive(true);
    }
    public void CloseAbout()
    {
        about.SetActive(false);
    }

    public void NoAds()
    {
        FindObjectOfType<IAPManager>().RemoveAds();
    }
}
