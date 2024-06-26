using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public string ProgressionStartEvent;
    public string progreesionEndEvent;
    public string DesignEventStart;
    public string DesignEventComplete;
    public bool isPrgessionStart;
    public bool isPrgessionStartProgressionByManagerValue;
    public bool isProgressionComplete;
    public bool isProgressionCompleteProgressionByManagerValue;
    public bool isDesingEventComplateOnDisable;
    public bool isDesignEventShowingOnStart;
    private string MyStartFinger;
    private string Lvl;

    void Start()
    {
        Instance = this;
        //  Debug.LogError("AppController.specialLevel"+AppController.specialLevel);
        DesignEventStartFun();
        if (isPrgessionStart)
        {
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "" );//sohail
                    firebasecall.Instance.LogEventGame("");//sohail
        }
        if (isPrgessionStartProgressionByManagerValue)
        {
              //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "special_Level" + saveLevel);
        }
    }

    //with Level Wise
    public void SubmitEvents(string EvenName) 
    {
        string eventNamePrefix;
        eventNamePrefix = EvenName.Replace(" ", ""); // Remove spaces

        firebasecall.Instance.LogEventGame(eventNamePrefix);//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(eventNamePrefix);//sohail
    }

    public void SubmitEventsFinger(string EvenName)
    {
        string eventNamePrefix;
        eventNamePrefix = EvenName.Replace(" ", ""); // Remove spaces

    }

    public void SubmitEventsFingerFreeStlye(string EvenName)
    {
        string eventNamePrefix;
        eventNamePrefix = EvenName.Replace(" ", ""); // Remove spaces
      //  Debug.LogError(eventNamePrefix);
        firebasecall.Instance.LogEventGame("freestyle" + "_" + eventNamePrefix);//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("freestyle" + "_" + eventNamePrefix);//sohail
    }

    public void SubmitEventsFreeStyle(string EvenName)
    {
        string eventNamePrefix;
        eventNamePrefix = EvenName.Replace(" ", ""); // Remove spaces                       
       // Debug.LogError(eventNamePrefix);
        firebasecall.Instance.LogEventGame("freestyle"+"_" + eventNamePrefix);//sohail
            GameAnalyticsSDK.GameAnalytics.NewDesignEvent("freestyle" +"_" + eventNamePrefix);//sohail
    }

    public void SubmitEventsWitoutFinger(string EvenName)
    {
        //if (AppController.specialLevel)
        //{
        //    firebasecall.Instance.LogEventGame("spcil_L" + Lvl + "_" + EvenName);//sohail
        //    GameAnalyticsSDK.GameAnalytics.NewDesignEvent("spcl_L" + Lvl + "_" + EvenName);//sohail
        // //   Debug.LogError("spcl_L" + Lvl + "_" + EvenName);
        //}
        //else
        //{
        //    firebasecall.Instance.LogEventGame("match_L" + Lvl + "_" + EvenName);//sohail
        //    GameAnalyticsSDK.GameAnalytics.NewDesignEvent("match_L" + Lvl + "_" + EvenName);//sohail
        // //   Debug.LogError("match_L" + Lvl + "_" + EvenName);
        //}
     
    }

    void DesignEventStartFun()
    {
        if (isDesignEventShowingOnStart)
        {
            firebasecall.Instance.LogEventGame(DesignEventStart);//sohail
            GameAnalyticsSDK.GameAnalytics.NewDesignEvent(DesignEventStart);//sohail
          //  Debug.LogError(DesignEventStart);
        }
    }
    public void ProgressionComplete() 
    {
        if (isProgressionComplete) 
        {
            //try
            //{
            //    string saveLevel = AppController.levelName.Substring(0, 2);
            //    if (AppController.specialLevel)
            //    {
            //        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "special_Level" + saveLevel);
            //    //    Debug.LogError("special_Level" + saveLevel);
            //    }
            //    else
            //    {
            //        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "matching_Level" + saveLevel);
            //     //   Debug.LogError("matching_Level" + saveLevel);
            //    }
            //}
            //catch (System.Exception)
            //{

            //}
        }
           

    }
    public void DesignEventCompleteFun()
    {
        firebasecall.Instance.LogEventGame(DesignEventComplete);//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(DesignEventComplete);//sohail
       // Debug.LogError(DesignEventStart);

    }

    public void DesignEventWithStr(string Designevent)
    {
      //  Debug.LogError(Designevent);
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(Designevent);//sohail
        firebasecall.Instance.LogEventGame(Designevent);//sohail
       
       // Debug.LogError(Designevent);
    }

    private void OnDisable()
    {
        if (isDesingEventComplateOnDisable)
        {
            firebasecall.Instance.LogEventGame(DesignEventComplete);//sohail
            GameAnalyticsSDK.GameAnalytics.NewDesignEvent(DesignEventComplete);//sohail
          //  Debug.LogError(DesignEventComplete);
        }

        ProgressionComplete();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
