using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Monetization;

public class AdmobInitialise : MonoBehaviour
{
    string gameId = "3150977";
    bool testMode = true;

    void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-3563227024265510~4121574348";
        #elif UNITY_EDITOR
            string appId = "unused";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }
}
