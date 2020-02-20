using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCount <= nextSceneIndex)
        {
            StartCoroutine(LoadLevel(nextSceneIndex));
            
        }
        else
        {
            //animator.ResetTrigger("LoadLevel");
            //SceneManager.LoadScene("MainMenu");
            ResumeGameSession();
            StartCoroutine(LoadLevel(0));         
        }
    }

    IEnumerator LoadLevel(int nextSceneIndex)
    {
        animator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(nextSceneIndex);
        ResumeGameSession();
    }

    public void LoadMainMenu()
    {
        //animator.ResetTrigger("LoadLevel");
        //SceneManager.LoadScene("MainMenu");
        ResumeGameSession();
        StartCoroutine(LoadLevel(0));    
    }

    public void PlayAgain()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        ResumeGameSession();
        StartCoroutine(LoadLevel(levelIndex));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     
    }

    public void StopGameSession()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGameSession()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
