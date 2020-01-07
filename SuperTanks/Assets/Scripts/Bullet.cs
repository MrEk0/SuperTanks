using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] float speed=10f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float timeToDestroyExplosion = 0.35f;
    [SerializeField] bool playerBullet = false;

    Rigidbody2D rb;
    string hostTag="Enemy";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        hostTag = playerBullet == true ? "Player" : "Enemy";
    }

    private void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(hostTag))
            return;

        //set up explosion
        GameObject explosion=Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, timeToDestroyExplosion);

        //if (collision.CompareTag("Enemy"))
        //{
        //    //damage
        //}

        Destroy(gameObject);
    }
}
