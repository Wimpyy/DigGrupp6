using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicHolder : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField] bool mute;
    SceneMusic sceneMusic;

    private void Start()
    {
        StartCoroutine(SetClip());
    }

    IEnumerator SetClip()
    {
        yield return null;
        yield return null;
        sceneMusic = FindObjectOfType<SceneMusic>();

        if (mute && sceneMusic.GetClip() != musicClip)
        {
            sceneMusic.Stop();
        }
        else
        {
            sceneMusic.SetClip(musicClip);
        }
    }
}
