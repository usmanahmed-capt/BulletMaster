using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using TMPro;
public class MainMenu : MonoBehaviour
{
   
    public GameObject settingPanal;
    public GameObject shopPanal;
    public GameObject CharSelectionPanal;
    public GameObject MainMenuObj;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
       
    }

    void GameStartMenuShow() 
    {
      //  MainMenuObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 83f), .2f).SetEase(Ease.OutBounce);
    }
    private void Update()
    {
       
    }
    public void OnClickLeaderBoard()
    {
        SoundManager.Instance.PlayButtonClickSound();
        //this.Invoke(() =>
        //{
        //    SampleManager.Instance.ShowLeaderBoard();
        //}, 0.2f);
    }

    public void OnClickLeaderBoardClose()
    {
        SoundManager.Instance.PlayButtonClickSound();
        //this.Invoke(() =>
        //{
        //    SampleManager.Instance.LeaderBoard.SetActive(false);
        //}, 0.2f);
    }
    public void PlayBtnClick() 
    {
        SoundManager.Instance.PlayButtonClickSound();
        SceneFader.instance.EndSceneWithName(2);

    }
    public void SettingBtnClick()
    {
        
        SoundManager.Instance.PlayButtonClickSound();
        settingPanal.transform.parent.gameObject.SetActive(true);
     
     //   settingPanal.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f,83f), .2f).SetEase(Ease.Linear);
    }

    public void CharShopOpen()
    {
        SoundManager.Instance.PlayButtonClickSound();
        CharSelectionPanal.SetActive(true);
      //  CharSelectionPanal.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), .2f).SetEase(Ease.Linear);
    }

    public void shopPanalOpen()
    {
        SoundManager.Instance.PlayButtonClickSound();
        shopPanal.SetActive(true);
       // shopPanal.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), .2f).SetEase(Ease.Linear);
    }

    public void shopPanalClose()
    {
        SoundManager.Instance.PlayButtonClickSound();
       
      //  shopPanal.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 2196f), .2f).SetEase(Ease.Linear);
        Invoke("onExitShope", 0.26f);
    }
    void onExitShope() 
    {
        shopPanal.SetActive(false);
    }


    public void SoundON() 
    {

    }
}
