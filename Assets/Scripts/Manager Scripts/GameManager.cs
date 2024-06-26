using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemyCount = 1;
    [HideInInspector]
    public bool gameOver;

    public int blackBullets = 3;
    public int goldenBullets = 1;

    public GameObject blackBullet, goldenBullet;

    internal int levelNumber;

    public Animator fadeAnim;
    public Transform bulletHold;

    private int InitialBlackBullets;
    private int InitialgoldenBullets;
    public List<GameObject> Bullet = new List<GameObject>();
    string SceneName;
   internal  int BuildIndux;
    public List<Enemy> EnemyAll = new List<Enemy>();
    public List<Enemy> EnemyAllEnmy = new List<Enemy>();

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);
       
    }

    private void Start()
    {
        InitialBlackBullets = blackBullets;
        InitialgoldenBullets = goldenBullets;
        GamePlay.Instance.player.ammo = blackBullets + goldenBullets;
        for (int i = 0; i < blackBullets; i++)
        {
            GameObject bbTemp = Instantiate(blackBullet);
            bbTemp.transform.SetParent(bulletHold);
            bbTemp.transform.localScale = Vector3.one;
            Bullet.Add(bbTemp);
        }

        for (int i = 0; i < goldenBullets; i++)
        {
            GameObject gbTemp = Instantiate(goldenBullet);
            gbTemp.transform.SetParent(bulletHold);
            gbTemp.transform.localScale = Vector3.one;
            Bullet.Add(gbTemp);
        }
        SceneName = SceneManager.GetActiveScene().name;
        BuildIndux = SceneManager.GetActiveScene().buildIndex;
        GameObject[] EnemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < EnemyObj.Length; i++)
        {
            EnemyAll.Add(EnemyObj[i].GetComponent<Enemy>());
            EnemyAllEnmy.Add(EnemyObj[i].GetComponent<Enemy>());
        }
    }

    internal void SpownBullets()
    {
        blackBullets = InitialBlackBullets;
        goldenBullets = InitialgoldenBullets;
        if (Bullet.Count > 0) 
        {
            for (int i = 0; i < Bullet.Count; i++)
            {
                Destroy(Bullet[i]);
            }
        }
        Bullet.Clear();
        GamePlay.Instance.player.ammo = blackBullets + goldenBullets;
        for (int i = 0; i < blackBullets; i++)
        {
            GameObject bbTemp = Instantiate(blackBullet);
            bbTemp.transform.SetParent(bulletHold);
            bbTemp.transform.localScale = Vector3.one;
            Bullet.Add(bbTemp);
        }

        for (int i = 0; i < goldenBullets; i++)
        {
            GameObject gbTemp = Instantiate(goldenBullet);
            gbTemp.transform.SetParent(bulletHold);
            gbTemp.transform.localScale = Vector3.one;
            Bullet.Add(gbTemp);
        }
    }

    void Update()
    {
        if (!gameOver && GamePlay.Instance.player.ammo <= 0 && enemyCount > 0 
            && GameObject.FindGameObjectsWithTag("Bullet").Length <= 0 && GameController.Instance.CanPlayOn)
        {
           
            gameOver = true;
            GameUI.instace.GameOverScreen();
        } 
    }

    public void CheckBullets()
    {
        if(goldenBullets > 0)
        {
            goldenBullets--;
         // GameObject bullet=   GameObject.FindGameObjectWithTag("GoldenBullet");
            //bullet.transform.GetChild(1).gameObject.SetActive(false);
            //bullet.tag = "Untagged";
            Bullet[GamePlay.Instance.player.ammo].transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(blackBullets > 0)
        {
            blackBullets--;
            //GameObject bullet = GameObject.FindGameObjectWithTag("BlackBullet");
            //bullet.transform.GetChild(1).gameObject.SetActive(false);
            //bullet.tag = "Untagged";
            Bullet[GamePlay.Instance.player.ammo].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            GameUI.instace.WinScreen(GameController.Instance.currenLvlClick);
            if (GameUI.instace.buildIndexx != GameController.Instance.previouseBuildIndux)
            {
                
                PlayerPrefs.SetInt("Level", GameController.Instance.currenLvlClick + 1);
            }
        }
    }

    public  IEnumerator FadeIn(int SceneIndex)
    {
        fadeAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneIndex);
    }

   
    public void NextLevel()
    {
        SoundManager.Instance.PlayButtonClickSound();
        if (BuildIndux == 3 || BuildIndux == 7 || BuildIndux == 10 || BuildIndux == 13 || BuildIndux == 16 || BuildIndux == 19
            || BuildIndux == 20 || BuildIndux == 22 || BuildIndux == 25 || BuildIndux == 27 || BuildIndux == 30 || BuildIndux == 33
            || BuildIndux == 36 || BuildIndux == 39)
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads = 1;
                AdsManager.Insatance.ShowInterstitial("NextLevelInterstatial");
            }
            else if (AdMob.Instance.timer > 10)
            {
                GameController.Instance.AfterCallingInterstatialads = 1;
                AdsManager.Insatance.ShowInterstitial("NextLevelInterstatial");
            }
            else
            {
                NextCallingAfterAds();
            }
        }
        else
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads =1;
                AdsManager.Insatance.ShowInterstitial("NextLevelInterstatial");
            }
            else
            {
                NextCallingAfterAds();
            }
        }
      
    }

    internal void NextCallingAfterAds()
    {
        GamePlay.Instance.HighBgVolume();
        if (GameUI.instace.buildIndexx != GameController.Instance.previouseBuildIndux)
        {
            GameController.Instance.previouseBuildIndux = GameController.Instance.currenLvlClick;
            GameController.Instance.currenLvlClick++;
        }
        if (GameController.Instance.currenLvlClick >= 121)
        {
            Exit_Fun();
            return;
        }
        if ((BuildIndux + 1) < 40)
        {
            StartCoroutine(FadeIn(BuildIndux + 1));
        }
        else
        {
                int Range = UnityEngine.Random.Range(0, 3);

                if (Range == 0)
                {
                    StartCoroutine(FadeIn(11));
                }
                if (Range == 1)
                {
                    StartCoroutine(FadeIn(21));
                }
                if (Range == 2)
                {
                    StartCoroutine(FadeIn(31));
                }
        }
    }

    public void HideBigBanner() 
    {
        AdsManager.Insatance.HideMREC();
    }

    public void Restart()
    {
        SoundManager.Instance.PlayButtonClickSound();

        if (BuildIndux == 3 || BuildIndux == 7 || BuildIndux == 10 || BuildIndux == 13 || BuildIndux == 16 || BuildIndux == 19
            || BuildIndux == 20 || BuildIndux == 22 || BuildIndux == 25 || BuildIndux == 27 || BuildIndux == 30 || BuildIndux == 33
            || BuildIndux == 36 || BuildIndux == 39)
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads = 2;
                AdsManager.Insatance.ShowInterstitial("RestartIntersatial");
            }
            else if (AdMob.Instance.timer > 10)
            {
                GameController.Instance.AfterCallingInterstatialads = 2;
                AdsManager.Insatance.ShowInterstitial("RestartIntersatial");
            }
            else 
            {
                Restart_Fun();
            }
        }
        else 
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads = 2;
                AdsManager.Insatance.ShowInterstitial("RestartIntersatial");
            }
            else 
            {
                Restart_Fun();
            }
        }
       
    }


    public void Exit()
    {
        SoundManager.Instance.PlayButtonClickSound();
        if (BuildIndux == 3 || BuildIndux == 7 || BuildIndux == 10 || BuildIndux == 13 || BuildIndux == 16 || BuildIndux == 19
           || BuildIndux == 20 || BuildIndux == 22 || BuildIndux == 25 || BuildIndux == 27 || BuildIndux == 30 || BuildIndux == 33
           || BuildIndux == 36 || BuildIndux == 39)
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads = 3;
                AdsManager.Insatance.ShowInterstitial("MainMenutIntersatial");
            }
            else if (AdMob.Instance.timer > 10)
            {
                GameController.Instance.AfterCallingInterstatialads = 3;
                AdsManager.Insatance.ShowInterstitial("MainMenutIntersatial");
            }
            else
            {
                Exit_Fun();
            }
        }
        else
        {
            if (AdMob.ShowAd)
            {
                GameController.Instance.AfterCallingInterstatialads = 3;
                AdsManager.Insatance.ShowInterstitial("MainMenutIntersatial");
            }
            else
            {
                Exit_Fun();
            }
        }
    }

    //public void Exit_playmode()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Exit_Fun();
    //}

    //public void Restar_PlayMode()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Restart_Fun();
    //}

    //public void Exit_Win()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Exit_Fun();
    //}

    //public void Restart_Win()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Restart_Fun();
    //}

    //public void Exit_Fail()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Exit_Fun();
    //}

    //public void Restar_Fail()
    //{
    //    SoundManager.Instance.PlayButtonClickSound();
    //    Restart_Fun();

    //}

    internal void Exit_Fun()
    {
        GameController.Instance.IsFromGameBack = true;
        GamePlay.Instance.HighBgVolume();
        firebasecall.Instance.LogEventGame(SceneName + "_toMainMenu");//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(SceneName + "_MainMenu");//sohail
        StartCoroutine(FadeIn(0));
    }

    internal void Restart_Fun()
    {
        GamePlay.Instance.HighBgVolume();
        StartCoroutine(FadeIn(BuildIndux));
        firebasecall.Instance.LogEventGame(SceneName + "_Restart");//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent(SceneName + "_Restart");//sohail
    }
}
