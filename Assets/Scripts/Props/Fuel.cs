﻿using SuperTanks.Core;
using SuperTanks.Tanks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Props
{
    public class Fuel : MonoBehaviour
    {
        [SerializeField] float fuelAdditive = 5f;

        public GameSuporter gameSuporter { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Movement>() != null)
            {
                AudioManager.PlayPickUpAudio();

                collision.GetComponent<Movement>().Refill(fuelAdditive);
                gameSuporter.RemoveItem(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
