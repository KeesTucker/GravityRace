using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobInitialise : MonoBehaviour
{
    void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-3563227024265510~4121574348";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
    }
}
