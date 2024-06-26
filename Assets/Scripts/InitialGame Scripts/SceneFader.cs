using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;

    public float fadeSpeed2 = 1.5f;
    public GameObject loadingText;
    public static SceneFader instance;

    public bool isLoadWithStartFading;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }



    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();
    }

    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        //if (!Gamemanager.Instance.IsSpleshNotShowing)
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
        //else
        //    FadeImg.color = Color.clear;

    }
    void FadeToBlack2()
    {
        // Lerp the colour of the image between itself and black.
        //if (!Gamemanager.Instance.IsSpleshNotShowing)
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed2 * Time.deltaTime);
        //else
        //    FadeImg.color = Color.black;
    }

    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        //if (!Gamemanager.Instance.IsSpleshNotShowing)
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
        //else
        //    FadeImg.color = Color.black;
    }


    void StartScene()
    {
        // Fade the texture to clear.

            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
    }


    public IEnumerator EndSceneRoutine(string SceneNumber)
    {
        // Make sure the RawImage is enabled.

                FadeImg.enabled = true;
                //  FadeImg2.enabled = true;
                do
                {
                    // Start fading towards black.
                    FadeToBlack();
                    if (FadeImg.color.a >= 0.97f)
                    {
                        loadingText.SetActive(true);
                    }
                    // If the screen is almost black...
                    if (FadeImg.color.a >= 0.97f)
                    {
                        // ... reload the level
                        SceneManager.LoadScene(SceneNumber);
                        yield break;
                    }
                    else
                    {
                        yield return null;
                    }
                } while (true);


    }

    public void EndScene(string SceneNumber)
    {

        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }

    public void EndFader()
    {
        StartCoroutine("EndRoutine");
    }

    public IEnumerator EndRoutine()
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack2();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.97f)
            {
                FadeImg.enabled = true;
                sceneStarting = true;
                //  StartCoroutine("EndRoutine");
                StartScene();
                // ... reload the level
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }
    public IEnumerator EndSceneRoutineName(string SceneNumber)
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.97f)
            {
                // ... reload the level
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }
    public IEnumerator EndSceneRoutineIndux(int  SceneIndux)
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.97f)
            {
                // ... reload the level
                SceneManager.LoadScene(SceneIndux);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }
    public void EndSceneWithName(int SceneIndux)
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutineIndux", SceneIndux);
    }
}