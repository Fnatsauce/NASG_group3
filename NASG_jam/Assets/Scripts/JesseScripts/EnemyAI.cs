using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseRadius;
    public float rotationSpeed = 5f;

    
    public float minPauseTime = 1f;
    public float maxPauseTime = 3f;
    public float minPatrolDistance = 3f;
    public float maxPatrolDistance = 5f;
    public float minRotateAngle = -30f;
    public float maxRotateAngle = 30f;

    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;
    private bool isChasing;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        isChasing = false;
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= chaseRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing)
            {
                // Move forward for a random distance
                float patrolDistance = Random.Range(minPatrolDistance, maxPatrolDistance);
                float moveTime = patrolDistance / speed;

                while (moveTime > 0f)
                {
                    enemyRigidbody.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
                    moveTime -= Time.deltaTime;
                    yield return null;
                }

                // Pause 
                float pauseTime = Random.Range(minPauseTime, maxPauseTime);
                yield return new WaitForSeconds(pauseTime);

                // Rotate
                float rotateAngle = Random.Range(minRotateAngle, maxRotateAngle);
                Quaternion newRotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * newRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Chase player
                Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                enemyRigidbody.MovePosition(temp);

                // Rotate towards player
                Vector3 direction = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
    
}
