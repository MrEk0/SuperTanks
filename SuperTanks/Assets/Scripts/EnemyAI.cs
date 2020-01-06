﻿using System.Collections;
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
    [SerializeField] float viewRadius = 5f;
    [SerializeField] float rayDistance = 10f;
    [SerializeField] float offset=0.5f;

    //Rigidbody2D rb;
    Vector2 targetPos;
    Vector2 previousTarget;
    Vector2 currentPoint;
    LayerMask mask;
    Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    Vector3[] offsets;
    //Vector2 rotateDirection;

    private void Awake()
    {
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
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            do
            {
                targetPos = GetTargetPos();
            }
            while (targetPos == previousTarget);

            if (currentPoint != null)
            {
                previousTarget = currentPoint;
            }

            currentPoint = targetPos;
        }
    }

    private Vector2 GetTargetPos()
    {
        Vector2 target;
        Vector2 rotateDirection;
        Dictionary<Vector2, Vector2> targetDirections = new Dictionary<Vector2, Vector2>();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offsets[i], directions[i]);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("WayPoint"))
            {
                targetDirections.Add(hit.transform.position, directions[i]);
            }
        }

        if(targetDirections.Count !=0)
        {
            int targetPoint = UnityEngine.Random.Range(0, targetDirections.Count);
            target = targetDirections.ElementAt(targetPoint).Key;
            rotateDirection= targetDirections.ElementAt(targetPoint).Value;
        }
        else
        {
            target = transform.position;
            rotateDirection = Vector2.zero;
        }

        RotateTank(rotateDirection);

        return target;
    }

    private bool isReachedTargetPoint()
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
        Directions dir =(Directions) Array.IndexOf(directions, direction);

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
}
