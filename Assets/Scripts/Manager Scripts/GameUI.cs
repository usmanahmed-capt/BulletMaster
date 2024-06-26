using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instace;

    public GameManager gameManager;

    private int startBB;

    [Header("WinScreen")]
    public Text goodJobText;
    public GameObject winPanel;
    public Image star1, star2, star3;
    public ParticleSystem star1part, star2part, star3part;
    public Sprite shineStar, darkStar;

    [Header("GameOver")]
    public GameObject gameOverPanel;

    //public PlayerController player;
    private int CurrentPlayedLvl;
    public Image charBack;
    public Image CharFillImage;

    [SerializeField] private Sprite[] charToFill; // Array of sprites
    [SerializeField] private float fillSpeed = 0.1f; // Initial fill speed (adjust as needed)
    [SerializeField] private float fillTime = 0.5f; // Desired animation time in seconds

    public GameObject ClaimBtn;
    public Animator CharAnim;
    public ParticleSystem CharParticle;
    public Canvas canvas;
    public GameObject OutOfBullet;
    public GameObject SkipLvl;

    private bool IsAllowOneTimeOutofBulletPanalShow;
    public GameObject ExitPanal;
    public Animator WinAnim;

    string SceneName;
    public GameObject ToturialTxt;

    private bool isToturials;
    public MoreMountains.NiceVibrations.NiceVibrationsDemoManager niceVibrations;
    public ParticleSystem CharUnlockParticle;
    public Text levelText;
    internal int buildIndexx;

    void Awake()
    {
        instace = this;

        
    }

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        startBB = gameManager.blackBullets;
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Level " + GameController.Instance.currenLvlClick;
        int spriteIndex = (buildIndex - 1) / 10; // Calculate sprite index based on level range
        charBack.sprite = charToFill[spriteIndex];
        CharFillImage.sprite = charToFill[spriteIndex];
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Ui";
        canvas.sortingOrder = 10;
        SceneName = SceneManager.GetActiveScene().name;
        buildIndexx = SceneManager.GetActiveScene().buildIndex;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, SceneName);//sohail
        firebasecall.Instance.LogEventGame(SceneName + "_start");//sohail
        AdsManager.Insatance.ShowBanner();

        if (PlayerPrefs.GetInt("Toturial") == 0) 
        {
            isToturials = true;
            ToturialTxt.SetActive(true);
        }
    }

    internal void DisableToturial()
    {
        if (isToturials)
        {
            if (PlayerPrefs.GetInt("Toturial") == 0)
            {
                ToturialTxt.SetActive(false);
                PlayerPrefs.SetInt("Toturial", 1);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanalShow();
        }
    }
    public void GameOverScreen()
    {
        GameController.Instance.CanPlayOn = false;
        for (int i = 0; i < gameManager.EnemyAll.Count; i++)
        {
            gameManager.EnemyAll[i].HappyFaceFun();
        }
        if (!IsAllowOneTimeOutofBulletPanalShow) 
        {
            OutOfBullet.SetActive(true);
            IsAllowOneTimeOutofBulletPanalShow = true;
        }
        else 
        {
            OutOfBullet.SetActive(false);
            ShowGameover();
        }
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, SceneName);//sohail
        firebasecall.Instance.LogEventGame(SceneName + "_failed");//sohail
    }

    public void ShowGameover()
    {
        AdsManager.Insatance.ShowMREC();
        niceVibrations.TriggerFailure();
        gameOverPanel.SetActive(true);
    }

    public void WinScreen(int LvlNo)
    {
        star2.gameObject.SetActive(false);
        star1.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
        GameController.Instance.CanPlayOn = false;
        CurrentPlayedLvl = LvlNo;
        ClaimBtn.SetActive(false);
        if (gameManager.blackBullets >= startBB)
        {
            goodJobText.text = "FANTASTIC!";
            StartCoroutine(Starts(3));
        }
        else if(gameManager.blackBullets >= startBB - (gameManager.blackBullets / 2))
        {
            goodJobText.text = "AWESOME!";
            StartCoroutine(Starts(2));
        }
        else if(gameManager.blackBullets > 0)
        {
            goodJobText.text = "WELL DONE!";
            StartCoroutine(Starts(1));
        }
        else
        {
            StartCoroutine(Starts(0));
            goodJobText.text = "GOOD";
        }
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, SceneName);//sohail
        firebasecall.Instance.LogEventGame(SceneName + "_complete");//sohail
    }

    private IEnumerator Starts(int shineNumber)
    {
        yield return new WaitForSeconds(0.5f);
        winPanel.SetActive(true);
        PlayerPrefs.SetInt("lvl" + CurrentPlayedLvl, shineNumber);
        // Calculate target fill amount based on level segments
        int segment = Mathf.CeilToInt((CurrentPlayedLvl - 1) / 10f);
        float targetFillAmount = (CurrentPlayedLvl - (segment - 1) * 10f) / 10.0f; // Corrected calculation

        if (CurrentPlayedLvl == 1 || CurrentPlayedLvl == 11 || CurrentPlayedLvl == 21 || CurrentPlayedLvl == 31 || CurrentPlayedLvl == 41)
        {
            GameController.Instance.PreviouseFillAmount = 0;
            targetFillAmount = 0.1f; // Set specific fill amount for level 1
        }
        GamePlay.Instance.LowBgVolume();
        yield return new WaitForSeconds(0.5f);
        star2.sprite = shineStar;
        switch (shineNumber)
        {
            //    public ParticleSystem star1part, star2part, star3part;
            case 3:
                yield return new WaitForSeconds(.15f);
                star1.gameObject.SetActive(true);
                star1.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star1part.Play();
                yield return new WaitForSeconds(.15f);
                star2.gameObject.SetActive(true);
                star2.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star2part.Play();
                yield return new WaitForSeconds(.15f);
                star3.gameObject.SetActive(true);
                star3.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star3part.Play();
                break;

            case 2:
                yield return new WaitForSeconds(.15f);
                star1.gameObject.SetActive(true);
                star1.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star1part.Play();
                yield return new WaitForSeconds(.15f);
                star2.gameObject.SetActive(true);
                star2.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star2part.Play();
                star3.sprite = darkStar;
                break;

            case 1:
                yield return new WaitForSeconds(.15f);
                star1.gameObject.SetActive(true);
                star1.sprite = shineStar;
                SoundManager.Instance.PlayStarSound();
                star1part.Play();
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;

            case 0:
                star1.sprite = darkStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
        }
        WinAnim.Rebind();
        WinAnim.enabled = true;
        yield return new WaitForSeconds(.35f);
        // Calculate adjusted fill speed based on desired animation time
        float adjustedFillSpeed = Mathf.Clamp(targetFillAmount - CharFillImage.fillAmount, 0f, 1f) / fillTime;
        float startTime = Time.time;

        if (GameController.Instance.PreviouseFillAmount < targetFillAmount)
        {
            while (Time.time - startTime < fillTime)
            {
                CharFillImage.fillAmount += adjustedFillSpeed * Time.deltaTime;
                CharFillImage.fillAmount = Mathf.Clamp(CharFillImage.fillAmount, 0f, targetFillAmount);
                yield return null;
            }

            CharFillImage.fillAmount = targetFillAmount; // Ensure fill amount reaches target value
            GameController.Instance.PreviouseFillAmount = CharFillImage.fillAmount;
        }
        else
        {
            while (Time.time - startTime < fillTime)
            {
                CharFillImage.fillAmount += GameController.Instance.PreviouseFillAmount * Time.deltaTime;
                CharFillImage.fillAmount = Mathf.Clamp(CharFillImage.fillAmount, 0f, GameController.Instance.PreviouseFillAmount);
                yield return null;
            }

            CharFillImage.fillAmount = targetFillAmount; // Ensure fill amount reaches target value
            GameController.Instance.PreviouseFillAmount = CharFillImage.fillAmount;
        }
        if (CharFillImage.fillAmount >= 1)
        {
            ClaimBtn.SetActive(true);
            SoundManager.Instance.PlayUnlockItemSound();
            CharUnlockParticle.Play();
        }
        //while (CharFillImage.fillAmount < targetFillAmount)
        //{
        //    CharFillImage.fillAmount += fillSpeed * Time.deltaTime;
        //    CharFillImage.fillAmount = Mathf.Clamp(CharFillImage.fillAmount, 0f, targetFillAmount);
        //    yield return null;
        //}
    }


    public void ClaimChar() 
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.CallingIndux = 1;
        AdsManager.Insatance.ShowRewardedAd();
       // AfterClarCallBackFun();
        //Calling ads Here
    }
    internal void AfterClarCallBackFun()
    {
        ClaimBtn.SetActive(false);
        CharAnim.enabled = true;
        CharParticle.Play();
        GameController.Instance.currPlayerIndux++;
    }

    public void ClaimBullets()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.CallingIndux = 2;
        AdsManager.Insatance.ShowRewardedAd();
       // AfterClaimingBullets();
        //Calling ads Here
    }
    public void NoThanks()
    {
        SoundManager.Instance.PlayButtonClickSound();
        firebasecall.Instance.LogEventGame(SceneName + "_NoThanks");//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(SceneName + "_NoThanks");//sohail
        OutOfBullet.SetActive(false);
        ShowGameover();
        //Calling ads Here
    }
    internal void AfterClaimingBullets()
    {
        GameController.Instance.CanPlayOn = true;
        SoundManager.Instance.PlayClainRewardSound();
        for (int i = 0; i < gameManager.EnemyAll.Count; i++)
        {
            gameManager.EnemyAll[i].NormalFaceFun();
        }
        gameManager.SpownBullets();
        OutOfBullet.SetActive(false);
        gameManager.gameOver = false;
    }

    public void ShowSkipLvl() 
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.CanPlayOn = false;
        SkipLvl.SetActive(true);
    }

    public void DisableSkipLvl()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.CanPlayOn = true;
        SkipLvl.SetActive(false);
    }

    public void SkipLvlFun()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.CallingIndux = 3;
        AdsManager.Insatance.ShowRewardedAd();
        //  AfterSkipLvl();
        //Calling ads Here
    }

    internal void AfterSkipLvl()
    {


        if ((SceneManager.GetActiveScene().buildIndex + 1) < 40)
        {

            if (buildIndexx != GameController.Instance.previouseBuildIndux)
            {
                GameController.Instance.previouseBuildIndux = GameController.Instance.currenLvlClick;
                PlayerPrefs.SetInt("lvl" + GameController.Instance.currenLvlClick, 1);
                PlayerPrefs.SetInt("Level", GameController.Instance.currenLvlClick + 1);
                GameController.Instance.currenLvlClick++;
            }

            gameManager.
            StartCoroutine(gameManager.FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            int Range = UnityEngine.Random.Range(0, 3);

            if (Range == 0)
            {
                StartCoroutine(gameManager.FadeIn(11));
            }
            if (Range == 1)
            {
                StartCoroutine(gameManager.FadeIn(21));
            }
            if (Range == 2)
            {
                StartCoroutine(gameManager.FadeIn(31));
            }
        }
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    //SkipLvl

    public void ExitPanalShow()
    {
        GameController.Instance.CanPlayOn = false;
        ExitPanal.SetActive(true);
        Time.timeScale = 0;
       
    }

    public void ExitPanalHide()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Time.timeScale = 1;
        ExitPanal.SetActive(false);
        GameController.Instance.CanPlayOn = true;
    }

    public void ApplicationQuit()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Application.Quit();
    }


   internal  void SlowEffects() 
    {
        StartCoroutine(SlowMotionEffect());
    }

    IEnumerator SlowMotionEffect()
    {
        // Save original time scale for later restoration
        float originalTimeScale = 1;

        // Slow down time (adjust slowMotionFactor for desired effect)
        Time.timeScale = 0.2f; // Adjust this value for desired slowness

        // Wait for slow-motion duration (adjust slowMotionDuration for desired length)
        yield return new WaitForSeconds(0.4f); // Adjust this value for duration

        // Gradually restore time scale (optional for smoother transition)
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, originalTimeScale, i);
            yield return null;
        }

        // Ensure time scale is fully restored (optional)
        Time.timeScale = originalTimeScale;
    }
}
