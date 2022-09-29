using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    List<string> keys = new List<string>();

    void Awake()
    {
        int keyManagerCount = FindObjectsOfType<KeyManager>().Length;

        if (keyManagerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddKey(string k) { keys.Add(k); }
    public bool ContainsKey(string k) { return keys.Contains(k); }
}
