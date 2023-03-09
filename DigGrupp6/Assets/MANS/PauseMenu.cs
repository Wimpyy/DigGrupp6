using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    public bool IsPaused() { return isPaused; }
    InputManager inputManager;
    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        inputManager = FindObjectOfType<InputManager>();
        inputManager.PauseEvent += Pause;
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            canvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            canvas.enabled = false;
        }
    }
}
