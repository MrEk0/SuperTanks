﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float shieldHealth = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            shieldHealth--;
            if(shieldHealth==0)
            {
                Destroy(gameObject);               
            }
        }
    }
}
