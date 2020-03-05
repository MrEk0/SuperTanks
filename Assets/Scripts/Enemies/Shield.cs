using SuperTanks.Core;
using SuperTanks.Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Tanks
{
    public class Shield : MonoBehaviour, IDamage
    {
        [SerializeField] float shieldHealth = 5f;
        [SerializeField] MovementAI movementAI;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                TakeDamage();
                movementAI.ChangeTargetOnCollison(collision);

                if (shieldHealth == 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void TakeDamage()
        {
            AudioManager.PlayEnemyHitAudio();
            shieldHealth--;
        }
    }
}
