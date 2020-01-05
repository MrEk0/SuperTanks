using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] List<GameObject> WayPoints;
    [SerializeField] float viewRadius = 5f;
    [SerializeField] float rayDistance = 10f;
    [SerializeField] float offset=0.5f;

    Rigidbody2D rb;
    Vector2 targetPos;
    Vector2 previousTarget;
    Vector2 currentPoint;
    LayerMask mask;
    Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    Vector3[] offsets;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("WayPoint");
        offsets = new Vector3[4] { new Vector3(offset, 0f), new Vector3(-offset, 0f), new Vector3(0f, offset), new Vector3(0f, -offset) };
    }

    private void Start()
    {
        targetPos = GetTargetPos();
        previousTarget = targetPos;
    }

    private void Update()
    {
        if (!isReachedTargetPoint())
        {
            transform.position=Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            do
            {
                targetPos = GetTargetPos();
                Debug.Log(targetPos);
            }
            while (targetPos == previousTarget);

            if(currentPoint!=null)
            {
                previousTarget = currentPoint;
            }

            currentPoint = targetPos;

            
        }
    }

    private Vector2 GetTargetPos()
    {
        Vector2 target;
        List<Vector2> targetPositions = new List<Vector2>();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position+offsets[i], directions[i] /*, rayDistance, mask*/);
            //Debug.Log(hit.collider.gameObject.layer==LayerMask.NameToLayer("WayPoint"));
            //Debug.Log(hit.collider.IsTouchingLayers(LayerMask.NameToLayer("WayPoint")));
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("WayPoint"))
            {
                targetPositions.Add(hit.transform.position);
                //Debug.Log(hit.collider.gameObject);
            }
        }

        if(targetPositions.Count!=0)
        {
            int targetPoint=Random.Range(0, targetPositions.Count);
            target = targetPositions[targetPoint];
        }
        else
        {
            target = transform.position;
        }


        return target;
    }

    private bool isReachedTargetPoint()
    {
        float distance = Vector2.Distance(transform.position, targetPos);

        return Mathf.Approximately(distance, 0f);
    }



}
