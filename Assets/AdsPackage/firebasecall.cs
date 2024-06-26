using UnityEngine;

public class firebasecall : MonoBehaviour
{
    public static firebasecall Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        OnFireBase();
    }
    #region Firebase
    [HideInInspector]
    public bool firebaseInitialized = false;


    void OnFireBase()
    {
        /*FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });*/
    }


    // fire base remote setting 

    //public Task FetchDataAsync()
    //{
    //    Debug.Log("Fetching data...");
    //    System.Threading.Tasks.Task fetchTask =
    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
    //        TimeSpan.Zero);
    //    return fetchTask.ContinueWithOnMainThread(FetchComplete);
    //}


    //// 
    //void FetchComplete(Task fetchTask)
    //{
    //    if (fetchTask.IsCanceled)
    //    {
    //        Debug.Log("Fetch canceled.");
    //    }
    //    else if (fetchTask.IsFaulted)
    //    {
    //        Debug.Log("Fetch encountered an error.");
    //    }
    //    else if (fetchTask.IsCompleted)
    //    {
    //        Debug.Log("Fetch completed successfully!");
    //    }

    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:

    //            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
    //            .ContinueWithOnMainThread(task =>
    //            {
    //                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
    //                               info.FetchTime));
    //                GetRemoteData();
    //            });

    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    Debug.Log("Fetch failed for unknown reason");
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            Debug.Log("Latest Fetch call still pending.");
    //            break;
    //    }
    //}
    public void GetRemoteData()
    {
        Debug.Log("Current Data:");
        //GlobalConstant.isMediation = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("isMediation").BooleanValue;
        //GlobalConstant.isHybrid = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("isHybrid").BooleanValue;
        //Debug.Log(GlobalConstant.isMediation+ "isMediation");
       // Debug.Log(GlobalConstant.isHybrid + "isHybrid");
    }

    // ......................
    void InitializeFirebase()
    {
       /* Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        Debug.Log("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
            FirebaseAnalytics.UserPropertySignUpMethod,
            "Google");
        // Set the user ID.
        //  FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        //  FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;
        FirebaseApp app = FirebaseApp.DefaultInstance;
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
        System.Collections.Generic.Dictionary<string, object> defaults =
       new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server
        // yet, or if we ask for values that the server doesn't have:
        defaults.Add("isMediation", true);
        defaults.Add("isHybrid", true);*/
        //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
        //  .ContinueWithOnMainThread(task =>
        //  {
        //      FetchDataAsync();
        //  });
        CheckInterNet();
    }
    public void LogEvent(string evt)
    {
        if (!firebaseInitialized)
            return;

        // Log an event with a float.
        Debug.Log("Logging a progress event.");
        //FirebaseAnalytics.LogEvent(evt);
    }

    public void LogEventGame(string evt)
    {
        if (!firebaseInitialized)
            return;

        // Log an event with a float.
        //Debug.Log("Logging a progress event.");
        //FirebaseAnalytics.LogEvent(evt);
    }

    void CheckInterNet()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            AnalyticsManager.instance.CustomOtherEvent("Internet_Available");
        }
        else
        {
            AnalyticsManager.instance.CustomOtherEvent("Internet_Notavailable");
        }
    }

    #endregion
}
