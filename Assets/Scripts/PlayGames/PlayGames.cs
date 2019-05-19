using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class PlayGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
            }
        });
    }

    public void Death()
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQCA", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQCQ", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQCg", 1, (bool success) => {
            // handle success or failure
        });

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

        PlayGamesPlatform.Instance.Events.IncrementEvent("CgkIz7Kul8IFEAIQAg", 1);
    }

    public void GameComplete(int stars)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQBA", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQBw", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQBQ", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQBg", 1, (bool success) => {
            // handle success or failure
        });

        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQCw", stars, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQDA", stars, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIz7Kul8IFEAIQDQ", stars, (bool success) => {
            // handle success or failure
        });

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

        PlayGamesPlatform.Instance.Events.IncrementEvent("CgkIz7Kul8IFEAIQAQ", (uint)stars);
        PlayGamesPlatform.Instance.Events.IncrementEvent("CgkIz7Kul8IFEAIQAw", 1);
    }
}
