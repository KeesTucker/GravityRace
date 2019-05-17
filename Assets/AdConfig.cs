﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdConfig : MonoBehaviour
{
    public GameObject standard;
    public GameObject banner;
    public GameObject none;

    public GameObject current;

    public Sprite selected;
    public Sprite circle;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("AdConfig"))
        {
            if (PlayerPrefs.GetInt("AdConfig") == 0)
            {
                standard.GetComponent<Image>().sprite = selected;
                current = standard;
            }
            else if (PlayerPrefs.GetInt("AdConfig") == 1)
            {
                banner.GetComponent<Image>().sprite = selected;
                current = banner;
            }
            else if (PlayerPrefs.GetInt("AdConfig") == 2)
            {
                none.GetComponent<Image>().sprite = selected;
                current = none;
            }
            else
            {
                standard.GetComponent<Image>().sprite = selected;
                PlayerPrefs.SetInt("AdConfig", 0);
                current = standard;
            }
        }
        else
        {
            standard.GetComponent<Image>().sprite = selected;
            PlayerPrefs.SetInt("AdConfig", 0);
            current = standard;
        }
    }

    public void Standard()
    {
        current.GetComponent<Image>().sprite = circle;
        standard.GetComponent<Image>().sprite = selected;
        PlayerPrefs.SetInt("AdConfig", 0);
        current = standard;
        FindObjectOfType<ShowAds>().HideBanner();
    }

    public void Banner()
    {
        current.GetComponent<Image>().sprite = circle;
        banner.GetComponent<Image>().sprite = selected;
        PlayerPrefs.SetInt("AdConfig", 1);
        banner = standard;
        FindObjectOfType<ShowAds>().StartBanner();
    }

    public void NoAds()
    {
        FindObjectOfType<IAPManager>().RemoveAds();
    }

    public void SuccessNoAds()
    {
        current.GetComponent<Image>().sprite = circle;
        none.GetComponent<Image>().sprite = selected;
        PlayerPrefs.SetInt("AdConfig", 2);
        none = standard;
        FindObjectOfType<ShowAds>().HideBanner();
    }
}
