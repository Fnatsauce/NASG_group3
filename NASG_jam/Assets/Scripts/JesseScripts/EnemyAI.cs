using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chasingRadius;
    public float rotationSpeed;
    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(CheckingRadius());
    }

    IEnumerator CheckingRadius()
    {
        while (true)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) <= chasingRadius)
            {
                Vector2 chasingPos = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                enemyRigidbody.MovePosition(chasingPos);


                //EnemeyRotatingTowardsPlayer
                Vector2 direction = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }


}
