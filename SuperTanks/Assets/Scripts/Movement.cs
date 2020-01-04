using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject tracePrefab;
    [SerializeField] Transform traceStartPos;
    [SerializeField] float traceLivingTime = 0.5f;
    [SerializeField] float traceDropFrequency = 1f;

    Rigidbody2D rb;

    float speedX;
    float speedY;
    Vector2 moveDirection;
    float angle;
    bool isMoving = false;
    float timeSinceLastTraceDrop = Mathf.Infinity;
    bool canDrop = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float verticalPos = Input.GetAxisRaw("Vertical");
        float horizontalPos = Input.GetAxisRaw("Horizontal");

        if (verticalPos != 0 && horizontalPos == 0)
        {
            speedY = verticalPos * speed;
            speedX = 0f;
            
            angle = verticalPos > 0 ? 0f : 180f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            speedY = 0f;
            transform.position = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y));
        }

        if (horizontalPos != 0 && verticalPos == 0)
        {
            speedX = horizontalPos * speed;
            speedY = 0f;

            angle = horizontalPos > 0 ? -90f : 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            speedX = 0f;
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y);
        }

        LeaveTrace();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trace"))
        {
            canDrop = false;
        }
        else
        {
            canDrop = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);
    }

    private void LeaveTrace()
    {
        if(speedX==0 && speedY==0)
        {
            isMoving = false;
            timeSinceLastTraceDrop = Mathf.Infinity;
        }
        else
        {
            isMoving = true;
        }

        Debug.Log(canDrop);
        if (isMoving && canDrop)
        {
            if (timeSinceLastTraceDrop > traceDropFrequency) 
            {
                GameObject trace = Instantiate(tracePrefab, transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(trace, traceLivingTime);
                timeSinceLastTraceDrop = 0f;
            }

            timeSinceLastTraceDrop += Time.deltaTime;
            
        }
    }




    //if (Input.GetKeyDown(KeyCode.D))
    //{           
    //    transform.rotation = Quaternion.Euler(0, 0, -90);
    //    speedX = speed;
    //    speedY = 0f;
    //}

    //if (Input.GetKeyDown(KeyCode.A))
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, 90);
    //    speedX = -speed;
    //    speedY = 0f;
    //}

    //if (Input.GetKeyDown(KeyCode.W))
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, 0);
    //    speedY = speed;
    //    speedX = 0f;
    //}
    ////else if (Input.GetKeyUp(KeyCode.W))
    ////{
    ////    transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.x));
    ////}

    //if (Input.GetKeyDown(KeyCode.S))
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, 180);
    //    speedY = -speed;
    //    speedX = 0f;
    //}
}
