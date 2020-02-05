using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public enum Directions
{
    Right,
    Left,
    Up,
    Down
}

public class MovementAI : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float viewRadius = 1f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask enemyMask;

    float rayDistance = 1f;

    Vector2 targetPos;
    Vector2 previousTarget;
    Vector2 currentPoint;
    Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    FireAI fireAI;

    Rigidbody2D rb;
    Vector2 target;

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
        CheckEnemyCollision();

        if (IsReachedTargetPoint())
        {
            DefinePosition();
        }
        else
        {
            target = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.rotation = SmoothRotation(target);
            rb.MovePosition(target);
        }
    }

    private void DefinePosition()
    {
        currentPoint = targetPos;
        targetPos = GetTargetPos();

        previousTarget = currentPoint;

        //previousTarget = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision) //?????
    {
        if (collision.collider.GetComponent<Bullet>() != null)
        {
            Vector2 firstTouchPoint = collision.GetContact(0).point;
            Vector2 secondTouchPoint = collision.GetContact(1).point;
            Vector2 middleTouchPoint = (firstTouchPoint + secondTouchPoint) / 2;

            Vector2 rotateDir = Vector2.zero;

            if (Mathf.Approximately(firstTouchPoint.y, secondTouchPoint.y))
            {
                rotateDir = middleTouchPoint.y > transform.position.y ? (Vector2)transform.position+directions[2] : (Vector2)transform.position + directions[3];
            }
            else if (Mathf.Approximately(firstTouchPoint.x, secondTouchPoint.x))
            {
                rotateDir = middleTouchPoint.x > transform.position.x ? (Vector2)transform.position + directions[0] : (Vector2)transform.position + directions[1];
            }

            List<Vector2> targetDirections = FormTargetDirections();

            if (targetDirections.Count != 0 && rotateDir != Vector2.zero)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                for(int i=0; i<targetDirections.Count; i++)
                {
                    if(targetDirections[i]==rotateDir)
                    {
                        targetPos = rotateDir;
                    }
                }
            }
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
            else if(hit.collider.GetComponent<Movement>()!=null)
            {
                float playerPosX = Mathf.RoundToInt(hit.transform.position.x);
                float playerPosY = Mathf.RoundToInt(hit.transform.position.y);

                targetDirections.Add(new Vector2(playerPosX, playerPosY));
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

    private Quaternion SmoothRotation(Vector2 target)
    {
        if ((Vector2)transform.position == target)
            return transform.rotation;

        Quaternion currentRotation = transform.rotation;

        Vector2 dir = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle-90f);

        return Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void SetPriorDirection(Transform playerTransform)
    {
        targetPos = playerTransform.position;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, viewRadius);
    //}

    private void CheckEnemyCollision()//!!!!!
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, viewRadius, Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.GetComponent<MovementAI>() != null && hits[i].transform.position != transform.position)
            {
                targetPos = previousTarget;
                //Debug.Log("hit");
            }
        }

        //Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, enemyMask);
        //if (collider != null && collider.transform.position != transform.position)
        //{
        //    targetPos = previousTarget;
        //}

    }
}
