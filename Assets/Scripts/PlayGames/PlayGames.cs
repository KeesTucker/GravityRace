using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Death()
    {

        if (PlayerPrefs.HasKey("Deathes"))
        {
            PlayerPrefs.SetInt("Deathes", PlayerPrefs.GetInt("Deathes") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Deathes", 1);
        }

        Social.ReportScore(PlayerPrefs.GetInt("Deathes"), "CgkIz7Kul8IFEAIQEA", (bool success) => {
            // handle success or failure
        });

    }

    public void GameComplete(int stars)
    {

        if (PlayerPrefs.HasKey("GamesPlayed"))
        {
            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }

        Social.ReportScore(PlayerPrefs.GetInt("GamesPlayed"), "CgkIz7Kul8IFEAIQDw", (bool success) => {
            // handle success or failure
        });

#if UNITY_ANDROID
#endif
    }
}
