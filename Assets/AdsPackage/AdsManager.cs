using System;
using UnityEngine; 
using GameAnalyticsSDK; 


public class AdsManager : MonoBehaviour
{
    [Header("Max IDS")]
    public string MaxSdkKey = "3eOdThMf08WPwr9aZS2PJyio9Pv8HrHLUfzyMKcMS7mMyvyWM_xfxdS52MEKBVffAdcWyEuOMdmIXiqvilTeN0";

    public string simpleBannerAdUnitId = "ENTER_BANNER_AD_UNIT_ID_HERE";
  
    public string InterstitialAdUnitId = "0bf5dd259a7babe3";
    public string RewardedAdUnitId = "5d75002bbc4126b9";
    //[HideInInspector]
    public string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";
    [Header("Admob IDS")]
    public string Admob_App_Id = "ENTER_MREC_AD_UNIT_ID_HERE";
    public string Admob_Banner_Simple = "ENTER_MREC_AD_UNIT_ID_HERE";
    public string Admob_BigBanner = "ENTER_MREC_AD_UNIT_ID_HERE";
    [HideInInspector]
    public string Admob_Banner_Med = "ENTER_MREC_AD_UNIT_ID_HERE";
    [HideInInspector]
    public string Admob_Banner_High = "ENTER_MREC_AD_UNIT_ID_HERE";
    public string Admob_Inter_Id = "ENTER_BANNER_AD_UNIT_ID_HERE";
    public string Admob_Rewarded_Id = "ENTER_MREC_AD_UNIT_ID_HERE";
    public string Admob_AppOpen_Id = "ENTER_MREC_AD_UNIT_ID_HERE";
    public BannerPosition bannerPosition;
    public BannerSize bannerSize;
  
    [HideInInspector]
    public BannerPosition rectbannerPosition;
    [HideInInspector]
    public BannerSize rectbannerSize;

    [HideInInspector]
    public bool IsRewardedLoaded = false;
    private bool _isBannerShowing, _isBannerReady;
    private bool _isFlooringBannerReady, _isFlooringBannerShowing;
    private bool isMRecShowing;

    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    public static AdsManager Insatance;

    public static float IdleScreenADThreshold = 30;
    public static int InGameAdTextTimer = 3;

    private void Awake()
    {

        if (Insatance != this && Insatance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Insatance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        GameAnalytics.Initialize();
        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.InitializeSdk();
    }

    void Start()
    {


        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
           
            if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1)
            {
                return;
            }
            Debug.Log("MAX SDK Initialized");
            InitializeRewardedAds(); 
            InitializeInterstitialAds();
            InitializeBannerAds();
            InitializeMRecAds();
        };


