using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Shot()
    {
        if (timeSinceLastShot > fireSpeed && currentAmmo>0)
        {
            AudioManager.PlayPlayerFireAudio();

            Instantiate(bulletPrefab, transform.position, transform.rotation);
            DecreaseAmmo();
            timeSinceLastShot = 0f;
        }
    }

    private void DecreaseAmmo()
    {
        if (currentAmmo>0)
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
