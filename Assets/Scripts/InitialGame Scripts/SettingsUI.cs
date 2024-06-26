using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
public class SettingsUI : MonoBehaviour
{
    
    public GameObject SONSp,SOffSp;
    public GameObject MONSp, MOffSp;
    public GameObject vibONSp, vibOffSp;
    public Image soundImage;
    public Image MusicImage;
    public Image VibarationImage;
    public GameObject Settingpanal;

    //public anim
    private void Start()
    {
        PlayerPrefs.SetInt("music", 0);
        PlayerPrefs.SetInt("sound", 0);
        PlayerPrefs.SetInt("vibration", 0);
        GameController.Instance.vibrationValue = PlayerPrefs.GetInt("vibration", 0);
        //MusicImage.sprite = PlayerPrefs.GetInt("music") == 0 ? MONSp : MOffSp;
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MONSp.SetActive(true);
            MOffSp.SetActive(false);
        }
        else {
            MONSp.SetActive(false);
            MOffSp.SetActive(true);
        }

        if (PlayerPrefs.GetInt("sound") == 0)
        {
            SONSp.SetActive(true);
            SOffSp.SetActive(false);
        }
        else
        {
            SONSp.SetActive(false);
            SOffSp.SetActive(true);
        }
        if (PlayerPrefs.GetInt("vibration") == 0)
        {
            vibONSp.SetActive(true);
            vibOffSp.SetActive(false);
        }
        else
        {
            vibONSp.SetActive(false);
            vibOffSp.SetActive(true);
        }
        /* soundImage.sprite =*/
       // PlayerPrefs.GetInt("sound") == 0 ? SONSp : SOffSp;
       ///* VibarationImage.sprite =*/ PlayerPrefs.GetInt("vibration") == 0 ? vibONSp : vibOffSp;

        SoundManager.Instance.AudioSoundOfOn();
        SoundManager.Instance.AudioMusicOfOn();
    }
   
    public void onMusicButton()
    {
        PlayerPrefs.SetInt("music", PlayerPrefs.GetInt("music") == 0 ? 1 : 0);
        //MusicImage.sprite = PlayerPrefs.GetInt("music") == 0 ? MONSp : MOffSp;
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MONSp.SetActive(true);
            MOffSp.SetActive(false);
        }
        else
        {
            MONSp.SetActive(false);
            MOffSp.SetActive(true);
        }
        SoundManager.Instance.AudioMusicOfOn();
        SoundManager.Instance.PlayButtonClickSound();
      //  print(PlayerPrefs.GetInt("music"));
    }

    public void onSoundButton ()
    {

        PlayerPrefs.SetInt("sound", PlayerPrefs.GetInt("sound")== 0? 1:0);
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            SONSp.SetActive(true);
            SOffSp.SetActive(false);
        }
        else
        {
            SONSp.SetActive(false);
            SOffSp.SetActive(true);
        }
       // soundImage.sprite = PlayerPrefs.GetInt("sound") == 0 ? SONSp : SOffSp;
        SoundManager.Instance.AudioSoundOfOn();
        SoundManager.Instance.PlayButtonClickSound();
      
    }
    public void onVibButton()
    {
        PlayerPrefs.SetInt("vibration", PlayerPrefs.GetInt("vibration") == 0 ? 1 : 0);
        if (PlayerPrefs.GetInt("vibration") == 0)
        {
            vibONSp.SetActive(true);
            vibOffSp.SetActive(false);
        }
        else
        {
            vibONSp.SetActive(false);
            vibOffSp.SetActive(true);
        }
       // VibarationImage.sprite = PlayerPrefs.GetInt("vibration") == 0 ? vibONSp : vibOffSp;
        GameController.Instance.vibrationValue= PlayerPrefs.GetInt("vibration", 0);
        SoundManager.Instance.PlayButtonClickSound();
      
    }

    public void onBackButton ()
    {

       // settingPanal.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 1500f), .25f).SetEase(Ease.Linear);
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void SettingPanalOn()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Settingpanal.SetActive(true);
    }

    public void SettingPanalOf()
    {
        SoundManager.Instance.PlayButtonClickSound();
        Settingpanal.SetActive(false);
    }

}
