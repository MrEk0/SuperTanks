using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] int numberOfLevels = 5;

    private int numberOfOpenedLevels = 1;
    private List<GameObject> levelButtons;

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

        levelButtons = new List<GameObject>();
    }

    public int GetLevelNumber()
    {
        return numberOfLevels;
    }

    public void PauseGame()
    {
        //AudioManager.PlayUIButtonAudio();
        IsGamePause = true;
        AudioManager.StopAllTankSounds();
    }

    public void ResumeGame()
    {
        IsGamePause = false;
    }

    //public void AddLevelButton(GameObject levelButton)
    //{
    //    levelButtons.Add(levelButton);
    //}

    //public void LevelUp()
    //{
    //    numberOfOpenedLevels++;
    //    OpenNewLevel(numberOfOpenedLevels);
    //    Debug.Log(numberOfOpenedLevels);
    //    //save
    //}
    
    //public void OpenNewLevel(int numberOfLevel)
    //{
    //    if (levelButtons.Contains(levelButtons[numberOfLevel-1]))
    //        levelButtons[numberOfLevel-1].GetComponent<LevelButton>().RevealButton();
    //}
}
