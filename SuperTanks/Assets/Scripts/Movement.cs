using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float speedSensity = 0.15f;
    [SerializeField] float amountOfFuel = 10f;
    [SerializeField] Slider fuelSlider;
    //[SerializeField] GameObject tracePrefab;
    //[SerializeField] Transform traceStartPos;
    //[SerializeField] float traceLivingTime = 0.5f;
    //[SerializeField] float traceDropFrequency = 1f;

    Rigidbody2D rb;

    float speedX;
    float speedY;
    Vector2 moveDirection;
    Transform myTransform;
    float angle;
    bool isMoving = false;
    //float timeSinceLastTraceDrop = Mathf.Infinity;
    float pressedVerTime = 0f;
    float pressedHorTime = 0f;
    float currentFuelAmount;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();

        currentFuelAmount = amountOfFuel;
    }
    void Update()
    {
        float verticalPos = Input.GetAxisRaw("Vertical");
        float horizontalPos = Input.GetAxisRaw("Horizontal");

        if (verticalPos != 0 || horizontalPos != 0)
        {
            VerticalMovement(verticalPos, horizontalPos);
            HorizontalMovement(verticalPos, horizontalPos);
            FuelConsumption();
        }
        else
        {
            speedX = 0f;
            speedY = 0f;
        }
        //LeaveTrace();
    }

    private void HorizontalMovement(float verticalPos, float horizontalPos)
    {
        if (horizontalPos != 0 && verticalPos == 0)
        {
            pressedHorTime += Time.deltaTime;

            if (speedSensity < pressedHorTime)
            {
                speedX = horizontalPos * speed;
                speedY = 0f;

            }

            angle = horizontalPos > 0 ? -90f : 90f;
            myTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            pressedHorTime = 0f;
            speedX = 0f;
            myTransform.position = new Vector3(Mathf.RoundToInt(myTransform.position.x), myTransform.position.y);
        }
    }

    private void VerticalMovement(float verticalPos, float horizontalPos)
    {
        if (verticalPos != 0 && horizontalPos == 0)
        {
            pressedVerTime += Time.deltaTime;

            if (speedSensity < pressedVerTime)
            {
                speedY = verticalPos * speed;
            }

            angle = verticalPos > 0 ? 0f : 180f;
            myTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            pressedVerTime = 0f;
            speedY = 0f;
            myTransform.position = new Vector3(myTransform.position.x, Mathf.RoundToInt(myTransform.position.y));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);
    }

    private void FuelConsumption()
    {
        if (Mathf.Max(currentFuelAmount, 0f) != 0f) 
        {
            currentFuelAmount -= Time.deltaTime;
            fuelSlider.value = currentFuelAmount / amountOfFuel;
        }
        else
        {
            Debug.Log("Run out of Fuel");
        }
    }

    public void Refill(float fuelAmount)
    {
        currentFuelAmount = Mathf.Min(amountOfFuel, currentFuelAmount+fuelAmount);
        fuelSlider.value = currentFuelAmount / amountOfFuel;
    }

    //private void LeaveTrace()
    //{
    //    if(speedX==0 && speedY==0)
    //    {
    //        isMoving = false;
    //        timeSinceLastTraceDrop = Mathf.Infinity;
    //    }
    //    else
    //    {
    //        isMoving = true;
    //    }

    //    if (isMoving )
    //    {
    //        if (timeSinceLastTraceDrop > traceDropFrequency) 
    //        {
    //            GameObject trace = Instantiate(tracePrefab, myTransform.position, Quaternion.Euler(0, 0, angle));
    //            Destroy(trace, traceLivingTime);
    //            timeSinceLastTraceDrop = 0f;
    //        }

    //        timeSinceLastTraceDrop += Time.deltaTime;
            
    //    }
    //}
}
