using UnityEngine;
using GameAnalyticsSDK;
using System;

public class ABTest_Script : MonoBehaviour
{
    public static ABTest_Script instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GetRemoteConfig_IsBGmusic() // For Time Strategytest run value 1
    {
        //if (AdsManager.AdsTimerAB == 0)// No Test Run Old Default Value will Run
        //{
        //    PlayerPrefs.SetInt("AdsTimerAB", 0);
        //    AdsManager.Instance.AdsTimerThreshold = 20; //AdsManager.Instance.AdsTimerThreshold = 20;
        //    //  Debug.LogError("Time20");
        //    return;
        //}
        //if (AdsManager.AdsTimerAB == 2)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("AdsTimerAB", 1);
        //    AdsManager.Instance.AdsTimerThreshold = 30;
        //    //Debug.LogError("Time30");
        //    return;
        //}
        //if (AdsManager.AdsTimerAB == 3)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("AdsTimerAB", 2);
        //    AdsManager.Instance.AdsTimerThreshold = 45;
        //   // Debug.LogError("Time45");
        //    return;
        //}
        //if (PlayerPrefs.GetInt("AdsTimerAB") == 1) // if user get value from GA, dont get value again.
        //{
        //    // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB")+"Time30");
        //    AdsManager.Instance.AdsTimerThreshold = 30;
        //    return;
        //}
        //if (PlayerPrefs.GetInt("AdsTimerAB") == 2) // if user get value from GA, dont get value again.
        //{
        //    //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "Time45");
        //    AdsManager.Instance.AdsTimerThreshold = 45;
        //    return;
        //}

        //GameAnalytics.RemoteConfigsUpdated();
        //if (GameAnalytics.IsRemoteConfigsReady())
        //{
        //    try
        //    {
        //        string val = GameAnalytics.GetRemoteConfigsValueAsString("AdsTimerAB", "Twenty");
        //        if (!string.IsNullOrEmpty(val))
        //        {
        //            if (val == "Twenty")
        //            {   //same color allowed to drop
                     
        //                PlayerPrefs.SetInt("AdsTimerAB", 0);
        //              //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "Twenty");
        //                AdsManager.Instance.AdsTimerThreshold = 20;
        //            }
        //            else if (val == "Thirty")
        //            {   //same color allowed to drop
        //               // Debug.Log("Old GamePlay Scenerio");
        //                PlayerPrefs.SetInt("AdsTimerAB", 1);
        //               // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "Thirty");
        //                AdsManager.Instance.AdsTimerThreshold = 30;
        //            }
        //            else if (val == "Forty")
        //            {   //same color allowed to drop
        //              //  Debug.Log("Old GamePlay Scenerio");
        //                PlayerPrefs.SetInt("AdsTimerAB", 2);
        //              //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "Forty");
        //                AdsManager.Instance.AdsTimerThreshold = 45;
        //            }
        //        }
        //        else
        //        {
        //            PlayerPrefs.SetInt("AdsTimerAB", 0); // if not ready game analytics ... or string is unable to reach out
        //            AdsManager.Instance.AdsTimerThreshold = 20;
        //          //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  Debug.Log("Remoteconfig exception=>" + ex.ToString());
        //        AdsManager.Instance.AdsTimerThreshold = 20;
        //      //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
        //    }
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("AdsTimerAB", 0); // if not ready game analytics
        //    AdsManager.Instance.AdsTimerThreshold = 20;
        //   // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
        //    //  print("RemoteConfig not ready");
        //}
    }



