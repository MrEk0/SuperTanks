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
        //if (instance == null)
        //    return;

        //AudioManager.PlayUIButtonAudio();
        IsGamePause = true;
        AudioManager.StopAllTankSounds();
    }

    public void ResumeGame()
    {
        //if (instance == null)
        //    return;

        IsGamePause = false;
    }

    public void SaveProgress()
    {
        //ProgressSaver.Save(numberOfOpenedLevels);
        DataSaver.Save(NumberOfOpenedLevels/*, numberOfOpenedLevels*/);
    }

    public void LoadProgress()
    {
        //ProgressData progress = ProgressSaver.Load();
        PlayerData progress = DataSaver.Load();

        if(progress!=null)
        {
            NumberOfOpenedLevels = progress.levelProgress;
            OpenNewLevels();
        }
    }

    public void LevelUp(int sceneIndex)
    {
        if (sceneIndex <= NumberOfOpenedLevels)
            return;

            NumberOfOpenedLevels++;
            Debug.Log(NumberOfOpenedLevels);
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