        // Admob Ids initialized 
        AdMob.Instance.appId = Admob_App_Id;
        AdMob.Instance.appOpenID = Admob_AppOpen_Id;
        AdMob.Instance.bannerID = Admob_Banner_Simple;
        AdMob.Instance.BigbannerID = Admob_BigBanner;
        // AdMob.Instance.rectbannerID = Admob_RectBanner_Id;
        AdMob.Instance.interstitialID = Admob_Inter_Id;
        AdMob.Instance.rewardedID = Admob_Rewarded_Id;
        AdMob.Instance.bannerPosition = bannerPosition;
        AdMob.Instance.bannerSize = bannerSize;    
    }

    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += Interstitial_OnAdDisplayedEvent; ;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += AnalyticsManager.instance.Revenue_ReportMax;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void Interstitial_OnAdDisplayedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        AdMob.Instance.OnAdOpeningEvent.Invoke();
    }

    void LoadInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
            MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }
    public bool IsMaxInterstiatialAvailable()
    {
        return MaxSdk.IsInterstitialReady(InterstitialAdUnitId);
    }
    public void ShowInterstitial(string placement)
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1)
        {
            AdMob.Instance.ReplayAfterAds();
            return;
        }
        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {
            AdMob.Instance.isInterstialAdPresent = true;
            AnalyticsManager.instance.InterstitialEvent(placement);
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            AdMob.Instance.StartTimer();
          //  Debug.LogError("IsInterstitialReadyMaxSdk");
        }
        else
        {
          //  Debug.LogError("ShowInterstitial");
            AnalyticsManager.instance.InterstitialEvent(placement);
            AdMob.Instance.ShowInterstitial();
        }
    }


    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
        // interstitialStatusText.text = "Loaded";
        //    Debug.Log("Interstitial loaded");

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Debug.Log(adUnitId + "InterstitialAdUnitId");
        // interstitialStatusText.text = "Failed load: " + errorCode + "\nRetrying in 3s...";
        //Debug.Log("Interstitial failed to load with error code: " + errorInfo);

        // Interstitial ad failed to load. We recommend retrying with exponentially higher delays.
        AdMob.Instance.OnAdInterAdFailedToShow.Invoke();
        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, interstitialRetryAttempt);

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        Debug.Log("Interstitial failed to display with error code: " + errorInfo);
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {   
        Debug.Log("Interstitial dismissed");

        AdMob.OnIntAdClosed.Invoke();
        LoadInterstitial();
    }
    #endregion

    #region Rewarded Ad Methods

    private void InitializeRewardedAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += AnalyticsManager.instance.Revenue_ReportMax;



        // Load the first RewardedAd
        LoadRewardedAd();
    }
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }
    public bool IsRewardedAvailable()
    {
        return MaxSdk.IsRewardedAdReady(RewardedAdUnitId);
    }
    public void ShowRewardedAd()
    {
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
        {
            AdMob.Instance.isInterstialAdPresent = true;
            MaxSdk.ShowRewardedAd(RewardedAdUnitId);
        }
        else
        {
            AdMob.Instance.ShowRewardedAdmob();
        }
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

        Debug.Log("Rewarded ad loaded");
        IsRewardedLoaded = true;
        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // rewardedStatusText.text = "Failed load: " + errorCode + "\nRetrying in 3s...";
        Debug.Log("Rewarded ad failed to load with error code: " + errorInfo);

        // Rewarded ad failed to load. We recommend retrying with exponentially higher delays.

        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, rewardedRetryAttempt);
        IsRewardedLoaded = false;
        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {

        // Rewarded ad failed to display. We recommend loading the next ad
        Debug.Log("Rewarded ad failed to display with error code: " + adInfo);
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad displayed");
        AdMob.Instance.OnAdOpeningEvent.Invoke();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad clicked");
    }

    private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        Debug.Log("Rewarded ad dismissed");
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        AdMob.Instance.OnUserEarnedRewardEvent?.Invoke();
        //Controller.Instance.ActionVideo(true);
        // Rewarded ad was displayed and user should receive the reward
        Debug.Log("Rewarded ad received reward");
    }

    #endregion

    #region Banner Ad Methods

    private void InitializeBannerAds()
    {
       // Debug.Log("InitializeBannerAds Max");
        MaxSdk.CreateBanner(simpleBannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerBackgroundColor(simpleBannerAdUnitId, Color.black);
        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += AnalyticsManager.instance.Revenue_ReportMax;

        
    }

    public void ToggleBannerVisibility()
    {
        if (!_isBannerShowing)
        {
            MaxSdk.ShowBanner(simpleBannerAdUnitId);
            // showBannerButton.GetComponentInChildren<Text>().text = "Hide Banner";
        }
        else
        {
            MaxSdk.HideBanner(simpleBannerAdUnitId);
            // showBannerButton.GetComponentInChildren<Text>().text = "Show Banner";
        }

        _isBannerShowing = !_isBannerShowing;
    }
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        MaxSdk.ShowBanner(simpleBannerAdUnitId);
       // Debug.Log("Max_Simplebanner Show");
    }

    public void HideBanner()
    {
        _isBannerReady = false;
        MaxSdk.HideBanner(simpleBannerAdUnitId);
       // Debug.Log("HideSimpleBanner Max");
    }
    public void DestroyBanner()
    {
        _isBannerReady = false;
        MaxSdk.DestroyBanner(simpleBannerAdUnitId);
        // Debug.Log("Max banner is Destroy");
    }
    public bool IsBannerAdAvailable()
    {
        return _isBannerReady;
    }
    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Max banner is loaded" + _isBannerReady);
        _isBannerReady = true;
    }
    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
       // Debug.Log(errorInfo.AdLoadFailureInfo);
       // Debug.Log("Max banner is failed");
        _isBannerReady = false;
    }
    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
       // Debug.Log("Max banner is collapsed");
        _isBannerReady = false;
    }





    #endregion

    #region MREC Ad Methods

    private void InitializeMRecAds()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        // MRECs are automatically sized to 300x250.
        MaxSdk.CreateMRec(MRecAdUnitId, MaxSdkBase.AdViewPosition.Centered);
    }

    private void ToggleMRecVisibility()
    {
        if (!isMRecShowing)
        {
            MaxSdk.ShowMRec(MRecAdUnitId);
        }
        else
        {
            MaxSdk.HideMRec(MRecAdUnitId);
        }

        isMRecShowing = !isMRecShowing;
    }
    public void ShowMREC()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        print("MRec Showing");

        if (AdMob.Instance.IsBigBannerOnAdmob)
        {
            AdMob.Instance.ShowBigBanner();
        }
        else 
        {
            MaxSdk.ShowMRec(MRecAdUnitId);
        }
       
    }
    public void HideMREC()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;

        if (AdMob.Instance.IsBigBannerOnAdmob)
        {
            AdMob.Instance.HideBigBanner();
        }
        else
        {
            
            MaxSdk.HideMRec(MRecAdUnitId);
        }
       
    }
    #endregion
}