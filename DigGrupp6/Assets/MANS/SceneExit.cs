using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] Transform sceneEntrance;
    SceneLoader sceneLoader;

    void Start()
    {
        StartCoroutine(FindSceneLoader());
    }

    void Update()
    {
        
    }

    IEnumerator FindSceneLoader()
    {
        yield return null;
        yield return null;

        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneToLoad == sceneLoader.GetPreviousScene())
        {
            FindObjectOfType<PlayerMove>().transform.position = sceneEntrance.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneLoader.LoadScene(sceneToLoad);
        }
    }

    public string GetScene()
    {
        return sceneToLoad;
    }
}
