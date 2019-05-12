using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobInitialise : MonoBehaviour
{
    void Start()
    {
        string appId = "ca-app-pub-3563227024265510~4121574348";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
    }
}
