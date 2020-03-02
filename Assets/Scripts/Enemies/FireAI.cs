using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SuperTanks.Core;

namespace SuperTanks.Tanks
{
    public class FireAI : MonoBehaviour
    {
        [SerializeField] float fireRate = 1f;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] LayerMask playerMask;
        [SerializeField] LayerMask foregroundMask;

        float timeSinceLastShot = Mathf.Infinity;
        Transform thisTransform;

        public bool canShoot { set; private get; } = true;

        public event Action<Vector2> onHitPlayer;

        private void Awake()
        {
            thisTransform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (GameManager.IsGamePause)
                return;

            RayToPlayer();

            timeSinceLastShot += Time.deltaTime;
        }

        private void RayToPlayer()
        {
            if (canShoot)
            {
                RaycastHit2D hit = Physics2D.Raycast(thisTransform.position, thisTransform.up, Mathf.Infinity, playerMask);
                RaycastHit2D foreHit = Physics2D.Raycast(thisTransform.position, thisTransform.up, Mathf.Infinity, foregroundMask);

                if (hit && hit.distance < foreHit.distance)
                {
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
                AudioManager.PlayEnemyFireAudio();
                Instantiate(bulletPrefab, thisTransform.position, thisTransform.rotation);
                timeSinceLastShot = 0f;
            }
        }
    }
}
