using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance;
    public GameObject[] Player;
    public Transform PlayerPos;
    public PlayerController player;
    int buildIndex;
    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        PlayerPos.gameObject.SetActive(false);

         buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex >= 1 && buildIndex <= 10)
        {
            GameController.Instance.currPlayerIndux = 0;
            SoundManager.Instance.BgSoundActiveLevelVice(0);
        }
        else if (buildIndex >= 11 && buildIndex <= 20)
        {
            GameController.Instance.currPlayerIndux =1;
            SoundManager.Instance.BgSoundActiveLevelVice(1);
        }
        else if (buildIndex >= 21 && buildIndex <= 30)
        {
            GameController.Instance.currPlayerIndux = 2;
            SoundManager.Instance.BgSoundActiveLevelVice(2);
        }
        else if (buildIndex >= 31 && buildIndex <= 40)
        {
            GameController.Instance.currPlayerIndux = 3;
            SoundManager.Instance.BgSoundActiveLevelVice(3);
        }
       
        GameObject playerControler=Instantiate(Player[GameController.Instance.currPlayerIndux], PlayerPos.position, Quaternion.identity) ;
        player= playerControler.GetComponent<PlayerController>();

    }


    public void LowBgVolume() 
    {
        if (buildIndex >= 1 && buildIndex <= 10)
        {
            SoundManager.Instance.BgSoundActiveLevelVolumeLow(0);
        }
        else if (buildIndex >= 11 && buildIndex <= 20)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeLow(1);
        }
        else if (buildIndex >= 21 && buildIndex <= 30)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeLow(2);
        }
        else if (buildIndex >= 31 && buildIndex <= 40)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeLow(3);
        }
    }

    public void HighBgVolume()
    {
        if (buildIndex >= 1 && buildIndex <= 10)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeRestVolume(0);
        }
        else if (buildIndex >= 11 && buildIndex <= 20)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeRestVolume(1);
        }
        else if (buildIndex >= 21 && buildIndex <= 30)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeRestVolume(2);
        }
        else if (buildIndex >= 31 && buildIndex <= 40)
        {

            SoundManager.Instance.BgSoundActiveLevelVolumeRestVolume(3);
        }
    }

   
}
