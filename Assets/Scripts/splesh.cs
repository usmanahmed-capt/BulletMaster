using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class splesh : MonoBehaviour
{
    public Image spleshImageClid;
    //public RectTransform rectTransform;
    public float TimeInSecond;
    IEnumerator coroHold;
    private bool CanShowAppOpen;
    public float LoadingTime;
    public GameObject MainPage;
    public GameObject Particle;
    void Start()
    {
        AdMob.Instance.HideAppOpen.AddListener(delegate
        {
            if (gameObject.activeSelf)
            {
                CancelInvoke(nameof(showLoadingIfApOpenNotavaible));
                Invoke(nameof(SceneLoad), .2f);
            }

        });
        if (PlayerPrefs.GetInt("FirsTimeAppOpen") == 1)
        {
            CanShowAppOpen = true;
            TimeInSecond = 4.2f;
            LoadingTime = 4.5f;
        }
        else
        {
            CanShowAppOpen = true;
            TimeInSecond = 3.2f;
            LoadingTime = 3.5f;
        }

        StartFillBar();
        Invoke(nameof(EventSent),2f);
     
    }

    void EventSent() 
    {
        firebasecall.Instance.LogEventGame("loading_start");//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("loading_start");//sohail 
    }
    public IEnumerator MoveOverSeconds()
    {
        if (CanShowAppOpen)
        {
            float elapsedTime = 0;
            while (elapsedTime < TimeInSecond)
            {
                elapsedTime += Time.deltaTime;
                //if (AdMob.Instance.appOpenAd != null)
                //{
                //    AdMob.Instance.showAppOpenOnStart();
                //    spleshImageClid.DOKill();
                //    StopCoroutine(coroHold);
                //    Invoke(nameof(showLoadingIfApOpenNotavaible),2f);
                //}
                yield return new WaitForEndOfFrame();
            }
        }
    }
    void showLoadingIfApOpenNotavaible()
    {
        spleshImageClid.DOFillAmount(1f, .5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            spleshImageClid.fillAmount = 1f;
            SceneLoad();
        });
    }

    void MoveToZeroX()
    {
        spleshImageClid.DOFillAmount(1f, LoadingTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            
            spleshImageClid.fillAmount = 1f;
            SceneLoad();
        });
    }

    void MoveToZeroXAferAddCalling()
    {
        spleshImageClid.DOFillAmount(1f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            spleshImageClid.fillAmount = 1f;
            SceneLoad();
        });
    }
    public void StartFillBar()
    {
        MoveToZeroX();
        coroHold = MoveOverSeconds();
        StartCoroutine(coroHold);
    }

    void SceneLoad()
    {
        PlayerPrefs.SetInt("FirsTimeAppOpen", 1);
     
        // Debug.LogError("FirsTimeAppOpen");
        if (PlayerPrefs.GetInt("FirsTimeLevelOne", 0) == 0)
        {
            MainPage.SetActive(true);
            //Particle.SetActive(true);
        }
        else 
        {
            GameController.Instance.currenLvlClick = 1;
            SceneManager.LoadScene(GameController.Instance.currenLvlClick);
        }
      
        firebasecall.Instance.LogEventGame("loading_end");//sohail
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("loading_end");//sohail 
        gameObject.SetActive(false);
    }
}
  

