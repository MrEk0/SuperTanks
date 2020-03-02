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

    [SerializeField] LayerMask grassMask;
    [SerializeField] LayerMask dirtMask;
    [SerializeField] LayerMask sandMask;

    [SerializeField] Slider fuelSlider;
    [SerializeField] UnityEvent onFuelConsumed;

    Rigidbody2D rb;
    Transform thisTransform;
    Collider2D thisCollider;

    float minSpeed = 1f;
    float startSpeed;
    float angle;
    float currentFuelAmount;

    float verticalPos;
    float horizontalPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        thisTransform = GetComponent<Transform>();
        thisCollider = GetComponent<Collider2D>();

        currentFuelAmount = amountOfFuel;
        startSpeed = speed;
    }

    void Update()
    {
        if (GameManager.IsGamePause)
            return;

        CheckGroundSpeed();
        AudioManager.PlayPlayerEngineAudio();

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
            thisTransform.position = new Vector3(Mathf.RoundToInt(thisTransform.position.x),
                                               Mathf.RoundToInt(thisTransform.position.y));
        }

        SmoothRotation(angle);
    }

    private void CheckGroundSpeed()
    {
        if(thisCollider.IsTouchingLayers(dirtMask))
        {
            speed = Mathf.Max(minSpeed, startSpeed - 1);
        }
        else if(thisCollider.IsTouchingLayers(sandMask))
        {
            speed = Mathf.Max(minSpeed, startSpeed - 2);
        }
        else if(thisCollider.IsTouchingLayers(grassMask))
        {
            speed = startSpeed;
        }
    }

    private void SmoothRotation(float angle)
    {
        if (transform.rotation.z == angle)
            return;

        Quaternion currentRotation = thisTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        thisTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
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
            GameManager.PauseGame();
            onFuelConsumed.Invoke();
            AudioManager.PlayGameOverAudio();
            //GameManager.PauseGame();
        }
    }

    public void Refill(float fuelAmount)
    {
        currentFuelAmount = Mathf.Min(amountOfFuel, currentFuelAmount+fuelAmount);
        fuelSlider.value = currentFuelAmount / amountOfFuel;
    }
}
