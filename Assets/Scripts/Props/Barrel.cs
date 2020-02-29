using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float damageRadius = 2f;

    bool isCreated=false;

    Collider2D thisCollider;

    private void Awake()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bullet>())
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CreateDamageWave();
        Destroy(gameObject);
    }

    private void CreateDamageWave()
    {
        if (isCreated)
            return;

        isCreated = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == thisCollider)
                continue;

            if (colliders[i].GetComponent<EnemyHealth>())
            {
                colliders[i].GetComponent<EnemyHealth>().TakeDamage();
            }
            else if (colliders[i].GetComponent<Barrel>())
            {
                colliders[i].GetComponent<Barrel>().Explode();
            }
        }


    }
}
