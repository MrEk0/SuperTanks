using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using SuperTanks.Core;

namespace SuperTanks.Tanks
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float fireSpeed = 1f;
        [SerializeField] float ammoNumber = 10f;
        [SerializeField] TextMeshProUGUI ammoText;

        float timeSinceLastShot = Mathf.Infinity;
        float currentAmmo;

        private void Awake()
        {
            currentAmmo = ammoNumber;
        }

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        public void Shot()//for ui button
        {
            if (timeSinceLastShot > fireSpeed && currentAmmo > 0)
            {
                AudioManager.PlayPlayerFireAudio();

                Instantiate(bulletPrefab, transform.position, transform.rotation);
                DecreaseAmmo();
                timeSinceLastShot = 0f;
            }
        }

        private void DecreaseAmmo()
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                ammoText.text = currentAmmo.ToString();
            }
        }

        public void ObtainAmmo(float amount)
        {
            currentAmmo = Math.Min(currentAmmo + amount, ammoNumber);
            ammoText.text = currentAmmo.ToString();
        }
    }
}
