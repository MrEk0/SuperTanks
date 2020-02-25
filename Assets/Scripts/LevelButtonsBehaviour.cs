using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelButtonsBehaviour : MonoBehaviour
{
    [SerializeField] float timeToLoad = 2f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToTheNextLevel()
    {
        AudioManager.PlayUIButtonAudio();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;//????

        if (SceneManager.sceneCount <= nextSceneIndex)
        {
            StartCoroutine(LoadLevel(nextSceneIndex));
        }
        else
        {
            //animator.ResetTrigger("LoadLevel");
            //SceneManager.LoadScene("MainMenu");
            //ResumeGameSession();
            StartCoroutine(LoadLevel(0));         
        }
    }

    IEnumerator LoadLevel(int nextSceneIndex)
    {
        animator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(nextSceneIndex);
        AudioManager.PlayReadyGoAudio();
        //ResumeGameSession();
    }

    public void LoadMainMenu()
    {
        AudioManager.PlayUIButtonAudio();
        //animator.ResetTrigger("LoadLevel");
        //SceneManager.LoadScene("MainMenu");
        //ResumeGameSession();
        StartCoroutine(LoadLevel(0));    
    }

    public void PlayAgain()
    {
        AudioManager.PlayUIButtonAudio();
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        //ResumeGameSession();
        StartCoroutine(LoadLevel(levelIndex));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     
    }

    public void StopGameSession()
    {
        AudioManager.PlayUIButtonAudio();
        AudioManager.StopAllTankSounds();
        Time.timeScale = 0f;
    }

    public void ResumeGameSession()
    {
        AudioManager.PlayUIButtonAudio();
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        AudioManager.PlayUIButtonAudio();
        Application.Quit();
    }

    public void LoadSpecificLevel(GameObject button)
    {
        AudioManager.PlayUIButtonAudio();
        int levelIndex = Convert.ToInt32(button.GetComponentInChildren<TextMeshProUGUI>().text);
        StartCoroutine(LoadLevel(levelIndex));
    }
}
