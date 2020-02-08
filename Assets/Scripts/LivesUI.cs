using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LivesUI : MonoBehaviour
{
    List<Image> lifeImages;
    Health playerHealth;
    int lifeNumber;
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        lifeImages = GetComponentsInChildren<Image>().ToList();
        lifeNumber = lifeImages.Count;
    }

    private void OnEnable()
    {
        playerHealth.onGetAttacked += DecreaseLifeNumber;
    }

    private void OnDisable()
    {
        playerHealth.onGetAttacked -= DecreaseLifeNumber;
    }

    private void DecreaseLifeNumber()
    {
        lifeNumber--;
        //Debug.Log(lifeImages.Count);
        //lifeImages.Remove(lifeImages[lifeImages.Count]);
        lifeImages[lifeNumber].enabled = false;
        //Debug.Log(lifeImages.Count);
    }
}

