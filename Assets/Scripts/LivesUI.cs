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
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        lifeImages = GetComponentsInChildren<Image>().ToList();
        lifeNumber = lifeImages.Count;

        //lifeImages[0].color = new Color(1, 0, 0, 1f);
    }

    //private void OnEnable()
    //{
    //    playerHealth.onGetAttacked += DecreaseLifeNumber;
    //}

    //private void OnDisable()
    //{
    //    playerHealth.onGetAttacked -= DecreaseLifeNumber;
    //}

    public void DecreaseLifeNumber()
    {
        lifeNumber--;
      
        Image image = lifeImages[lifeNumber];
        image.color = new Color(1f, 1f, 1f, image.color.a / 2f);
      
    }
}

