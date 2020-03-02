using SuperTanks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Props
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] GameObject explosionPrefab;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            AudioManager.PlayRocketAudio();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);

        }
    }
}
