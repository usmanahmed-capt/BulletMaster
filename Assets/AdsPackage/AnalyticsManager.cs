using GameAnalyticsSDK;
using GoogleMobileAds.Api;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(this.gameObject);
        }
    }


    #region PUBLIC EVENTS

    public void ProgressionEvent(GAProgressionStatus progressionStatus, int LevelNumber)
    {
        string msg = "";
        switch (progressionStatus)
        {
            case GAProgressionStatus.Start:
                msg = "GD_LVL_Start";
                break;
            case GAProgressionStatus.Complete:
                msg = "GD_LVL_Complete";
                break;
            case GAProgressionStatus.Fail:
                msg = "GD_LVL_Fail";
                break;
            case GAProgressionStatus.Retry:
                msg = "GD_LVL_Retry";
                break; 
        } 

        FB_ProgressionEvent(msg, LevelNumber);
        GA_ProgressionEvent(progressionStatus, msg, LevelNumber);
    }
    public void VideoEvent(string placement)
    {
        FB_VideoEvent(placement);
        GA_VideoEvent(placement);
    }
    public void InterstitialEvent(string placement)
    {
        FB_InterstitialEvent(placement);
        GA_InterstitialEvent(placement);
    }
    public void CustomScreenEvent(string placement)
    {
        FB_CustomScreenEvent(placement);
        GA_CustomScreenEvent(placement);
    }
    public void CustomBtnEvent(string placement)
    {
        FB_CustomBtnEvent(placement);
        GA_CustomBtnEvent(placement);
    }
    public void CustomOtherEvent(string placement)
    {
        FB_CustomOtherEvent(placement);
        GA_CustomOtherEvent(placement);
    }
    public void IAPEvent(string sku)
    {
        FB_IAPEvent(sku);
        GA_IAPEvent(sku);
    }

    #endregion

    #region PRIVATE EVENTS

    #region firebaseEvents

    private void FB_ProgressionEvent(string msg, int LevelNumber)
    {
       
    }

    private void FB_VideoEvent(string placement)
    {
       
        print("FB_ADS_REWARDED_" + placement);
    }
    private void FB_InterstitialEvent(string placement)
    {
       
        print("FB_ADS_INTER_" + placement);
    }
    private void FB_CustomScreenEvent(string placement)
    {
        
        print("FB_GD_SCREEN_" + placement);
    }
    private void FB_CustomBtnEvent(string placement)
    {
        
        print("FB_GD_BTN_" + placement);
    }
    private void FB_CustomOtherEvent(string placement)
    {
        
        print("FB_GD_Other_" + placement);
    }
    private void FB_IAPEvent(string sku)
    {
        
        print("FB_IAP_" + sku);
    }
    #endregion

    #region GameAnalyticsEvent
    private void GA_ProgressionEvent(GAProgressionStatus Id, string msg, int LevelNumber)
    {
        GameAnalytics.NewProgressionEvent(Id, "LVL_" + LevelNumber.ToString());
        GameAnalytics.NewDesignEvent(msg + LevelNumber);
        print("GA_ProgressionEvent :" + msg + LevelNumber);
    }
    private void GA_CustomScreenEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("GD_SCREEN_" + placement);
        print("GA_GD_SCREEN_" + placement);
    }
    private void GA_CustomBtnEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("GD_BTN_" + placement);
        print("GA_GD_BTN_" + placement);
    }
    private void GA_VideoEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("ADS_REWARDED_" + placement);
        print("GA_ADS_REWARDED_" + placement);
    }
    private void GA_InterstitialEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("ADS_INTER_" + placement);
        print("GA_ADS_INTER_" + placement);
    }
    private void GA_CustomOtherEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("GD_Other_" + placement);
        print("GA_GD_Other_" + placement);
    }
    private void GA_IAPEvent(string sku)
    {
        GameAnalytics.NewDesignEvent("IAP_" + sku);
        print("GA_IAP_" + sku);
    }
    #endregion
    #endregion



    #region Admob Paid Event
    public void Revenue_ReportAdmob(AdValue admobAd, string adType)
    {
        double revenue = (admobAd.Value / 1000000f);

        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("ad_format", "admob_" + adType);
        //AppsFlyerAdRevenue.logAdRevenue("simple_admob", AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeGoogleAdMob, revenue, "USD", dic);
        Debug.Log("EnterAdmob" + revenue);
    }

    #endregion

    #region Max Paid Event
    public void Revenue_ReportMax(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
       
    }
    #endregion
}
