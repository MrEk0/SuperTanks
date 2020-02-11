using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float speedRotation = 10f;
    [SerializeField] float numberOfAdditive = 5f;

    public GameSuporter gameSuporter { get; set; }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 36*speedRotation*Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Fire>()!=null)
        {
            collision.GetComponent<Fire>().ObtainAmmo(numberOfAdditive);
            gameSuporter.RemoveItem(gameObject);
            Destroy(gameObject);
        }
    }
}
