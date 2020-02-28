using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] int numberOfLevels = 5;

    private int numberOfOpenedLevels = 0;

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
        DataSaver.Save(numberOfOpenedLevels);
    }

    public void LoadProgress()
    {
        //ProgressData progress = ProgressSaver.Load();
        PlayerData progress = DataSaver.Load();

        if(progress!=null)
        {
            numberOfOpenedLevels = progress.levelProgress;
            OpenNewLevels();
        }
    }

    public void LevelUp()
    {
        numberOfOpenedLevels++;
        Debug.Log(numberOfOpenedLevels);
        SaveProgress();
    }

    public void OpenNewLevels()
    {
        if (instance == null)
            return;

        for (int i = 0; i<=numberOfOpenedLevels; i++)
        {
            if(levelButtons!=null && levelButtons.Contains(levelButtons[i]))
            levelButtons[i].GetComponent<LevelButton>().RevealButton();
        }
    }
}
