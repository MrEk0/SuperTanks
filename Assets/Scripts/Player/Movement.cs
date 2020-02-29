using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float amountOfFuel = 10f;
    [SerializeField] Slider fuelSlider;
    [SerializeField] UnityEvent onFuelConsumed;

    Rigidbody2D rb;

    //LayerMask dirtMask;
    //LayerMask grassMask;
    //LayerMask whiteMask;

    Transform myTransform;
    float angle;
    float currentFuelAmount;

    float verticalPos;
    float horizontalPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();

        currentFuelAmount = amountOfFuel;

        //dirtMask = LayerMask.GetMask("Dirt");//!!!!!!!!!!!!
        //grassMask = LayerMask.GetMask("Grass");
        //whiteMask = LayerMask.GetMask("Sand");
    }

    void Update()
    {
        if (GameManager.IsGamePause)
            return;

        horizontalPos = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalPos = CrossPlatformInputManager.GetAxis("Vertical");

        if (verticalPos != 0 || horizontalPos != 0)
        {
            FuelConsumption();

            if(verticalPos!=0)
            angle = verticalPos > 0 ? 0f : 180f;
            else
            angle = horizontalPos > 0 ? -90f : 90f;
        }
        else
        {
            myTransform.position = new Vector3(Mathf.RoundToInt(myTransform.position.x),
                                               Mathf.RoundToInt(myTransform.position.y));
        }

        AudioManager.PlayPlayerEngineAudio();
        SmoothRotation(angle);
    }

    private void SmoothRotation(float angle)
    {
        if (transform.rotation.z == angle)
            return;

        Quaternion currentRotation = myTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        myTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

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
            AudioManager.PlayGameOverAudio();
            onFuelConsumed.Invoke();
            
        }
    }

    public void Refill(float fuelAmount)
    {
        currentFuelAmount = Mathf.Min(amountOfFuel, currentFuelAmount+fuelAmount);
        fuelSlider.value = currentFuelAmount / amountOfFuel;
    }
}
