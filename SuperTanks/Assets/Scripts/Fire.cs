using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireSpeed = 1f;

    float timeSinceLastShot = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
