﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireSpeed = 1f;
    //[SerializeField] float health = 3f;
    [SerializeField] float ammoNumber = 10f;
    [SerializeField] TextMeshProUGUI ammoText;

    //public event Action onGetAttacked;

    float timeSinceLastShot = Mathf.Infinity;
    float currentAmmo;

    private void Awake()
    {
        currentAmmo = ammoNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //fire
            if(timeSinceLastShot>fireSpeed)
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation);
                DecreaseAmmo();
                timeSinceLastShot = 0f;
            }           
        }

        timeSinceLastShot += Time.deltaTime;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<Bullet>() !=null)
    //    {
    //        health=Mathf.Max(0, health - 1);
    //        onGetAttacked();

    //        if(health==0f)
    //        {
    //            Debug.Log("Gameover");
    //        }
    //    }
    //}

    private void DecreaseAmmo()
    {
        if (Mathf.Max(currentAmmo, 0) != 0f)
        {
            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
        }
        else
        {
            Debug.Log("You are run out of ammo");
        }
    }

    public void ObtainAmmo(float amount)
    {
        currentAmmo += amount;
        ammoText.text = currentAmmo.ToString();
    }
}
