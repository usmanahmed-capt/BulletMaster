using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;  
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
// Example script showing how to invoke the Google Mobile Ads Unity plugin.

public enum BannerPosition { Bottom, Top, TopLeft, TopRight, BottomLeft, BottomRight, Center };
public enum BannerSize { Banner, SmartBanner, MediumRectangle, IABBanner, Leaderboard, Adaptive };

public class AdMob : MonoBehaviour
{
    [HideInInspector]
    private string outputMessage = "";
    [HideInInspector]
    public string bannerID, BigbannerID, bannerIDMed, bannerIDHigh, interstitialID, InterHighFloorID, InterMediumFloorID, rewardedID, appOpenID, appId;
    [HideInInspector]
    public bool isInterstialAdPresent = false;
    [HideInInspector]
    public enum RequestFloorType
    {
        High,
        Meduim,
        Simple
    }
    [HideInInspector]
    public RequestFloorType FloorType;
    [HideInInspector]
    public BannerSize RectbannerSize;

    private AdPosition adPosition;
    private AdPosition adPositionRect;
    private AdSize adSize;
    private AdSize adSizeRect;
    //public static string appId;

    //private static InterstitialAd interstitialHighFloor;
    //private static InterstitialAd interstitialMediumfloor;


    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private BannerView bannerViewBig;

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;

