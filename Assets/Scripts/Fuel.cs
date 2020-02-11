using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] float fuelAdditive = 5f;

    public GameSuporter gameSuporter { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Movement>()!=null)
        {
            collision.GetComponent<Movement>().Refill(fuelAdditive);
            gameSuporter.RemoveItem(gameObject);
            Destroy(gameObject);
        }
    }
}
