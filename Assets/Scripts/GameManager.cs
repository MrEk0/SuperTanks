using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] int numberOfLevels = 5;

    public static int NumberOfOpenedLevels { get; private set; } = 0;

    public static List<GameObject> levelButtons { get; set; }

    public static bool IsGamePause { get; private set; } = false;

    private void Awake()
    {
       if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadProgress();
    }

    public static int GetLevelNumber()
    {
        return instance.numberOfLevels;
    }

    public static void PauseGame()
    {
        if (instance == null)
            return;

        IsGamePause = true;
        AudioManager.StopAllTankSounds();
    }

    public static void ResumeGame()
    {
        if (instance == null)
            return;

        IsGamePause = false;
    }

    public static void SaveProgress()
    {
        if (instance == null)
            return;

        float sound = AudioManager.SoundVolume;
        float music = AudioManager.MusicVolume;
        DataSaver.Save(sound, music, NumberOfOpenedLevels);
    }

    public static void LoadProgress()
    {
        if (instance == null)
            return;

        PlayerData progress = DataSaver.Load();

        if(progress!=null)
        {
            NumberOfOpenedLevels = progress.levelProgress;
        }
        OpenNewLevels();
    }

    public static void LevelUp(int sceneIndex)
    {
        if (instance == null)
            return;

        if (sceneIndex <= NumberOfOpenedLevels)
            return;

            NumberOfOpenedLevels++;
            SaveProgress();       
    }

    public static void OpenNewLevels()
    {
        if (instance == null)
            return;

        for (int i = 0; i<=NumberOfOpenedLevels; i++)
        {
            if(levelButtons!=null && levelButtons.Contains(levelButtons[i]))
            levelButtons[i].GetComponent<LevelButton>().RevealButton();
        }
    }
}
