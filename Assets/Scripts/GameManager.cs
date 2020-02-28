using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] int numberOfLevels = 5;

    public int NumberOfOpenedLevels { get; private set; } = 0;

    public List<GameObject> levelButtons { get; set; }

    public bool IsGamePause { get; private set; } = false;

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

    public int GetLevelNumber()
    {
        return numberOfLevels;
    }

    public void PauseGame()
    {
        IsGamePause = true;
        AudioManager.StopAllTankSounds();
    }

    public void ResumeGame()
    {
        IsGamePause = false;
    }

    public void SaveProgress()
    {
        float sound = AudioManager.SoundVolume;
        float music = AudioManager.MusicVolume;
        DataSaver.Save(sound, music, NumberOfOpenedLevels);
    }

    public void LoadProgress()
    {
        PlayerData progress = DataSaver.Load();

        if(progress!=null)
        {
            NumberOfOpenedLevels = progress.levelProgress;
        }
        OpenNewLevels();
    }

    public void LevelUp(int sceneIndex)
    {
        if (sceneIndex <= NumberOfOpenedLevels)
            return;

            NumberOfOpenedLevels++;
            SaveProgress();
        
    }

    public void OpenNewLevels()
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
