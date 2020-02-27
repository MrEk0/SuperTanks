using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProgressSaver 
{
    const string FILENAME = "progress.data";

    public static void Save(int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetFilePath();

        FileStream stream = new FileStream(path, FileMode.Create);
        ProgressData progressData = new ProgressData(level);

        formatter.Serialize(stream, progressData);
        stream.Close();
    }

    public static ProgressData Load()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData progressData = formatter.Deserialize(stream) as ProgressData;
            stream.Close();

            return progressData;
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
