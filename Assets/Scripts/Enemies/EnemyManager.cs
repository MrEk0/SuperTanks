﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using SuperTanks.Core;

namespace SuperTanks.Tanks
{
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
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }

            if (enemies.Count == 0 && bossPrefab != null)
            {
                StartCoroutine(InitBoss(enemy.transform.position));
            }
            else if (enemies.Count == 0)
            {
                ShowWinPanel();
            }
        }

        IEnumerator ShowWinPanelCoroutine()
        {
            yield return new WaitForSeconds(timeToShowWinPanel);
            GameManager.PauseGame();
            AudioManager.PlayCongratulationsAudio();
            LevelUp();
            winPanel.SetActive(true);
        }

        private void LevelUp()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.LevelUp(sceneIndex);
        }

        IEnumerator InitBoss(Vector3 enemy)
        {
            float xPos = Mathf.RoundToInt(enemy.x);
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
}
