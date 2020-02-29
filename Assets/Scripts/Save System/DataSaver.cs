using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataSaver 
{
    const string FILENAME = "player.data";

    public static void Save(float soundVolume, float musicVolume, int levelProgress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetFilePath();

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData volumeData = new PlayerData(soundVolume, musicVolume, levelProgress);

        formatter.Serialize(stream, volumeData);
        stream.Close();
    }

    public static PlayerData Load()
    {
        string path = GetFilePath();
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData volumeData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return volumeData;
        }
        else
        {
            //throw new FileNotFoundException("File has not been found", FILENAME);
            return null;
        }
    }

    private static string GetFilePath()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILENAME);
        return filePath;
    }

}
