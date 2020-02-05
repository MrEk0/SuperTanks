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
    bool canShoot;

    //public Vector2 rayDirection { set; private get; }

    public event Action<Transform> onHitPlayer;

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
        //if (rayDirection == null)
        //    return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, mask);
        RaycastHit2D foreHit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, foregroundMask);

        if(hit && hit.distance<foreHit.distance)
        {          
            Shoot();

            //float playerPosX = Mathf.RoundToInt(hit.transform.position.x);
            //float playerPosY = Mathf.RoundToInt(hit.transform.position.y);

            onHitPlayer(hit.transform);
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
