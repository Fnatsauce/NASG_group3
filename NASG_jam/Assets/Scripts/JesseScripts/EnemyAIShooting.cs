using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAIShooting : MonoBehaviour
{
    public GameObject waterItemPrefab;
    public GameObject deadEnemyPrefab;

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
    private bool isTooClose;
    private bool dead;
    SpriteRenderer m_SpriteRenderer;

    public GameObject EnemyBullet;
    public GameObject EnemyBulletReal;
    public float fireRate;
    private float nextFireTime;

    private IEnumerator enemyAICoroutine;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        isChasing = false;
        dead = false;

        nextFireTime = Time.time;

        enemyAICoroutine = PatrolRoutine();
        StartCoroutine(enemyAICoroutine);
    }

    void Update()
    {
        //check the chaseRadius between player and enemy
        if (Vector3.Distance(playerTransform.position, transform.position) <= chaseRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.transform.position);
        if (distance < 0.5f) { isTooClose = true; }
        else { isTooClose = false; }


        //Check Enemy Fire Again
        if (isChasing && !isTooClose && !dead)
        {
            nextFireTime += Time.deltaTime;
            if (nextFireTime > 2)
            {
                nextFireTime = 0;
                ShootPlayer();
            }
        }
    }

    void ShootPlayer()
    {
        // Temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // This is just to ensure only the RobotDeath scene is affected by this code. Other scenes should not have monsters triggering pressure plates
        if (sceneName == "Level02_ApoNew")
        {
            GameObject bullet = Instantiate(EnemyBulletReal, transform.position, Quaternion.identity);
        }
        else
        {
            GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
        }

    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing)
            {
                // Move forward
                float patrolDistance = Random.Range(minPatrolDistance, maxPatrolDistance);
                float moveTime = patrolDistance / speed;

                while (moveTime > 0f)
                {
                    enemyRigidbody.MovePosition(
                        transform.position + transform.right * speed * Time.deltaTime
                    );
                    moveTime -= Time.deltaTime;
                    yield return null;
                }

                // Pause
                float pauseTime = Random.Range(minPauseTime, maxPauseTime);
                yield return new WaitForSeconds(pauseTime);

                // Rotate
                float rotateAngle = Random.Range(minRotateAngle, maxRotateAngle);
                Quaternion newRotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    transform.rotation * newRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
            else
            {
                // Chase player
                Vector3 temp = Vector3.MoveTowards(
                    transform.position,
                    playerTransform.position,
                    speed * Time.deltaTime
                );
                enemyRigidbody.MovePosition(temp);

                // Rotate towards player
                Vector3 direction = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (!isTooClose)
                {
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        rotation,
                        rotationSpeed * Time.deltaTime
                    );
                }
                yield return null;
            }
        }
    }

    public void EnemyDies()
    {
        // Lock position of enemy


        // Disable collision, if not already something like a trigger


        // Temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // This is just to ensure only the RobotDeath scene is affected by this code. Other scenes should not have monsters triggering pressure plates
        if (sceneName == "Level01")
        {
            //Fetch the SpriteRenderer from the GameObject
            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            //Set the GameObject's Color quickly to a set Color (blue)
            m_SpriteRenderer.color = Color.blue;
            dead = true;
            StopCoroutine(enemyAICoroutine);
        } else
        {
            // Switch to death sprite
            dead = true;
            StopCoroutine(enemyAICoroutine);
            Instantiate(deadEnemyPrefab, transform.position, new Quaternion());
            Instantiate(waterItemPrefab, transform.position + transform.up, new Quaternion());
            Destroy(gameObject);
            
        }
        
    }
}
