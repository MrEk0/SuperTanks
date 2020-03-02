using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float viewRadius = 1f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] BoxCollider2D bodyCollider;

    float rayDistance = 1f;

    Vector2 targetPos;
    Vector2 previousTarget;
    Vector2 targetBeforePrevious;
    Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    FireAI fireAI;
    Rigidbody2D rb;

    private void Awake()
    {
        fireAI = GetComponent<FireAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        fireAI.onHitPlayer += SetPriorDirection;
    }

    private void OnDisable()
    {
        fireAI.onHitPlayer -= SetPriorDirection;
    }

    private void Start()
    {
        targetPos = GetTargetPos();
    }

    private void Update()
    {
        if (GameManager.IsGamePause)
            return;

        AudioManager.PlayEnemyEngineAudio();
        CheckEnemyCollision();

        if (IsReachedTargetPoint())
        {
            DefinePosition();
        }
        else
        {
            Vector2 nextTarget = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            //SmoothRotation();
            rb.MovePosition(nextTarget);
        }
        SmoothRotation();
    }

    private void DefinePosition()
    {
        targetPos = GetTargetPos();

        targetBeforePrevious = previousTarget;
        previousTarget = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Bullet>() != null)
        {
            Vector2 newTarget = FormRoundVector(collision.transform.position);

            targetPos = newTarget;
            previousTarget = FormRoundVector(transform.position);
        }
    }

    private List<Vector2> FormTargetDirections()
    {
        List<Vector2> targetDirections = new List<Vector2>();
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], rayDistance, wallMask);
            
            if(hit.collider==null)
            {
                targetDirections.Add((Vector2)transform.position+directions[i]);
            }
        }

        return targetDirections;
    }

    private Vector2 GetTargetPos()
    {
        Vector2 target;

        List<Vector2> targetDirections = FormTargetDirections();
        targetDirections.Remove(previousTarget);

        if (targetDirections.Count != 0)
        {
            int targetPoint = UnityEngine.Random.Range(0, targetDirections.Count);
            target = targetDirections[targetPoint];
        }
        else
        {
            target = previousTarget;
        }

        return target;
    }

    private bool IsReachedTargetPoint()
    {
        float distance = Vector2.Distance(transform.position, targetPos);

        return Mathf.Approximately(distance, 0f);
    }

    private void SmoothRotation()
    {
        //if ((Vector2)transform.position == targetPos)
        //    return;

        Quaternion currentRotation = transform.rotation;

        Vector2 dir = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        //if (!Mathf.Approximately(Quaternion.Angle(currentRotation, targetRotation), 0f))//find out!!!
        if (Quaternion.Angle(currentRotation, targetRotation) != 0f)
        {
            fireAI.canShoot = false;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            fireAI.canShoot = true;
        }
    }

    private void SetPriorDirection(Vector2 playerPos)
    {
        targetPos = playerPos;
    }

    private void CheckEnemyCollision()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, viewRadius, enemyMask);

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            if (enemyColliders[i] != bodyCollider)
            {
                targetPos = targetBeforePrevious;
            }
        }
    }

    private Vector2 FormRoundVector(Vector3 position)
    {
        float xPos = Mathf.RoundToInt(position.x);
        float yPos = Mathf.RoundToInt(position.y);

        return new Vector2(xPos, yPos);
    }
}
