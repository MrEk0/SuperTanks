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
    LayerMask foregroundMask;

    public bool canShoot { set; private get; } = true;

    public event Action<Vector2> onHitPlayer;

    private void Awake()
    {
        mask = LayerMask.GetMask("Player");
        foregroundMask = LayerMask.GetMask("Foreground");
    }

    private void Update()
    {
        RayToPlayer();

        timeSinceLastShot += Time.deltaTime;
    }

    private void RayToPlayer()
    {
        //Debug.Log(canShoot);
        if (canShoot)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, mask);
            RaycastHit2D foreHit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, foregroundMask);

            if (hit && hit.distance < foreHit.distance)
            {
                Debug.Log(hit.collider);
                Shoot();

                float playerPosX = Mathf.RoundToInt(hit.transform.position.x);
                float playerPosY = Mathf.RoundToInt(hit.transform.position.y);

                onHitPlayer(new Vector2(playerPosX, playerPosY));
            }
        }
    }

    private void Shoot()
    {
        if (timeSinceLastShot > fireRate)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            timeSinceLastShot = 0f;
        }       
    }
}