    public void GetRemoteConfig_FirsTimeLevelOne() // For Time Strategy test run value 2
    {
        if (PlayerPrefs.GetInt("runOnlyOnce", 0) == 0)
        {
            PlayerPrefs.SetInt("runOnlyOnce", 1);
            if (GameController.FirsTimeLevelOne == 0)// No Test Run Old Default Value will Run
            {
                PlayerPrefs.SetInt("FirsTimeLevelOne", 0); //mode Selection // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                return;
            }
            if (GameController.FirsTimeLevelOne == 1)// 100% BG off
            {
                PlayerPrefs.SetInt("FirsTimeLevelOne", 1);//lvl Selection
                                                          //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                return;
            }
            if (PlayerPrefs.GetInt("FirsTimeLevelOne") == 1) // if user get value from GA, dont get value again.
            {
                PlayerPrefs.SetInt("FirsTimeLevelOne", 1);//lvl Selection
                                                          // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                return;
            }

            GameAnalytics.RemoteConfigsUpdated();
            if (GameAnalytics.IsRemoteConfigsReady())
            {
                try
                {
                    string val = GameAnalytics.GetRemoteConfigsValueAsString("Lvlone", "lvlplay");//ModeSelection,levelsection,modeplvlfityprn
                    if (!string.IsNullOrEmpty(val))
                    {
                        if (val == "lvlplay")
                        {   //same color allowed to drop
                            // Debug.Log("Go to Direct ScreenSelection");
                            PlayerPrefs.SetInt("FirsTimeLevelOne", 1);
                            // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                        }
                        else if (val == "mainplay")
                        {   //same color allowed to drop
                            // Debug.Log("Go to Direct MatchingPlay");
                            PlayerPrefs.SetInt("FirsTimeLevelOne", 0);
                            // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                        }

                    }
                    else
                    {
                        PlayerPrefs.SetInt("FirsTimeLevelOne", 0); // if not ready game analytics ... or string is unable to reach out
                                                                   //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                    }
                }
                catch (Exception ex)
                {
                    //  Debug.Log("Remoteconfig exception=>" + ex.ToString());
                    PlayerPrefs.SetInt("FirsTimeLevelOne", 0);
                    // Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
                }
            }
            else
            {
                PlayerPrefs.SetInt("FirsTimeLevelOne", 0);
                //  Debug.LogError(PlayerPrefs.GetInt("AdsTimerAB") + "20");
            }
        }
        else 
        {
            PlayerPrefs.SetInt("FirsTimeLevelOne", 0);
        }
    }
    public void GetRemoteConfig_RewardAdsOnSpeciallvl() // For Time Strategy test run value 2
    {
        //if (GameManager.RewardAdsOnSpecialLvl == 0)// No Test Run Old Default Value will Run
        //{
        //    PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 0); //mode Selection
        //}
        //if (GameManager.RewardAdsOnSpecialLvl == 1)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 1);//lvl Selection
        //}
        //if (PlayerPrefs.GetInt("RewardAdsOnSpecialLvl") == 1) // if user get value from GA, dont get value again.
        //{
        //    PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 1);//lvl Selection
        //    return;
        //}

        GameAnalytics.RemoteConfigsUpdated();
        if (GameAnalytics.IsRemoteConfigsReady())
        {
            try
            {
                string val = GameAnalytics.GetRemoteConfigsValueAsString("rewardspcil", "spcilrwardno");//ModeSelection,levelsection,modeplvlfityprn
                if (!string.IsNullOrEmpty(val))
                {
                    if (val == "spcilrwardno")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct ScreenSelection");
                        PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 0);
                    }
                    else if (val == "spcilrwardon")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct MatchingPlay");
                        PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 1);
                    }
                    //else if (val == "modeplvlfityprn")
                    //{   //same color allowed to drop
                    //    Debug.Log("Go 50% mode or lvlselection");
                    //    PlayerPrefs.SetInt("ABLvlToModTest", 1);
                    //}
                }
                else
                {
                    PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 0); // if not ready game analytics ... or string is unable to reach out
                }
            }
            catch (Exception ex)
            {
                //  Debug.Log("Remoteconfig exception=>" + ex.ToString());
                PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("RewardAdsOnSpecialLvl", 0);
            print("RemoteConfig not ready");
        }
    }


    public void GetRemoteConfig_NextHometest() // For Time Strategy test run value 2
    {
        //if (GameManager.Instance.nextHometest == 0)// No Test Run Old Default Value will Run
        //{
        //    PlayerPrefs.SetInt("nextHometest", 0); //mode Selection
        //}
        //if (GameManager.Instance.nextHometest == 1)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("nextHometest", 1);//lvl Selection
        //}
        //if (PlayerPrefs.GetInt("nextHometest") == 1) // if user get value from GA, dont get value again.
        //{
        //    PlayerPrefs.SetInt("nextHometest", 1);//lvl Selection
        //    return;
        //}

        GameAnalytics.RemoteConfigsUpdated();
        if (GameAnalytics.IsRemoteConfigsReady())
        {
            try
            {
                string val = GameAnalytics.GetRemoteConfigsValueAsString("nexthometest", "nextbtn");//ModeSelection,levelsection,modeplvlfityprn
                if (!string.IsNullOrEmpty(val))
                {
                    if (val == "nextbtn")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct ScreenSelection");
                        PlayerPrefs.SetInt("nextHometest", 0);
                    }
                    else if (val == "homebtn")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct MatchingPlay");
                        PlayerPrefs.SetInt("nextHometest", 1);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("nextHometest", 0); // if not ready game analytics ... or string is unable to reach out
                }
            }
            catch (Exception ex)
            {
                //  Debug.Log("Remoteconfig exception=>" + ex.ToString());
                PlayerPrefs.SetInt("nextHometest", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("nextHometest", 0);
            print("RemoteConfig not ready");
        }
    }

    public void GetRemoteConfig_selectionthreeOrfour() // For Time Strategy test run value 2
    {
        //if (GameManager.Instance.selectionthreeOrfour == 0)// No Test Run Old Default Value will Run
        //{
        //    PlayerPrefs.SetInt("selectionthreeOrfour", 0); //mode Selection
        //}
        //if (GameManager.Instance.selectionthreeOrfour == 1)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("selectionthreeOrfour", 1);//lvl Selection
        //}
        //if (PlayerPrefs.GetInt("selectionthreeOrfour") == 1) // if user get value from GA, dont get value again.
        //{
        //    PlayerPrefs.SetInt("selectionthreeOrfour", 1);//lvl Selection
        //    return;
        //}

        GameAnalytics.RemoteConfigsUpdated();
        if (GameAnalytics.IsRemoteConfigsReady())
        {
            try
            {
                string val = GameAnalytics.GetRemoteConfigsValueAsString("scltrhorfour", "selectionthree");//ModeSelection,levelsection,modeplvlfityprn
                if (!string.IsNullOrEmpty(val))
                {
                    if (val == "selectionthree")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct ScreenSelection");
                        PlayerPrefs.SetInt("selectionthreeOrfour", 0);
                    }
                    else if (val == "selectionfour")
                    {   //same color allowed to drop
                        // Debug.Log("Go to Direct MatchingPlay");
                        PlayerPrefs.SetInt("selectionthreeOrfour", 1);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("selectionthreeOrfour", 0); // if not ready game analytics ... or string is unable to reach out
                }
            }
            catch (Exception ex)
            {
                //  Debug.Log("Remoteconfig exception=>" + ex.ToString());
                PlayerPrefs.SetInt("selectionthreeOrfour", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("selectionthreeOrfour", 0);
            print("RemoteConfig not ready");
        }
    }
    public void GetRemoteConfig_storehorizontalorverticle() // For Time Strategy test run value 2
    {
        //if (GameManager.Instance.Storehorizontalorverticle == 0)// No Test Run Old Default Value will Run
        //{
        //    PlayerPrefs.SetInt("storehorizontalorverticle", 0); //mode Selection
        //}
        //if (GameManager.Instance.Storehorizontalorverticle == 1)// 100% BG off
        //{
        //    PlayerPrefs.SetInt("storehorizontalorverticle", 1);//lvl Selection
        //}
        //if (PlayerPrefs.GetInt("storehorizontalorverticle") == 1) // if user get value from GA, dont get value again.
        //{
        //    PlayerPrefs.SetInt("storehorizontalorverticle", 1);//lvl Selection
        //    return;
        //}

        GameAnalytics.RemoteConfigsUpdated();
        if (GameAnalytics.IsRemoteConfigsReady())
        {
            try
            {
                string val = GameAnalytics.GetRemoteConfigsValueAsString("strhrivrtile", "verticle");//ModeSelection,levelsection,modeplvlfityprn
                if (!string.IsNullOrEmpty(val))
                {
                    if (val == "verticle")
                    {   //same color allowed to drop
                        Debug.Log("Go to Direct ScreenSelection");
                        PlayerPrefs.SetInt("storehorizontalorverticle", 0);
                    }
                    else if (val == "horizontal")
                    {   //same color allowed to drop
                        Debug.Log("Go to Direct MatchingPlay");
                        PlayerPrefs.SetInt("storehorizontalorverticle", 1);
                    }
                }
                else
                {
                    Debug.Log("Remoteconfig exception=>");
                    PlayerPrefs.SetInt("storehorizontalorverticle", 0); // if not ready game analytics ... or string is unable to reach out
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Remoteconfig exception=>" + ex.ToString());
                PlayerPrefs.SetInt("storehorizontalorverticle", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("storehorizontalorverticle", 0);
            print("RemoteConfig not ready");
        }
    }

}




