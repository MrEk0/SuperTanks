using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LivesUI : MonoBehaviour
{
    List<Image> lifeImages;
    Fire playerFire;
    int lifeNumber;
    private void Awake()
    {
        playerFire = GameObject.FindGameObjectWithTag("Player").GetComponent<Fire>();
        lifeImages = GetComponentsInChildren<Image>().ToList();
        lifeNumber = lifeImages.Count;
    }

    private void OnEnable()
    {
        playerFire.onGetAttacked += DecreaseLifeNumber;
    }

    private void OnDisable()
    {
        playerFire.onGetAttacked -= DecreaseLifeNumber;
    }

    private void DecreaseLifeNumber()
    {
        lifeNumber--;
        //Debug.Log(lifeImages.Count);
        //lifeImages.Remove(lifeImages[lifeImages.Count]);
        Destroy(lifeImages[lifeNumber].gameObject);
        //Debug.Log(lifeImages.Count);
    }
}

