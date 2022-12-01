using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Unity.Mathematics;

public class SaveManager : MonoBehaviour
{
    [Serializable] public class SaveData
    {
        public float3 playerPos;
    }

    public SaveData saveData;
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/SaveDataGrupp6.dat";

        if (!HasSaveFile())
        {
            saveData = new SaveData();
        }
    }

    public bool HasSaveFile()
    {
        return File.Exists(filePath);
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(filePath);

        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public SaveData Load()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Open(filePath, FileMode.Open);
        saveData = (SaveData)binaryFormatter.Deserialize(saveFile);
        saveFile.Close();

        return saveData;
    }

    public void Delete()
    {
        File.Delete(filePath);
        saveData = new SaveData();
    }
}
