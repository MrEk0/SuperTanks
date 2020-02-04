using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] float fuelAdditive = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Movement>()!=null)
        {
            collision.GetComponent<Movement>().Refill(fuelAdditive);
            Destroy(gameObject);
        }
    }
}
