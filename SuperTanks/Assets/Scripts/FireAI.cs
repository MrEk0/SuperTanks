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
        RayToPlayer();

        timeSinceLastShot += Time.deltaTime;
    }

    private void RayToPlayer()
    {
        if (rayDirection == null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, mask);

        if(hit/*.collider.gameObject.layer == LayerMask.NameToLayer("Player")*/)//goes through trees!!!!
        {
            //canShoot = true;
            Shoot();

            float playerPosX = Mathf.RoundToInt(hit.transform.position.x);
            float playerPosY = Mathf.RoundToInt(hit.transform.position.y);

            onHitPlayer(new Vector2(playerPosX, playerPosY));
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
