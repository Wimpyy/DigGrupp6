using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    SaveManager savemanager;

    void Start()
    {
        savemanager = FindObjectOfType<SaveManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            savemanager.saveData.playerPos = other.transform.position;
            savemanager.Save();
        }
    }
}
