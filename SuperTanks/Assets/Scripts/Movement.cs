using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;

    float speedX;
    float speedY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalPos = Input.GetAxisRaw("Vertical");
        speedY = verticalPos * speed;

        float horizontalPos = Input.GetAxisRaw("Horizontal");
        speedX = horizontalPos * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);
    }
}
