using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [SerializeField] float healthPoint = 3f;
    [SerializeField] float tintFadeSpeed = 5f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] UnityEvent receiveDamage;
    [SerializeField] UnityEvent onDead;

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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>()!=null)
        {
            TakeDamage();
        }
    }

    public override void TakeDamage()
    {
        AudioManager.PlayEnemyHitAudio();

        attackColor.a = 1f;
        material.SetColor(AttackColorName, attackColor);
        StartCoroutine(FadeTint());

        healthPoint = Mathf.Max(0, healthPoint - 1);

        if (healthPoint == 0f)
        {
            AudioManager.PlayPlayerExplosionAudio();
            Death();
        }

        receiveDamage.Invoke();
    }

    private void Death()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        onDead.Invoke();
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