    private float deltaTime;
    [HideInInspector]
    public UnityEvent OnAdLoadedEvent;
    [HideInInspector]
    public UnityEvent OnAdFailedToLoadEvent;
    [HideInInspector]
    public UnityEvent OnAdOpeningEvent;
    [HideInInspector]
    public UnityEvent OnAdFailedToShowEvent;
    [HideInInspector]
    public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector]
    public UnityEvent OnAdClosedEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnAdInterAdFailedToShow = new UnityEvent();

    [HideInInspector]
    public UnityEvent HideAppOpen = new UnityEvent();

    [HideInInspector]
    public static UnityEvent OnIntAdClosed = new UnityEvent();
    // public Text statusText;
    public static AdMob Instance;
    #region UNITY MONOBEHAVIOR METHODS


    [HideInInspector]
    public BannerPosition bannerPosition;
    [HideInInspector]
    public BannerSize bannerSize;
    [HideInInspector]
    public BannerPosition rectbannerPosition;
    [HideInInspector]
    public BannerSize rectbannerSize;

    public int AdsTimerThreshold = 30;
    public int timer;
    public static bool ShowAd;
    public bool IsBigBannerOnAdmob;
    IEnumerator timercoroutine;
    public void StartTimer()
    {
        timer = 0;
        ShowAd = false;
        if (timercoroutine != null)
        {
            StopCoroutine(timercoroutine);
            timercoroutine = null;
        }
        timercoroutine = AdTimer();
        StartCoroutine(timercoroutine);
    }
    IEnumerator AdTimer()
    {
        yield return new WaitForSeconds(1);
        timer++;
        //Debug.Log("Ads Timer: " + timer);
        if (timer >= AdsTimerThreshold)
        {
            ShowAd = true;
            timer = 0;
        }
        else
        {
            timercoroutine = AdTimer();
            StartCoroutine(timercoroutine);
        }
    }

    public void Awake()
    {
     //   PlayerPrefs.SetInt("RemoveAdsOnly",1);

        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


    }
    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif
        BannerSpecs();
        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);


        OnUserEarnedRewardEvent.AddListener(delegate
        {
            AdsManager.Insatance.ShowBanner();
            Invoke(nameof(ClaiRewardCaller), .2f);
        });

        OnAdInterAdFailedToShow.AddListener(delegate
        {
            AdsManager.Insatance.ShowBanner();
            Invoke(nameof(ReplayAfterAds), .2f);
           
        });

        OnIntAdClosed.AddListener(delegate
        {
            AdsManager.Insatance.ShowBanner();
            Invoke(nameof(ReplayAfterAds), .2f);
        });

        OnAdOpeningEvent.AddListener(delegate
        {
            AdsManager.Insatance.HideBanner();
        });

        HideAppOpen.AddListener(delegate
        {
            StartTimer();
        });
        StartTimer();
    }

    void ClaiRewardCaller()
    {
        StartTimer();
        if (GameController.Instance.CallingIndux == 1)
        {
            GameUI.instace.AfterClarCallBackFun();
        }
        if (GameController.Instance.CallingIndux == 2)
        {
            GameUI.instace.AfterClaimingBullets();
        }
        if (GameController.Instance.CallingIndux == 3)
        {
            GameUI.instace.AfterSkipLvl();
        }
    }

    internal void ReplayAfterAds()
    {
        Time.timeScale = 1;
        if (GameController.Instance.AfterCallingInterstatialads == 1)
        {
            GameUI.instace.gameManager.NextCallingAfterAds();
        }
        if (GameController.Instance.AfterCallingInterstatialads == 2)
        {
            GameUI.instace.gameManager.Restart_Fun();
        }
        if (GameController.Instance.AfterCallingInterstatialads == 3)
        {
            GameUI.instace.gameManager.Exit_Fun();
        }
        GameController.Instance.AfterCallingInterstatialads = 0;
    }
    public void BannerSpecs()
    {
        switch (bannerPosition)
        {
            case BannerPosition.Bottom:
                adPosition = AdPosition.Bottom;
                break;
            case BannerPosition.Top:
                adPosition = AdPosition.Top;
                break;
            case BannerPosition.TopLeft:
                adPosition = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                adPosition = AdPosition.TopRight;
                break;
            case BannerPosition.BottomLeft:
                adPosition = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                adPosition = AdPosition.BottomRight;
                break;
            case BannerPosition.Center:
                adPosition = AdPosition.Center;
                break;
        }

        switch (rectbannerPosition)
        {
            case BannerPosition.Bottom:
                adPositionRect = AdPosition.Bottom;
                break;
            case BannerPosition.Top:
                adPositionRect = AdPosition.Top;
                break;
            case BannerPosition.TopLeft:
                adPositionRect = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                adPositionRect = AdPosition.TopRight;
                break;
            case BannerPosition.BottomLeft:
                adPositionRect = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                adPositionRect = AdPosition.BottomRight;
                break;
            case BannerPosition.Center:
                adPositionRect = AdPosition.Center;
                break;
        }
        switch (RectbannerSize)
        {
            case BannerSize.Banner:
                adSizeRect = AdSize.Banner;
                break;
            case BannerSize.SmartBanner:
                adSizeRect = AdSize.SmartBanner;
                break;
            case BannerSize.MediumRectangle:
                adSizeRect = AdSize.MediumRectangle;
                break;
            case BannerSize.IABBanner:
                adSizeRect = AdSize.IABBanner;
                break;
            case BannerSize.Leaderboard:
                adSizeRect = AdSize.Leaderboard;
                break;
            case BannerSize.Adaptive:

                //float widthInPixels = Screen.safeArea.width > 0 ? Screen.safeArea.width : Screen.width;
                //int width = (int)(widthInPixels / MobileAds.Utils.GetDeviceScale());
                //MonoBehaviour.print("requesting width: " + width.ToString());
                adSizeRect = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

                break;
        }

        switch (bannerSize)
        {
            case BannerSize.Banner:
                adSize = AdSize.Banner;
                break;
            case BannerSize.SmartBanner:
                adSize = AdSize.SmartBanner;
                break;
            case BannerSize.MediumRectangle:
                adSize = AdSize.MediumRectangle;
                break;
            case BannerSize.IABBanner:
                adSize = AdSize.IABBanner;
                break;
            case BannerSize.Leaderboard:
                adSize = AdSize.Leaderboard;
                break;
            case BannerSize.Adaptive:

                //float widthInPixels = Screen.safeArea.width > 0 ? Screen.safeArea.width : Screen.width;
                //int width = (int)(widthInPixels / MobileAds.Utils.GetDeviceScale());
                //MonoBehaviour.print("requesting width: " + width.ToString());
                adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

                break;
        }
    }
    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
          //  RequestAndLoadRewardedAd();
            Invoke(nameof(RequestAndLoadRewardedAd), 3f);
            if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
            RequestAndLoadAppOpenAd();
            RequestAndLoadInterstitialAd(); 
           // Invoke(nameof(ShowAppOpenAd), 1f);
        });
    }



    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {
        Debug.Log("Requesting Banner ad.");

        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Add Event Handlers
        bannerView.OnBannerAdLoaded += () =>
        {
            PrintStatus("Banner ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("Banner ad failed to load with error: " + error.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        bannerView.OnAdImpressionRecorded += () =>
        {
            PrintStatus("Banner ad recorded an impression.");
        };
        bannerView.OnAdClicked += () =>
        {
            PrintStatus("Banner ad recorded a click.");
        };
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("Banner ad opening.");
          //  OnAdOpeningEvent.Invoke();
        };
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            RequestBannerAd();
            PrintStatus("Banner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "Banner");
        };
       

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }

    public void RequestBannerAdBigBanner()
    {
        Debug.Log("Requesting BigBanner ad.");

        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        // Clean up banner before reusing
        if (bannerViewBig != null)
        {
            bannerViewBig.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerViewBig = new BannerView(BigbannerID, AdSize.MediumRectangle, AdPosition.Center);

        // Add Event Handlers
        bannerViewBig.OnBannerAdLoaded += () =>
        {
            PrintStatus("BigBanner ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        bannerViewBig.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("BigBanner ad failed to load with error: " + error.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        bannerViewBig.OnAdImpressionRecorded += () =>
        {
            PrintStatus("BigBanner ad recorded an impression.");
        };
        bannerViewBig.OnAdClicked += () =>
        {
            PrintStatus("BigBanner ad recorded a click.");
        };
        bannerViewBig.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("BigBanner ad opening.");
           // OnAdOpeningEvent.Invoke();
        };
        bannerViewBig.OnAdFullScreenContentClosed += () =>
        {
            RequestBannerAdBigBanner();
            PrintStatus("BigBanner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        bannerViewBig.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("BigBanner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "BigBanner");
        };


        // Load a banner ad
        bannerViewBig.LoadAd(CreateAdRequest());
    }

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        if (bannerView != null)
        {
            bannerView.Show();
        }
        else
        {
            RequestBannerAd();
        }
    }

    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
            this.bannerView = null;
        }  
    }

    public void ShowBigBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        if (bannerViewBig != null)
        {
            bannerViewBig.Show();
        }
        else
        {
            RequestBannerAdBigBanner();
        }
    }

    public void HideBigBanner()
    {
        if (bannerViewBig != null)
        {
            bannerViewBig.Hide();
            this.bannerViewBig = null;
        }
    }

    #endregion

    #region RECTBANNER ADS

    //public void RequestRectBannerAd()
    //{
    //    Debug.Log("Requesting RectBanner ad.");
    //    // Debug.Log("RectbannerRequest");
    //    //        // These ad units are configured to always serve test ads.
    //    //#if UNITY_EDITOR
    //    //        string adUnitId = "unused";
    //    //#elif UNITY_ANDROID
    //    //        string adUnitId = bannerID;
    //    //#elif UNITY_IPHONE
    //    //        string adUnitId = bannerID;
    //    //#else
    //    //        string adUnitId = "unexpected_platform";
    //    //#endif

    //    // Clean up banner before reusing
    //    if (RectbannerView != null)
    //    {
    //        RectbannerView.Destroy();
    //    }

    //    // Create a 320x50 banner at top of the screen

    //    RectbannerView = new BannerView(rectbannerID, AdSize.MediumRectangle, AdPosition.BottomLeft);

    //    // Add Event Handlers
    //    RectbannerView.OnBannerAdLoaded += () =>
    //    {
    //        PrintStatus("Banner ad loaded.");
    //        OnAdLoadedEvent.Invoke();
    //    };
    //    RectbannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
    //    {
    //        PrintStatus("Banner ad failed to load with error: " + error.GetMessage());
    //        OnAdFailedToLoadEvent.Invoke();
    //    };
    //    RectbannerView.OnAdImpressionRecorded += () =>
    //    {
    //        PrintStatus("Banner ad recorded an impression.");
    //    };
    //    RectbannerView.OnAdClicked += () =>
    //    {
    //        PrintStatus("Banner ad recorded a click.");
    //    };
    //    RectbannerView.OnAdFullScreenContentOpened += () =>
    //    {
    //        PrintStatus("Banner ad opening.");
    //        OnAdOpeningEvent.Invoke();
    //    };
    //    RectbannerView.OnAdFullScreenContentClosed += () =>
    //    {
    //        PrintStatus("Banner ad closed.");
    //        OnAdClosedEvent.Invoke();
    //    };
    //    RectbannerView.OnAdPaid += (AdValue adValue) =>
    //    {
    //        string msg = string.Format("{0} (currency: {1}, value: {2}",
    //                                    "Banner ad received a paid event.",
    //                                    adValue.CurrencyCode,
    //                                    adValue.Value);
    //        PrintStatus(msg);
    //    };

    //    // Load a banner ad
    //    RectbannerView.LoadAd(CreateAdRequest());
    //}

    //public void DestroyRectBannerAd()
    //{
    //    if (RectbannerView != null)
    //    {
    //        RectbannerView.Destroy();
    //        this.RectbannerView = null;
    //    }
    //}
    //public void ShowRecBanner()
    //{
    //    if (RectbannerView != null)
    //    {
    //        RectbannerView.Show();
    //    }
    //    else
    //    {
    //        RequestRectBannerAd();
    //    }
    //}
    //public void HideRecBanner()
    //{
    //    if (RectbannerView != null)
    //    {
    //        RectbannerView.Hide();
    //        this.RectbannerView = null;
    //    }
    //}

    #endregion

    #region INTERSTITIAL ADS
    [HideInInspector]
    public bool Once;
    public bool isAppOpenShowOnStart;

    public void RequestAndLoadInterstitialAd()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        PrintStatus("Requesting Interstitial ad.");

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = interstitialID;
#elif UNITY_IPHONE
        string adUnitId = interstitialID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }


        // Load an interstitial ad
        InterstitialAd.Load(adUnitId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Interstitial ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Interstitial ad failed to load.");
                    return;
                }

                PrintStatus("Interstitial ad loaded.");
                interstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Interstitial ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Interstitial ad closed.");                  
                    RequestAndLoadInterstitialAd();
                    OnAdClosedEvent.Invoke();
                    OnIntAdClosed.Invoke();
                    //Data_GF.Instance.RewardedAdWatched();                 
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    OnAdInterAdFailedToShow.Invoke();
                    PrintStatus("Interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Interstitial ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "Interstitial");
                };             
            });
    }
    public bool IsAdmobInterstiatialAvailable()
    {
        return interstitialAd != null && interstitialAd.CanShowAd();  
    }
    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {         
            isInterstialAdPresent = true;
            interstitialAd.Show();
            StartTimer();
        }
        else
        {
            RequestAndLoadInterstitialAd();
            Invoke(nameof(ReplayAfterAds), .2f);
            PrintStatus("Interstitial ad is not ready yet.");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS
    public void RequestAndLoadRewardedAd()
    {
        PrintStatus("Requesting Rewarded ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = rewardedID;
#elif UNITY_IPHONE
        string adUnitId = rewardedID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // create new rewarded ad instance
        RewardedAd.Load(adUnitId, CreateAdRequest(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Rewarded ad failed to load with error: " +
                                loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Rewarded ad failed to load.");
                    return;
                }

                PrintStatus("Rewarded ad loaded.");
                rewardedAd = ad; 
                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Rewarded ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    OnUserEarnedRewardEvent?.Invoke();
                    //Controller.Instance.ActionVideo(true);
                    RequestAndLoadRewardedAd();
                    PrintStatus("Rewarded ad closed.");
                    OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Rewarded ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Rewarded ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Rewarded ad failed to show with error: " +
                               error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Rewarded ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "Rewarded");
                };
            });
    }
    public bool IsAdmobRewardedAvailable()
    {
        return rewardedAd != null;
    }
    public void ShowRewardedAdmob()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show((Reward reward) =>
            {              
                isInterstialAdPresent = true;
                PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
            });
        }
        else
        {
            //ToastHelper.ShowToast("Reward is not Loaded yet", false);
            RequestAndLoadRewardedAd();
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {
        PrintStatus("Requesting Rewarded Interstitial ad.");

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = rewardedID;
#elif UNITY_IPHONE
            string adUnitId = rewardedID;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a rewarded interstitial.
        RewardedInterstitialAd.Load(adUnitId, CreateAdRequest(),
            (RewardedInterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Rewarded interstitial ad failed to load with error: " +
                                loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Rewarded interstitial ad failed to load.");
                    return;
                }

                PrintStatus("Rewarded interstitial ad loaded.");
                rewardedInterstitialAd = ad;  
                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Rewarded interstitial ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    OnUserEarnedRewardEvent?.Invoke();
                    //Data_GF.Instance.RewardedAdWatched();
                    PrintStatus("Rewarded interstitial ad closed.");
                    OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Rewarded interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Rewarded interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Rewarded interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                                "Rewarded interstitial ad received a paid event.",
                                                adValue.CurrencyCode,
                                                adValue.Value);
                    AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "rewardedInterstitial");
                };
            });
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                PrintStatus("Rewarded interstitial granded a reward: " + reward.Amount);
            });
        }
        else
        {
            AdsManager.Insatance.ShowRewardedAd();
            PrintStatus("Rewarded interstitial ad is not ready yet.");
        }
    }

    #endregion

    #region APPOPEN ADS

    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (appOpenAd != null
                    && appOpenAd.CanShowAd()
                    && DateTime.Now < appOpenExpireTime);
        }
    }

    public void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);

        // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (state == AppState.Foreground)
            {
                ShowAppOpenAd();
            }
        });
    }
    //ca-app-pub-3940256099942544/3419835294
    public void RequestAndLoadAppOpenAd()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        PrintStatus("Requesting App Open ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = appOpenID;
#elif UNITY_IPHONE
        string adUnitId = appOpenID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // destroy old instance.
        if (appOpenAd != null)
        {
            DestroyAppOpenAd();
        }

        // Create a new app open ad instance.
        AppOpenAd.Load(adUnitId, ScreenOrientation.Portrait, CreateAdRequest(),
            (AppOpenAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("App open ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("App open ad failed to load.");
                    return;
                }
                PrintStatus("App Open ad loaded. Please background the app and return.");
                this.appOpenAd = ad;
                this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;
                if (isAppOpenShowOnStart)
                {
                    ShowAppOpenAd();
                }
                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("App open ad opened.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("App open ad closed."); 
                    AdsManager.Insatance.ShowBanner();
                    OnAdClosedEvent.Invoke();
                    HideAppOpen.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("App open ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("App open ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("App open ad failed to show with error: " +
                        error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "App open ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    AnalyticsManager.instance.Revenue_ReportAdmob(adValue, "AppOpen");
                    PrintStatus(msg);
                };
            });
    }

    public void DestroyAppOpenAd()
    {
        if (this.appOpenAd != null)
        {
            this.appOpenAd.Destroy();
            this.appOpenAd = null;
        }
    }

    public void ShowAppOpenAd()
    {
        if (PlayerPrefs.GetInt("RemoveAdsOnly") == 1) return;
        if (!IsAppOpenAdAvailable)
        {
            return;
        }
        appOpenAd.Show();
        AdsManager.Insatance.HideBanner();
    }

    public void OnApplicationPause(bool paused)
    {

        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            if (isInterstialAdPresent)
            {
                isInterstialAdPresent = false;
                return;
            }


            ShowAppOpenAd();

            //   isInterstialAdPresent check true where interstitial show......
        }

    }

    #endregion


    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        PrintStatus("Opening Ad inspector.");

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                PrintStatus("Ad inspector failed to open with error: " + error);
            }
            else
            {
                PrintStatus("Ad inspector opened successfully.");
            }
        });
    }

    #endregion

    #region Utility

    /// <summary>
    /// Loads the Google Ump sample scene.
    /// </summary>
    public void LoadUmpScene()
    {
        SceneManager.LoadScene("GoogleUmpScene");
    }

    ///<summary>
    /// Log the message and update the status text on the main thread.
    ///<summary>
    private void PrintStatus(string message)
    {
        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            // statusText.text = message;
        });
    }

    #endregion

}