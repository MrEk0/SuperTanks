﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject bossPrefab;

    List<EnemyHealth> enemies = new List<EnemyHealth>();

    private void Awake()
    {
        enemies = GetComponentsInChildren<EnemyHealth>().ToList();
    }

    public void Add(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(EnemyHealth enemy)
    {
        enemies.Remove(enemy);

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
        //Transform enemyPos = enemy;
        yield return new WaitForSeconds(2f);
        winPanel.SetActive(true);
    }

    IEnumerator InitBoss(Vector3 enemy)
    {
        //Transform enemyPos = enemy;
        yield return new WaitForSeconds(2f);
        Instantiate(bossPrefab, enemy, Quaternion.identity, transform);
    }

    public void ShowWinPanel()
    {
        StartCoroutine(ShowWinPanelCoroutine());
    }
}
