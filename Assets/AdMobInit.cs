using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdmobInit : MonoBehaviour, IRewardedVideoAdListener
{
    // Start is called before the first frame update
    void Start()
    {
        Appodeal.initialize("9f464a30aac4e63eb2aea433229e472ccfaabe27369ba71e", Appodeal.REWARDED_VIDEO, true);
        Appodeal.setRewardedVideoCallbacks(this);
    }

    void Update()
    {
        showRewardedVideo();
    }

    public void showRewardedVideo()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    #region Rewarded Video callback handlers

    public void onRewardedVideoLoaded(bool isPrecache)
    {
        print("Appodeal. Video loaded");
    }

    public void onRewardedVideoFailedToLoad()
    {
        print("Appodeal. Video failed");
    }

    public void onRewardedVideoShowFailed()
    {
        print("Appodeal. RewardedVideo show failed");
    }

    public void onRewardedVideoShown()
    {
        print("Appodeal. Video shown");
    }

    public void onRewardedVideoClosed(bool finished)
    {
        print("Appodeal. Video closed");
    }

    public void onRewardedVideoFinished(double amount, string rewardedName)
    {
        print("Appodeal. Reward: " + amount + " " + rewardedName);
    }

    public void onRewardedVideoExpired()
    {
        print("Appodeal. Video expired");
    }

    public void onRewardedVideoClicked()
    {
        print("Appodeal. Video clicked");
    }

    #endregion
}
