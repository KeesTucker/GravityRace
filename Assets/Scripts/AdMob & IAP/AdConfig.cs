using System.Collections;
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
                if (standard)
                {
                    standard.GetComponent<Image>().sprite = selected;
                    current = standard;
                }
                
                FindObjectOfType<ShowAds>().HideBanner();
                
            }
            else if (PlayerPrefs.GetInt("AdConfig") == 1)
            {
                if (banner)
                {
                    banner.GetComponent<Image>().sprite = selected;
                    current = banner;
                }
                
                FindObjectOfType<ShowAds>().StartBanner();
                
            }
            else if (PlayerPrefs.GetInt("AdConfig") == 2)
            {
                if (none)
                {
                    none.GetComponent<Image>().sprite = selected;
                    standard.GetComponent<Button>().interactable = false;
                    banner.GetComponent<Button>().interactable = false;
                    none.GetComponent<Button>().interactable = false;
                    current = none;
                }
                
                FindObjectOfType<ShowAds>().HideBanner();
                
            }
            else if (standard)
            {
                standard.GetComponent<Image>().sprite = selected;
                PlayerPrefs.SetInt("AdConfig", 0);
                current = standard;
            }
            else
            {
                PlayerPrefs.SetInt("AdConfig", 0);
            }
        }
        else
        {
            if (standard)
            {
                standard.GetComponent<Image>().sprite = selected;
                current = standard;
            }
            
            PlayerPrefs.SetInt("AdConfig", 0);
            
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
        current = banner;
        FindObjectOfType<ShowAds>().StartBanner();
    }

    public void NoAds()
    {
        FindObjectOfType<IAPManager>().RemoveAds();
    }

    public void SuccessNoAds()
    {
        if (current)
        {
            current.GetComponent<Image>().sprite = circle;
            none.GetComponent<Image>().sprite = selected;
            PlayerPrefs.SetInt("AdConfig", 2);
            current = none;
            FindObjectOfType<ShowAds>().HideBanner();
            standard.GetComponent<Button>().interactable = false;
            banner.GetComponent<Button>().interactable = false;
            none.GetComponent<Button>().interactable = false;
        }
    }
}
