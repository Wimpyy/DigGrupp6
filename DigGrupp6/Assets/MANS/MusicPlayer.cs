using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] float fadeInDuration;
    [SerializeField] float fadeOutDuration;

    AudioSource audioSource;
    bool isPlaying;
    public bool IsPlaying() { return isPlaying; }
    float fadeOutTimer;
    float fadeInTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isPlaying && fadeOutTimer > 0)
        {
            fadeOutTimer -= Time.deltaTime;
            audioSource.volume = Mathf.Clamp01(fadeOutTimer / fadeOutDuration);

            if (fadeOutTimer <= 0)
            {
                audioSource.Stop();
            }
        }

        if (isPlaying && fadeInTimer > 0)
        {
            fadeInTimer -= Time.deltaTime;
            audioSource.volume = 1 - Mathf.Clamp01(fadeInTimer / fadeInDuration);
        }
    }

    public void StopMusic()
    {
        fadeOutTimer = fadeOutDuration;
        isPlaying = false;
    }

    public void PlayMusic()
    {
        audioSource.Play();
        audioSource.volume = 0;
        fadeInTimer = fadeInDuration;
        isPlaying = true;
    }
}
