using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManagerInstance;

    public GameObject enemyPrefab; // Could be named to reflect multiple enemy types

    [SerializeField] private float enemySpawnTimer = 0.0f;
    [SerializeField] private float timeTillNextEnemySpawn = 8.0f;
    [SerializeField] private float maxEnemySpawnTime = 30.0f;
    [SerializeField] private float enemySpawnTimeAdjuster = 1.0f;
    private bool activelySpawnEnemies = true;


    private GameObject player;

    // Start is called before the first frame update
    public void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (gameManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (activelySpawnEnemies)
        {
            enemySpawnTimer += Time.deltaTime;

            if (enemySpawnTimer >= timeTillNextEnemySpawn)
            {
                Instantiate(enemyPrefab, GenerateRandomPositionOutsideCameraView(), transform.rotation);

                timeTillNextEnemySpawn += enemySpawnTimeAdjuster;

                enemySpawnTimeAdjuster += 1.0f; // This is a temp value, it could also be an adjustable variable to further control spawn rate

                enemySpawnTimer = 0; // Reset timer
            }
            if (timeTillNextEnemySpawn >= maxEnemySpawnTime)
            {
                activelySpawnEnemies = false;
            }
        }
    }

    private Vector3 GenerateRandomPositionOutsideCameraView()
    {
        var playerPosition = player.transform.position;

        var yCoordinateUpperBound = 7.0f;
        var yCoordinateLowerBound = -7.0f;
        var xCoordinateRightBound = 11.0f;
        var xCoordinateLeftBound = -11.0f;

        var randomEnemySpawnPositionIndicator = Random.value;

        if (randomEnemySpawnPositionIndicator <= 0.25f)
        {
            // Spawn above the player
            var xRange = Random.Range(xCoordinateLeftBound, xCoordinateRightBound);
            return new Vector3(playerPosition.x + xRange, playerPosition.y + yCoordinateUpperBound, 0);

        } else if (randomEnemySpawnPositionIndicator > 0.25f && randomEnemySpawnPositionIndicator <= 0.5f)
        {
            // Spawn below the player
            var xRange = Random.Range(xCoordinateLeftBound, xCoordinateRightBound);
            return new Vector3(playerPosition.x + xRange, playerPosition.y + yCoordinateLowerBound, 0);
        } else if (randomEnemySpawnPositionIndicator > 0.5f && randomEnemySpawnPositionIndicator <= 0.75f)
        {
            // Spawn to the right of the player
            var yRange = Random.Range(yCoordinateLowerBound, yCoordinateUpperBound);
            return new Vector3(playerPosition.x + xCoordinateRightBound, playerPosition.y + yRange, 0);
        }
        else
        {
            // Spawn to the left of the player
            var yRange = Random.Range(yCoordinateLowerBound, yCoordinateUpperBound);
            return new Vector3(playerPosition.x + xCoordinateLeftBound, playerPosition.y + yRange, 0);
        }
        
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
    }
}
