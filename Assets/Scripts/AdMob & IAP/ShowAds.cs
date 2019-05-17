using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardBasedVideoAd reward;
    private BannerView banner;
    private bool requested = false;
    private bool rewardRequested = false;

    void Start()
    {
        this.reward = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        reward.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        reward.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        reward.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        reward.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        reward.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        reward.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        reward.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestReward();
        this.RequestBanner();

        if (PlayerPrefs.HasKey("AdConfig"))
        {
            if (PlayerPrefs.GetInt("AdConfig") == 1)
            {
                this.banner.Show();
            }
        }
    }

    void Update()
    {
        if (requested)
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            requested = false;
        }
        if (rewardRequested)
        {
            if (this.reward.IsLoaded())
            {
                this.reward.Show();
            }
            rewardRequested = false;
        }
    }

    private void RequestReward()
    {
        string adUnitId = "ca-app-pub-3563227024265510/9335485964";

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.reward.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestReward();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        if (PlayerPrefs.HasKey("Stars"))
        {
            PlayerPrefs.SetInt("Stars", (int)amount + PlayerPrefs.GetInt("Stars"));
        }
        else
        {
            PlayerPrefs.SetInt("Stars", (int)amount);
        }
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3563227024265510/7186721384";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3563227024265510/1203389669";

        this.banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.banner.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.banner.OnAdFailedToLoad += HandleOnAdFailedToLoadB;
        // Called when an ad is shown.
        this.banner.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.banner.OnAdClosed += HandleOnAdClosedB;
        // Called when the ad click caused the user to leave the application.
        this.banner.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.banner.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
    }

    public void HandleOnAdClosedB(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        if (banner != null)
        {
            banner.Destroy();
        }
        RequestBanner();
    }

    public void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
    }

    public void HandleOnAdFailedToLoadB(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        if (banner != null)
        {
            banner.Destroy();
        }
    }

    public void HandleOnAdOpened(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdLeavingApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void GameOver()
    {
        this.RequestInterstitial();
        requested = true;
    }

    public void StartBanner()
    {
        this.banner.Show();
    }
    public void HideBanner()
    {
        this.banner.Hide();
    }

    public void Reward()
    {
        rewardRequested = true;
    }
}
