using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float timeToLoad = 2f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButton(0))
    //    {
    //        StartCoroutine(LoadNextLevel());
    //    }
    //}
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
        }
    }

    IEnumerator LoadNextLevel(int nextSceneIndex)
    {
        animator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
