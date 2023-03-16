using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    AudioSource audioSource;
    MusicPlayer musicPlayer;

    void Awake()
    {
        if (FindObjectsOfType<SceneMusic>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            audioSource = GetComponentInChildren<AudioSource>();
            musicPlayer = GetComponentInChildren<MusicPlayer>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetClip(AudioClip clip)
    {
        if (audioSource != null)
        {
            //Changes clip if that clip is not playing, otherwise continues playing the clip.
            if (audioSource.clip != clip || !musicPlayer.IsPlaying())
            {
                audioSource.clip = clip;
                musicPlayer.PlayMusic();
            }
        }
    }

    public AudioClip GetClip()
    {
        return audioSource.clip;
    }

    public void Stop()
    {
        musicPlayer.StopMusic();
    }
}
