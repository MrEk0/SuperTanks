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
            StartCoroutine(LoadNextLevel(nextSceneIndex));
            
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            StopPause();
        }
    }

    IEnumerator LoadNextLevel(int nextSceneIndex)
    {
        animator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(nextSceneIndex);
        StopPause();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        StopPause();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StopPause();
    }

    public void PlayPause()
    {
        Time.timeScale = 0f;
    }

    public void StopPause()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
