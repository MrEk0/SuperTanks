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

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] List<GameObject> WayPoints;
    [SerializeField] float viewRadius = 1f;
    //[SerializeField] float rayDistance = 10f;
    [SerializeField] float offset=0.5f;

    //Rigidbody2D rb;
    Vector2 targetPos;
    Vector2 previousTarget;
    Vector2 currentPoint;
    //LayerMask mask;
    Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    Vector3[] offsets;
    FireAI fireAI;

    Vector2 rotateDirection=Vector2.zero;

    Vector2 priorTarget;

    private void Awake()
    {
        offsets = new Vector3[4] { new Vector3(offset, 0f), new Vector3(-offset, 0f), new Vector3(0f, offset), new Vector3(0f, -offset) };
        fireAI = GetComponent<FireAI>();
        //mask = LayerMask.GetMask("Enemy");
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
        //previousTarget = targetPos;
    }

    private void Update()
    {
        CheckEnemyCollision();

        if (!IsReachedTargetPoint())
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            DefinePosition();
        }
        //Debug.Log(targetPos);
    }

    private void DefinePosition()
    {
        currentPoint = targetPos;
        targetPos = GetTargetPos();

        previousTarget = currentPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 firstTouchPoint = collision.GetContact(0).point;
            Vector2 secondTouchPoint = collision.GetContact(1).point;
            Vector2 middleTouchPoint = (firstTouchPoint + secondTouchPoint) / 2;

            Vector2 rotateDir = Vector2.zero;
            //Vector2 target=Vector2.zero;

            if (Mathf.Approximately(firstTouchPoint.y, secondTouchPoint.y))
            {
                rotateDir = middleTouchPoint.y > transform.position.y ? Vector2.up : Vector2.down;
            }
            else if (Mathf.Approximately(firstTouchPoint.x, secondTouchPoint.x))
            {
                rotateDir = middleTouchPoint.x > transform.position.x ? Vector2.right : Vector2.left;
            }

            Dictionary<Vector2, Vector2> targetDirections = FormTargetDirections();

            if (targetDirections.Count != 0 && rotateDir!=Vector2.zero)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                targetPos = targetDirections.First(x => x.Value == rotateDir).Key;
            }

            RotateTank(rotateDir);
            fireAI.rayDirection = rotateDir;
        }
    }

    private Dictionary<Vector2, Vector2> FormTargetDirections()
    {
        Dictionary<Vector2, Vector2> targetDirections = new Dictionary<Vector2, Vector2>();
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offsets[i], directions[i]);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("WayPoint"))
            {
                targetDirections.Add(hit.transform.position, directions[i]);
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                float playerPosX = Mathf.RoundToInt(hit.transform.position.x);
                float playerPosY = Mathf.RoundToInt(hit.transform.position.y);

                targetDirections.Add(new Vector2(playerPosX, playerPosY), directions[i]);
            }
        }

        return targetDirections;
    }

    private Vector2 GetTargetPos()
    {
        Vector2 target=previousTarget;
        //Dictionary<Vector2, Vector2> targetDirections = new Dictionary<Vector2, Vector2>();

        if (priorTarget != Vector2.zero)
        {          
            target = priorTarget;
        }
        else
        {
            Dictionary<Vector2, Vector2>  targetDirections = FormTargetDirections();

            targetDirections.Remove(previousTarget);

            if (targetDirections.Count != 0)
            {
                int targetPoint = UnityEngine.Random.Range(0, targetDirections.Count);
                target = targetDirections.ElementAt(targetPoint).Key;
                rotateDirection = targetDirections.ElementAt(targetPoint).Value;
                RotateTank(rotateDirection);
                fireAI.rayDirection = rotateDirection;
            }
        }    
       

        priorTarget = Vector2.zero;

        return target;
    }

    private bool IsReachedTargetPoint()
    {
        float distance = Vector2.Distance(transform.position, targetPos);

        return Mathf.Approximately(distance, 0f);
    }


    private void RotateTank(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        float angle = 0f;
        Directions dir = (Directions)Array.IndexOf(directions, direction);

        switch(dir)
        {
            case Directions.Right:
                angle = -90f;
                break;
            case Directions.Left:
                angle = 90f;
                break;
            case Directions.Up:
                angle = 0f;
                break;
            case Directions.Down:
                angle = 180f;
                break;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void SetPriorDirection(Vector2 direction)
    {
        priorTarget = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    private void CheckEnemyCollision()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, viewRadius, Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Enemy") && hits[i].transform.position != transform.position)
            {
                targetPos = previousTarget;
                //Debug.Log("hit");
            }
        }

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, rotateDirection, viewRadius, mask);
        //Debug.DrawRay(transform.position, rotateDirection, Color.red);

        //if(hit.collider.CompareTag("Enemy") && hit.transform.position != transform.position)
        //{
        //    targetPos = previousTarget;
        //    Debug.Log("Hit");
        //}
    }
}
