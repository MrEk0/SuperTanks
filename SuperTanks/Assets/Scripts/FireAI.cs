using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireAI : MonoBehaviour
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] GameObject bulletPrefab;

    float timeSinceLastShot = Mathf.Infinity;
    LayerMask mask;
    bool canShoot;

    public Vector2 rayDirection { set; private get; }

    public event Action<Vector2> onHitPlayer;

    private void Awake()
    {
        mask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if(timeSinceLastShot>fireRate)
        {
            RayToPlayer();
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void RayToPlayer()
    {
        if (rayDirection == null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, mask);

        if(hit)
        {
            //canShoot = true;
            Shoot();
            onHitPlayer(rayDirection);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
