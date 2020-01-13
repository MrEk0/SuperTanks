using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireSpeed = 1f;
    [SerializeField] float health = 3f;

    public event Action onGetAttacked;

    float timeSinceLastShot = Mathf.Infinity;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //fire
            if(timeSinceLastShot>fireSpeed)
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation);
                timeSinceLastShot = 0f;
            }           
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            health=Mathf.Max(0, health - 1);
            onGetAttacked();

            if(health==0f)
            {
                Debug.Log("Gameover");
            }
        }
    }
}
