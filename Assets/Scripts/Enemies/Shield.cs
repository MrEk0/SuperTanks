using SuperTanks.Core;
using SuperTanks.Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Tanks
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] float shieldHealth = 5f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                AudioManager.PlayEnemyHitAudio();
                shieldHealth--;
                if (shieldHealth == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
