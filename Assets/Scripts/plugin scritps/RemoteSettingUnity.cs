using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System;
public class RemoteSettingUnity : MonoBehaviour
{
    public static RemoteSettingUnity Instance;

    private void Awake()
    {
        Instance = this;
    }  
     

     
    public struct userAttributes { }
    public struct appAttributes { }

    async Task InitializeRemoteConfigAsync()
    {  
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            await InitializeRemoteConfigAsync();
        }


        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(Unity.Services.RemoteConfig.ConfigResponse configResponse)
    {

        AdMob.Instance.AdsTimerThreshold = RemoteConfigService.Instance.appConfig.GetInt("AdTimer", 30);
        AdMob.Instance.IsBigBannerOnAdmob = RemoteConfigService.Instance.appConfig.GetBool("IsBigBannerOnAdmob", true);
        //  AdsManager.Instance.UseBigBanner = RemoteConfigService.Instance.appConfig.GetBool("BigBanner", true);
        // AdmobAdsManager.Reviewbool = RemoteConfigService.Instance.appConfig.GetBool(AdmobAdsManager.Instance.ReviewBool, false);
        //  AdsManager.Instance.AdsTimerThreshold = RemoteConfigService.Instance.appConfig.GetInt("AdTimer", 30);
        // InternetConnectivity._instance.ping = RemoteConfigService.Instance.appConfig.GetInt("PingThreshold", 120);
        // AdsManager.Instance.AdmobFirst = RemoteConfigService.Instance.appConfig.GetBool("AdmobFirst", false);
        // AdsManager.Instance.AdmobFirstInGame = RemoteConfigService.Instance.appConfig.GetBool("AdmobFirstInGame", false);
        // AdsManager.UseInterstitialToUnlock = RemoteConfigService.Instance.appConfig.GetBool("UseInterstitialToUnlock", false);
        // AdsManager.UseVideoInterstitialToUnlock = RemoteConfigService.Instance.appConfig.GetBool("UseVideoInterstitialToUnlock", false);
        //  AdsManager.hideBigBannerBool = RemoteConfigService.Instance.appConfig.GetBool("hideBigBannerBool", false);
        //  AdsManager.IsMediationBottomBanner = RemoteConfigService.Instance.appConfig.GetBool("IsMediationBottomBanner01", false);
        //  AdmobAdsManager.Instance.CanAppOpenOrInter = RemoteConfigService.Instance.appConfig.GetBool("CanAppOpenOrInter", false);
        //  GameManager.Instance.IsInGameAdsOn = RemoteConfigService.Instance.appConfig.GetBool("IsInGameAdsOn", false);
        GameController.FirsTimeLevelOne = RemoteConfigService.Instance.appConfig.GetInt("FirsTimeLevelOnevalue", 0);
        // ABTest_Script.instance.GetRemoteConfig_IsBGmusic();
        ABTest_Script.instance.GetRemoteConfig_FirsTimeLevelOne();



        //if (!AdmobAdsManager.Instance.UseMediationBanner)
        //{
        //    bool usemediationbanner = RemoteConfigService.Instance.appConfig.GetBool("UseMediationBanner01");

        //    if (usemediationbanner)
        //    {
        //        AdmobAdsManager.Instance.UseMediationBanner = true;

        //        if (AdsManager.IsMediationBottomBanner)
        //        {
        //            MediationInitialization.Instance.position = IronSourceBannerPosition.BOTTOM;
        //            AdmobAdsManager.Instance.DestoryBannerBottom();

        //        }
        //        else
        //        {
        //            MediationInitialization.Instance.position = IronSourceBannerPosition.TOP;
        //            AdmobAdsManager.Instance.DestoryBannerTop();

        //        }


        //        MediationInitialization.Instance.LoadBanner();

        //    }
        //}

    }




}
