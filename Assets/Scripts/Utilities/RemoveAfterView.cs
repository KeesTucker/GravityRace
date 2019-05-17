using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterView : MonoBehaviour
{
    void Start()
    {
        if (gameObject.name == "Info")
        {
            if (PlayerPrefs.HasKey("viewed"))
            {
                gameObject.SetActive(false);
            }
        }
        if (gameObject.name == "StarsInfo")
        {
            if (PlayerPrefs.HasKey("viewed"))
            {
                if (PlayerPrefs.GetInt("viewed") != 1)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        if (gameObject.name == "AdsInfo")
        {
            if (PlayerPrefs.HasKey("viewed"))
            {
                if (PlayerPrefs.GetInt("viewed") != 2)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void StoreOpened()
    {
        if (!PlayerPrefs.HasKey("viewed"))
        {
            PlayerPrefs.SetInt("viewed", 1);
            gameObject.SetActive(false);
        }
    }

    public void IAPOpened()
    {
        if (PlayerPrefs.GetInt("viewed") == 1)
        {
            PlayerPrefs.SetInt("viewed", 2);
            gameObject.SetActive(false);
        }
    }

    public void AdsOpened()
    {
        if (PlayerPrefs.GetInt("viewed") == 2)
        {
            PlayerPrefs.SetInt("viewed", 3);
            gameObject.SetActive(false);
        }
    }
}
