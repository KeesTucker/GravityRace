using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

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
        if (gameObject.name == "NoAds")
        {
            if (PlayerPrefs.HasKey("NoAds"))
            {
                if (PlayerPrefs.GetInt("NoAds") == 1)
                {
                    GetComponent<Button>().interactable = false;
                }
            }
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

    public void AdIAP()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("AdIAP");
    }

    public void RewardStars()
    {
        //FindObjectOfType<ShowAds>().Reward();
    }

    public void Store()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("Store");
    }

    public void Levels()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("Levels");
    }

    public void OpenStore()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("IAP");
    }

    public void Buy30Stars()
    {
    }

    public void Buy60Stars()
    {
    }

    public void Buy120Stars()
    {
    }

    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/afunroyalegame");
    }

    public void Privacy()
    {
        Application.OpenURL("https://gravity-zen.flycricket.io/privacy.html");
    }

    public void Leaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}
