using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsScript : MonoBehaviour
{
    public static AdsScript ins;
    public BannerView bannerView;

    public GameObject Ads;

    void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("noAds") == 0)
        {
            RequestBanner();
            bannerView.Show();
        }
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-1993244347703127/3853857294";
        #elif UNITY_EDITOR
                string adUnitId = "unused";
        #elif UNITY_IPHONE
                        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
                        string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        AdSize adSize = new AdSize(360, 50);
        bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom); ;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
                .AddTestDevice("9565C07D9D79A32DCE422450163F1408")  // My test device.
                .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void ShowAd()
    {
        if (PlayerPrefs.GetInt("noAds") == 0)
            ShowRewardedAd();
        else
            NoAds();
    }

    public void NoAds()
    {
        PlayerPrefs.SetInt("BonusBiscuits", 2);
        GameObject.Find("Ads").SetActive(false);
        PlayerPrefs.SetInt("ShowAd", 6);
        PlayerPrefs.Save();
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                PlayerPrefs.SetInt("BonusBiscuits", 2);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                PlayerPrefs.SetInt("BonusBiscuits", 1);
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                PlayerPrefs.SetInt("BonusBiscuits", 1);
                break;
        }
        GameObject.Find("Ads").SetActive(false);
        PlayerPrefs.SetInt("ShowAd", 6);
        PlayerPrefs.Save();
    }
}