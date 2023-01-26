using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float fadeTime;
    [SerializeField] CanvasGroup fadeCanvasGroup;

    bool fadeIn;
    bool fadeOut;
    string sceneName;
    string oldSceneName;

    void Awake()
    {
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            fadeCanvasGroup.alpha = 1;
            fadeIn = true;
        }
    }

    void Update()
    {
        if (fadeIn)
        {
            fadeCanvasGroup.alpha -= (1 / fadeTime) * Time.deltaTime;

            if (fadeCanvasGroup.alpha <= 0)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            fadeCanvasGroup.alpha += (1 / fadeTime) * Time.deltaTime;

            if (fadeCanvasGroup.alpha >= 1)
            {
                oldSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(sceneName);
                fadeOut = false;
                fadeIn = true;
            }
        }
    }

    public void LoadScene(string _sceneName)
    {
        fadeCanvasGroup.alpha = 0;
        sceneName = _sceneName;
        fadeOut = true;
    }

    public string GetPreviousScene() { return oldSceneName; }
}
