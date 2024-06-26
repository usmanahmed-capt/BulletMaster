using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;
    public int currenLvlClick;
    public float SfxSoundLevel;
    public bool CanPlayOn;
    public float bulletSpeed;
    //  public PlayerController player;
    [SerializeField]
    int _coins;
    public int Coins
    {
        get { return _coins; }
        private set { _coins = value; }
    }

    [SerializeField]
    int _star;
    internal string rewardPlacement;
    /// <summary>
    /// rewardIndux=1 spinWheel:
    /// </summary>
    [Header("RewardIndux")]
    internal int rewardIndux;


    public int Stars
    {
        get { return _star; }
        private set { _star = value; }
    }

    public int CallingIndux { get; internal set; }
    public bool IsLoading { get; internal set; }
    public bool IsFromGameBack { get; internal set; }
    public static int FirsTimeLevelOne { get; internal set; }

    /// <summary>
    ///  1 Claimchar  2 claimBullet
    /// </summary>

    public int SkinReward;
    internal int vibrationValue;
    internal float PreviouseFillAmount;
    internal int currPlayerIndux;
    internal int AfterCallingInterstatialads;
    internal int previouseBuildIndux;

    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public  bool GiftAvalible(string PrfesName)
    {

        return RemendingTimeSpanForGift(PrfesName).TotalSeconds <= 0;
    }

    public  TimeSpan RemendingTimeSpanForGift(string PrfesName)
    {
        return (NextGiftTimeGet(PrfesName) - DateTime.Now);
    }
    public  DateTime NextGiftTimeGet(string PrfesName)
    {
        return DateTime.FromFileTime(long.Parse(PlayerPrefs.GetString(PrfesName, DateTime.Now.ToFileTime() + "")));
    }

    public  void NextGiftTimeSet(string PrfesName, DateTime dateTime)
    {
        PlayerPrefs.SetString(PrfesName, dateTime.ToFileTime() + "");
    }

  

    void Awake()
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
       

      //  PlayerPrefs.SetInt("coins", 30000);
        Coins = PlayerPrefs.GetInt("coins");
//#if UNITY_EDITOR
//        Debug.unityLogger.logEnabled = true;
//#elif UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_ANDROID
//                        Debug.unityLogger.logEnabled = false;
//#endif

        //player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
       
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("coins", Coins);
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        // Store new coin value
        PlayerPrefs.SetInt("coins", Coins);
    }

    public void AddStar(int amount)
    {
        Stars += amount;
        PlayerPrefs.SetInt("Star", Stars);
    }

    public void UpadetStar(int amount)
    {
        Stars = amount;
    }

    public void RemoveStar(int amount)
    {
        Stars -= amount;
        // Store new coin value
        PlayerPrefs.SetInt("Star", Stars);
    }


}
