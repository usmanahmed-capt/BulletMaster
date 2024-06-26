using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BgAudioSource;
    public AudioSource BgAudioSource2;
    public AudioSource BgAudioSource3;
    public AudioSource BgAudioSource4;
    public AudioSource ClickAudioSource;
    public AudioSource GameOverAudioSource;
    public AudioSource CoinsHitAudio;
    public AudioSource PlayWithClipAudioSource;
    public AudioSource PlayWithClipAudioSourceAnVolume;
    public AudioSource PlayWithClipAudioSourceAnVolume_2;
    public AudioSource PlayWithAudioSourceAnVolume_3;
    public AudioSource PlayWithAudioSourceAnVolume_4;
    public AudioSource GunAudioSource;
    public AudioSource LoopAudioSource;
    public bool isMultipleSounds = false;
    public bool isSound = true;
    public AudioSource[] bgSounds;
    public AudioSource[] ClickAudioSources;
    private List<GameObject> BgAudiList = new List<GameObject>();
    private List<AudioSource> BgAudiListAudio = new List<AudioSource>();

    #region OneShotSoundClips

    /*
	 * Audio Clip Files for One Shot
	 */

    public AudioClip buttonClickSound;
    public AudioClip GameOverClip;
    public AudioClip CoinsClip;
    public AudioClip GameWinClip;
    public AudioClip RewardWinClip;
    public AudioClip popupopen;
    public AudioClip popupClose;
    public AudioClip deathEnemyClip;
    public AudioClip StarClip;
    public AudioClip boxHit, plankHit, groundHit, explodeHit, gunShot, MatelplankHit, Glasshit,UnlockItem,ClaimRewad,headShoot,AmmoReLoad;
    //  public float SfxSoundLevel;
    public void SoundGameObject(int buildIndux)
    {
        //for (int i = 0; i < bgSounds.Length; i++)
        //{
        //    bgSounds[i].gameObject.SetActive(false);
        //}
        if (buildIndux == 1)
        {
            bgSounds[0].gameObject.SetActive(false);
            if (!bgSounds[buildIndux].gameObject.activeSelf)
                bgSounds[buildIndux].gameObject.SetActive(true);
        }
        else
        {
            bgSounds[1].gameObject.SetActive(false);
            if (!bgSounds[buildIndux].gameObject.activeSelf)
                bgSounds[buildIndux].gameObject.SetActive(true);
        }


    }
    #endregion

    #region InGameLoopSoundClips

    /*
	 * Audio Clip Files for In Game Loop Sound
	 */

    #endregion

    #region BackGroundSoundClips

    /*
	 * Audio Clip Files for BG Loop Sound
	 */



    #endregion


    //public AudioClip[] mensterPattrenSound;

    #region DefaultMethods

    //TODO: Make sure you override your awake by override keyword
    /*override*/
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
        BgAudiList.Add(BgAudioSource.gameObject);
        BgAudiList.Add(BgAudioSource2.gameObject);
        BgAudiList.Add(BgAudioSource3.gameObject);
        BgAudiList.Add(BgAudioSource4.gameObject);

        BgAudiListAudio.Add(BgAudioSource);
        BgAudiListAudio.Add(BgAudioSource2);
        BgAudiListAudio.Add(BgAudioSource3);
        BgAudiListAudio.Add(BgAudioSource4);

    }

    void Start()
    {

        //PlayerPrefs.DeleteAll ();
        /*
         * If Sound is mute previously Mute the sound.
         */
        //TODO : Need to change issound with your player prefrence
        if (!isSound) {
            GetComponent<AudioSource>().mute = true;
        } else {
            GetComponent<AudioSource>().mute = false;
        }

        /*
		 * Checking whether dual sound enable or not.
		 * If Enable setting MenuBGSound and GameBGSound Accourding.
		 * Else always set GameBGSound.
		 */



        /*
		 * If Sound is not playing Play the musicAudioSource
		 */

        if (!GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().Play();
        }
    }

    #endregion



    #region InGameLoopSoundMethods

    /*
	 * Stop Playing previous in game loop sound
     * checking is new in game loop sound available playing it.
	 */



    public void StopInGameLoop()
    {
        BgAudioSource.Stop();
    }

    #endregion

    #region BGSoundMethods

    /*
	 * Playing Different Background Sounds
	 */


    public void BackgroundSoundOn()
    {

        for (int i = 0; i < bgSounds.Length; i++)
        {
            bgSounds[i].mute = false;
        }

    }
    public void BackgroundSoundOf()
    {
        for (int i = 0; i < bgSounds.Length; i++)
        {
            bgSounds[i].mute = true;
        }

    }





    #endregion

    #region OneShotSoundMethods

    /*
	 * Playing one shot for each OneShotSound.
     * Muting and UnMuting sound Accordingly.
	 */


    public void PlayButtonClickSound()
    {
        if (buttonClickSound) {
            Utilities.PlaySFX(ClickAudioSource, buttonClickSound, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayCoinsSound()
    {
        if (CoinsClip)
        {
            Utilities.PlaySFX(ClickAudioSource, CoinsClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    //  public AudioClip popupopen;
    //  public AudioClip popupClose;
    //  public AudioClip PageOpen;



    public void PlaypopUpOpen()
    {
        if (buttonClickSound)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, popupopen, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlaypopupClose()
    {
        if (buttonClickSound)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, popupClose, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayScrollSound()
    {
        if (buttonClickSound)
        {
            //  Utilities.PlaySFX(ClickAudioSource, ScrollSoundclip, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayRewardWinSound()
    {
        if (buttonClickSound)
        {
            Utilities.PlaySFX(ClickAudioSource, RewardWinClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayGameWinSound()
    {
        if (GameWinClip)
        {
            Utilities.PlaySFX(GameOverAudioSource, GameWinClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayStarSound()
    {
        if (StarClip)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, StarClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayGameOverSound()
    {
        if (GameOverClip)
        {
            Utilities.PlaySFX(GameOverAudioSource, GameOverClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlaySounWithVolume(AudioClip Clip, float Volume)
    {
        if (Clip)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume, Clip, Volume, false);
        }
    }
    public void PlaySounWithVolume_2(AudioClip Clip, float Volume)
    {
        if (Clip)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, Clip, Volume, false);
        }
    }
    public void PlayEnemyDeath()
    {
        if (deathEnemyClip)
        {
            Utilities.PlaySFX(PlayWithAudioSourceAnVolume_3, deathEnemyClip, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayplayBoxhitSound()
    {
        if (boxHit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, boxHit, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlaygroundHitSound()
    {
        if (groundHit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, groundHit, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayplankHitSound()
    {
        if (plankHit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, plankHit, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayGlassHitSound()
    {
        if (Glasshit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, Glasshit, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayMetalplankHitSound()
    {
        if (MatelplankHit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, MatelplankHit, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlaygunShotSound()
    {
        if (gunShot)
        {
            Utilities.PlaySFX(GunAudioSource, gunShot, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlaygunEmptySound()
    {
        if (AmmoReLoad)
        {
            Utilities.PlaySFX(GunAudioSource, AmmoReLoad, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayexplodeHitSound()
    {
        if (explodeHit)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, explodeHit, GameController.Instance.SfxSoundLevel, false);
        }
    }

    public void PlayheadShootSound()
    {
        if (headShoot && !PlayWithAudioSourceAnVolume_4.isPlaying)
        {
            Utilities.PlaySFX(PlayWithAudioSourceAnVolume_4, headShoot, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayUnlockItemSound()
    {
        if (UnlockItem)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, UnlockItem, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlayClainRewardSound()
    {
        if (ClaimRewad)
        {
            Utilities.PlaySFX(PlayWithClipAudioSourceAnVolume_2, ClaimRewad, GameController.Instance.SfxSoundLevel, false);
        }
    }
    public void PlaySoundWithClip2(AudioClip clip)
    {
        if (clip && !PlayWithClipAudioSource.isPlaying)
        {
            Utilities.PlaySFX(PlayWithClipAudioSource, clip, GameController.Instance.SfxSoundLevel, false);
        }
    }


    public void stopSound()
    {
        LoopAudioSource.Stop();
    }
    public void stopSoundTimerEnd()
    {
        LoopAudioSource.Stop();
    }


    public void MuteSound()
    {
        isSound = false;
        for (int i = 0; i < ClickAudioSources.Length; i++)
        {
            ClickAudioSources[i].mute = true;
        }

        //TODO : Need to save the sound states 
    }

    public void UnMuteSound()
    {
        isSound = true;
        for (int i = 0; i < ClickAudioSources.Length; i++)
        {
            ClickAudioSources[i].mute = false;
        }
        //TODO : Need to save the sound states 
    }

    public void BgSoundLow()
    {
        BgAudioSource.volume = .3f;
    }

    public void BgSoundHigh()
    {
        BgAudioSource.volume = 1f;
    }

    public void AudioSoundOfOn()
    {

        ClickAudioSource.mute = PlayerPrefs.GetInt("sound") == 0 ? ClickAudioSource.mute = false : ClickAudioSource.mute = true;
        GameOverAudioSource.mute = PlayerPrefs.GetInt("sound") == 0 ? GameOverAudioSource.mute = false : GameOverAudioSource.mute = true;
        CoinsHitAudio.mute = PlayerPrefs.GetInt("sound") == 0 ? CoinsHitAudio.mute = false : CoinsHitAudio.mute = true;
        PlayWithClipAudioSource.mute = PlayerPrefs.GetInt("sound") == 0 ? PlayWithClipAudioSource.mute = false : PlayWithClipAudioSource.mute = true;
        PlayWithClipAudioSourceAnVolume.mute = PlayerPrefs.GetInt("sound") == 0 ? PlayWithClipAudioSourceAnVolume.mute = false : PlayWithClipAudioSource.mute = true;
        PlayWithClipAudioSourceAnVolume_2.mute = PlayerPrefs.GetInt("sound") == 0 ? PlayWithClipAudioSourceAnVolume.mute = false : PlayWithClipAudioSource.mute = true;
        PlayWithAudioSourceAnVolume_3.mute = PlayerPrefs.GetInt("sound") == 0 ? PlayWithAudioSourceAnVolume_3.mute = false : PlayWithAudioSourceAnVolume_3.mute = true;
        PlayWithAudioSourceAnVolume_4.mute = PlayerPrefs.GetInt("sound") == 0 ? PlayWithAudioSourceAnVolume_4.mute = false : PlayWithAudioSourceAnVolume_4.mute = true;
        GunAudioSource.mute = PlayerPrefs.GetInt("sound") == 0 ? GunAudioSource.mute = false : GunAudioSource.mute = true;
        LoopAudioSource.mute = PlayerPrefs.GetInt("sound") == 0 ? LoopAudioSource.mute = false : LoopAudioSource.mute = true;
    }
    public void AudioMusicOfOn()
    {
        BgAudioSource.mute = PlayerPrefs.GetInt("music") == 0 ? BgAudioSource.mute = false : BgAudioSource.mute = true;
        BgAudioSource2.mute = PlayerPrefs.GetInt("music") == 0 ? BgAudioSource2.mute = false : BgAudioSource2.mute = true;
        BgAudioSource3.mute = PlayerPrefs.GetInt("music") == 0 ? BgAudioSource3.mute = false : BgAudioSource3.mute = true;
        BgAudioSource4.mute = PlayerPrefs.GetInt("music") == 0 ? BgAudioSource4.mute = false : BgAudioSource4.mute = true;
    }


    public void BgSoundActiveLevelVice(int ActicIndux)
    {
        for (int i = 0; i < BgAudiList.Count; i++)
        {
            if(i!=ActicIndux)
                BgAudiList[i].SetActive(false);

        }
        if (!BgAudiList[ActicIndux].activeSelf)
            BgAudiList[ActicIndux].SetActive(true);

    }

    public void BgSoundActiveLevelVolumeLow(int ActicIndux)
    {
        float Volume = .75f;
        if (ActicIndux == 0 || ActicIndux == 2)
            Volume = .3f;       
        if (BgAudiListAudio[ActicIndux])
        {
            BgAudiListAudio[ActicIndux].volume = Volume;
        }

    }
    public void BgSoundActiveLevelVolumeRestVolume(int ActicIndux)
    {
        float Volume =1f;
        if (ActicIndux == 0 || ActicIndux == 2)
            Volume = .4f;

        if (BgAudiListAudio[ActicIndux])
            BgAudiListAudio[ActicIndux].volume = Volume;

    }
    #endregion
}