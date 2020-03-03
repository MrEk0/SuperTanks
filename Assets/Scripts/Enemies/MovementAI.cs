using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTanks.Core;

namespace SuperTanks.Tanks
{
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
        Transform thisTransform;

        private void Awake()
        {
            fireAI = GetComponent<FireAI>();
            rb = GetComponent<Rigidbody2D>();
            thisTransform = GetComponent<Transform>();

            previousTarget = thisTransform.position;
            targetBeforePrevious = thisTransform.position;
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
                MoveTowardsTarget();
            }
        }

        private void MoveTowardsTarget()
        {
            Vector2 nextTarget = Vector2.MoveTowards(thisTransform.position, targetPos, speed * Time.deltaTime);

            SmoothRotation();
            rb.MovePosition(nextTarget);
        }

        private void DefinePosition()
        {
            targetPos = GetTargetPos();

            targetBeforePrevious = previousTarget;
            previousTarget = thisTransform.position;
        }

        public void ChangeTargetOnCollison(Collider2D collision)
        {
            Vector2 newTarget = FormRoundVector(collision.transform.position);
            targetPos = newTarget;
            previousTarget = FormRoundVector(thisTransform.position);
        }

        private List<Vector2> FormTargetDirections()
        {
            List<Vector2> targetDirections = new List<Vector2>();
            for (int i = 0; i < directions.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(thisTransform.position, directions[i], rayDistance, wallMask);

                if (hit.collider == null)
                {
                    targetDirections.Add((Vector2)thisTransform.position + directions[i]);
                }
            }

            return targetDirections;
        }

        private Vector2 GetTargetPos()
        {
            Vector2 target;

            List<Vector2> targetDirections = FormTargetDirections();

            if (targetDirections.Contains(previousTarget))
            {
                targetDirections.Remove(previousTarget);
            }

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
            float distance = Vector2.Distance(thisTransform.position, targetPos);

            return Mathf.Approximately(distance, 0f);
        }

        private void SmoothRotation()
        {
            Quaternion currentRotation = thisTransform.rotation;

            Vector2 dir = targetPos - (Vector2)thisTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);

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
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(thisTransform.position, viewRadius, enemyMask);

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
}
