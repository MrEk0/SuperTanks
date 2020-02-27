using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataSaver 
{
    const string FILENAME = "volume.data";

    public static void Save(float musicVolume, float soundVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetFilePath();

        FileStream stream = new FileStream(path, FileMode.Create);
        VolumeData volumeData = new VolumeData(soundVolume, musicVolume);

        formatter.Serialize(stream, volumeData);
        stream.Close();
    }

    public static VolumeData Load()
    {
        string path = GetFilePath();
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            VolumeData volumeData = formatter.Deserialize(stream) as VolumeData;
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
        //Debug.Log(filePath);
        return filePath;
    }
}
