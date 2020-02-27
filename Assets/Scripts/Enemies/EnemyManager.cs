using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] float timeToDropBoss = 2f;
    [SerializeField] float timeToShowWinPanel = 2f;

    List<EnemyHealth> enemies = new List<EnemyHealth>();

    public void Add(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(EnemyHealth enemy)
    {
        //Debug.Log(enemies.Count);
        enemies.Remove(enemy);
        //Debug.Log(enemies.Count);
        //ShowWinPanel();
        if (enemies.Count == 0)
        {
            if (bossPrefab != null)
            {
                StartCoroutine(InitBoss(enemy.transform.position));
            }
            else
            {
                ShowWinPanel();
            }
        }
    }

    IEnumerator ShowWinPanelCoroutine()
    {      
        yield return new WaitForSeconds(timeToShowWinPanel);
        GameManager.instance.PauseGame();
        AudioManager.PlayCongratulationsAudio();
        //GameManager.instance.LevelUp();
        winPanel.SetActive(true);
    }

    IEnumerator InitBoss(Vector3 enemy)
    {
        float xPos=Mathf.RoundToInt(enemy.x);
        float yPos = Mathf.RoundToInt(enemy.y);
        Vector2 roundEnemyPosition = new Vector2(xPos, yPos);

        yield return new WaitForSeconds(timeToDropBoss);
        Instantiate(bossPrefab, roundEnemyPosition, Quaternion.identity, transform);
    }

    public void ShowWinPanel()
    {
        StartCoroutine(ShowWinPanelCoroutine());
    }
}
