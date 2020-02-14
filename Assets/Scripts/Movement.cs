﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public enum Directions
{
    Up,
    Down,
    Right,
    Left,
}

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    //[SerializeField] float speedSensity = 0.15f;
    [SerializeField] float amountOfFuel = 10f;
    [SerializeField] Slider fuelSlider;

    Rigidbody2D rb;
    Collider2D myCollider;

    LayerMask dirtMask;
    LayerMask grassMask;
    LayerMask whiteMask;

    //float speedX;
    //float speedY;
    //Vector2 moveDirection;
    Transform myTransform;
    float angle;
    //bool isMoving = false;
    //float timeSinceLastTraceDrop = Mathf.Infinity;
    //float pressedVerTime = 0f;
    //float pressedHorTime = 0f;
    float currentFuelAmount;

    float verticalPos;
    float horizontalPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myCollider = GetComponent<Collider2D>();

        currentFuelAmount = amountOfFuel;

        dirtMask = LayerMask.GetMask("Dirt");
        grassMask = LayerMask.GetMask("Grass");
        whiteMask = LayerMask.GetMask("Sand");
    }
    void Update()
    {

        horizontalPos = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalPos = CrossPlatformInputManager.GetAxis("Vertical");

        if (verticalPos != 0 || horizontalPos != 0)
        {
            FuelConsumption();

            if(verticalPos!=0)
            angle = verticalPos > 0 ? 0f : 180f;
            else
            angle = horizontalPos > 0 ? -90f : 90f;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            myTransform.position = new Vector3(Mathf.RoundToInt(myTransform.position.x),
                                               Mathf.RoundToInt(myTransform.position.y));
        }
        //if(myCollider.IsTouchingLayers(dirtMask))
        //{
        //    speed = 5f;
        //}
        //else if(myCollider.IsTouchingLayers(grassMask))
        //{
        //    speed = 8f;
        //}
        //else
        //{
        //    speed = 3f;
        //}
    }

    public void SmoothRotation(Directions direction)
    {
        //Quaternion currentRotation = myTransform.rotation;

        //Vector2 dir = target - (Vector2)myTransform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        //myTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 10f);
    }

    //private void HorizontalMovement(float verticalPos, float horizontalPos)
    //{
    //    if (horizontalPos != 0 && verticalPos == 0)
    //    {
    //        pressedHorTime += Time.deltaTime;

    //        if (speedSensity < pressedHorTime)
    //        {
    //            speedX = horizontalPos * speed;
    //            speedY = 0f;

    //        }

    //        angle = horizontalPos > 0 ? -90f : 90f;
    //        myTransform.rotation = Quaternion.Euler(0, 0, angle);
    //    }
    //    else
    //    {
    //        pressedHorTime = 0f;
    //        speedX = 0f;
    //        myTransform.position = new Vector3(Mathf.RoundToInt(myTransform.position.x), myTransform.position.y);
    //    }
    //}

    //private void VerticalMovement(float verticalPos, float horizontalPos)
    //{
    //    if (verticalPos != 0 && horizontalPos == 0)
    //    {
    //        pressedVerTime += Time.deltaTime;

    //        if (speedSensity < pressedVerTime)
    //        {
    //            speedY = verticalPos * speed;
    //        }

    //        angle = verticalPos > 0 ? 0f : 180f;
    //        myTransform.rotation = Quaternion.Euler(0, 0, angle);
    //    }
    //    else
    //    {
    //        pressedVerTime = 0f;
    //        speedY = 0f;
    //        myTransform.position = new Vector3(myTransform.position.x, Mathf.RoundToInt(myTransform.position.y));
    //    }
    //}

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalPos*speed, verticalPos*speed);
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
}
