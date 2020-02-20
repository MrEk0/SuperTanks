﻿using System.Collections;
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

    //private void Awake()
    //{
    //    enemies = GetComponentsInChildren<EnemyHealth>().ToList();
    //}

    public void Add(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(EnemyHealth enemy)
    {
        Debug.Log(enemies.Count);
        enemies.Remove(enemy);
        Debug.Log(enemies.Count);
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
        winPanel.SetActive(true);
    }

    IEnumerator InitBoss(Vector3 enemy)
    {
        yield return new WaitForSeconds(timeToDropBoss);
        Instantiate(bossPrefab, enemy, Quaternion.identity, transform);
    }

    public void ShowWinPanel()
    {
        StartCoroutine(ShowWinPanelCoroutine());
    }
}
