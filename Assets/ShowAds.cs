using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowAds : MonoBehaviour
{
    private InterstitialAd interstitial;

    void Start()
    {
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        this.interstitial.OnAdClosed += HandleOnAdClosed;

        void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }
    }

    public void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
}
