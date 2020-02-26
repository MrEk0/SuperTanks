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
            StartCoroutine(LoadLevel(0));         
        }
    }

    IEnumerator LoadLevel(int nextSceneIndex)
    {
        GameManager.instance.PauseGame();

        animator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(nextSceneIndex);

        if(nextSceneIndex!=0)//!!!!
        AudioManager.PlayReadyGoAudio();
    }

    public void LoadMainMenu()
    {
        AudioManager.PlayUIButtonAudio();
        StartCoroutine(LoadLevel(0));    
    }

    public void PlayAgain()
    {
        AudioManager.PlayUIButtonAudio();
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(levelIndex));    
    }

    public void PlayPause()
    {
        AudioManager.PlayUIButtonAudio();
        GameManager.instance.PauseGame();
    }

    public void ResumeGame()
    {
        AudioManager.PlayUIButtonAudio();
        GameManager.instance.ResumeGame();
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
