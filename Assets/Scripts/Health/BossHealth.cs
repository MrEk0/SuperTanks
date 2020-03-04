using SuperTanks.Core;
using SuperTanks.Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Tanks
{
    public class BossHealth : MonoBehaviour, IDamage
    {
        [SerializeField] float healthPoint = 10f;
        [SerializeField] float tintFadeSpeed = 5f;
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] MovementAI movementAI;

        Material material;
        Color attackColor;

        const string AttackColorName = "_TintAttack";

        public void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
            attackColor = material.GetColor(AttackColorName);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                TakeDamage();
                movementAI.ChangeTargetOnCollison(collision);
            }
        }

        public void TakeDamage()
        {
            attackColor.a = 1f;
            material.SetColor(AttackColorName, attackColor);
            StartCoroutine(FadeTint());

            healthPoint = Mathf.Max(0, healthPoint - 1);

            if (healthPoint == 0f)
            {
                Death();
            }
        }

        private void Death()
        {
            AudioManager.PlayEnemyExplosionAudio();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GetComponentInParent<EnemyManager>().ShowWinPanel();
            Destroy(gameObject);
        }

        IEnumerator FadeTint()
        {
            while (attackColor.a > 0)
            {
                attackColor.a = Mathf.Clamp01(attackColor.a - tintFadeSpeed * Time.deltaTime);
                material.SetColor(AttackColorName, attackColor);
                yield return null;
            }
        }
    }
}
