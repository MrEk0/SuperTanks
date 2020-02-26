using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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

    public void PauseGame()
    {
        AudioManager.PlayUIButtonAudio();
        IsGamePause = true;
        AudioManager.StopAllTankSounds();
    }

    public void ResumeGame()
    {
        //AudioManager.PlayUIButtonAudio();
        IsGamePause = false;
    }
}
