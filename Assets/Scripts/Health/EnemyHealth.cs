using SuperTanks.Core;
using SuperTanks.Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Tanks
{
    public class EnemyHealth : Health
    {
        [SerializeField] GameObject explosionPrefab;

        EnemyManager enemyManager;
        private void Awake()
        {
            enemyManager = GetComponentInParent<EnemyManager>();
            enemyManager.Add(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                TakeDamage();
            }
        }

        public override void TakeDamage()
        {
            AudioManager.PlayEnemyExplosionAudio();

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            enemyManager.Remove(this);
            Destroy(gameObject);
        }
    }
}
