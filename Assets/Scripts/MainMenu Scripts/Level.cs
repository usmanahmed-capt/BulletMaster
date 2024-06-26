using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private Button levelBtn;

    public int levelReq;
    public GameObject[] stars;
    public int Star;
    void Start()
    {
        PlayerPrefs.SetInt("Level", 120);
        levelBtn = GetComponent<Button>();
        if (PlayerPrefs.GetInt("Level", 1) >= levelReq)
        {
            levelBtn.onClick.AddListener(() => LoadLevel());
            GetComponent<CanvasGroup>().alpha = 1f;
            Star =  PlayerPrefs.GetInt("lvl" + levelReq);
            for (int i = 0; i < Star; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else
        {
            if(levelReq<=40)
                GetComponent<CanvasGroup>().alpha = .7f;
            else
                GetComponent<CanvasGroup>().alpha = .45f;


        }

        transform.GetChild(0).GetComponent<Text>().text = levelReq.ToString();

    }

    internal int GetStars()
    {
        Star = PlayerPrefs.GetInt("lvl" + levelReq);
        return Star;
    }

    public void LoadLevel()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameController.Instance.currenLvlClick = levelReq;

        if ((GameController.Instance.currenLvlClick) < 40)
        {

            SceneManager.LoadScene(gameObject.name);
        }
        else
        {
            int Range = UnityEngine.Random.Range(0, 3);

            if (Range == 0)
            {
                SceneManager.LoadScene(11);
            }
            if (Range == 1)
            {
                SceneManager.LoadScene(21);
            }
            if (Range == 2)
            {
                SceneManager.LoadScene(31);
            }
        }
    }
}
