using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject Play, LevelSelection;
    public Level[] MyLvls;
    public Text Startext;
    public Text LevelPlayedText;
   public  int starToDisPlay;
    public GameObject ExitPanal;
    public GameObject Loading;
    public GameObject MainMenu;
   // public ParticleSystem MainParticle;
    private void Start()
    {
        Invoke(nameof(ShowStars), .1f);
        LevelPlayedText.text = "" + (PlayerPrefs.GetInt("Level"))+"/120";

        if (!GameController.Instance.IsLoading)
        {
            Loading.SetActive(true);
            GameController.Instance.IsLoading = true;
            MainMenu.SetActive(false);
        }
        else
        {
            if (GameController.Instance.IsFromGameBack)
            {
                Loading.SetActive(false);
                MainMenu.SetActive(true);
                LevelSelection.SetActive(true);
            }
            else 
            {
                Loading.SetActive(false);
                MainMenu.SetActive(true);
            
            }
            if (GameController.Instance.IsFromGameBack) 
            {
                AdsManager.Insatance.ShowBanner();
            }

            GameController.Instance.IsFromGameBack = false;
            GameController.Instance.previouseBuildIndux = 0;
        }
      
    }

    void ShowStars() 
    {
        for (int i = 1; i < MyLvls.Length; i++)
        {
            starToDisPlay += MyLvls[i].GetStars();
        }
        Startext.text = starToDisPlay.ToString();
    }
    public void PlayGame()
    {
        SoundManager.Instance.PlayButtonClickSound();
       
        LevelSelection.SetActive(true);
        //  SceneManager.LoadScene("Level1");
        
    }

    //private void OnValidate()
    //{
    //    for (int i = 1; i < MyLvls.Length; i++)
    //    {
    //        MyLvls[i].transform.name = "Level" + i;
    //        MyLvls[i].levelReq = i;
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                 ExitPanalShow();
            }
    }
    public void BackBtnFun() 
    {
        SoundManager.Instance.PlayButtonClickSound();
        LevelSelection.SetActive(false);
    }

    public void ExitPanalShow()
    {
        Time.timeScale = 0;
        ExitPanal.SetActive(true);
    }

    public void ExitPanalHide()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Time.timeScale = 1;
        ExitPanal.SetActive(false);
    }

    public void ApplicationQuit()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Application.Quit();
    }
}
