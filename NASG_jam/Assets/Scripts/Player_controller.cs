using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_controller : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D playerRB;
    private Vector2 movement;

    // Shooting variables:
    [SerializeField] private bool transitionToRealGunHasOccured = false;
    public GameObject waterBulletPrefab;
    public GameObject realBulletPrefab;
    [SerializeField] private float projectileSpeed = 20.0f;

    public static Player_controller instance;

    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> footsteps;

    [SerializeField] float timeBetweenSteps = 0.5f;
    private float timeBetweenStepsCounter;

    [SerializeField] private float timeTillNextDrink;
    [SerializeField] private float timeBetweenDrinks = 12.0f;

    //private int lastIndex = 0;

    private bool currentlyDrinking = false;
    private GameObject playerBottleSpriteObject;
    private GameObject playerNotifierSpriteObject;

    private void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerBottleSpriteObject = GetComponentInChildren<ActivateBottleScript>().gameObject;
        playerNotifierSpriteObject = GetComponentInChildren<ActivateNotifierScript>().gameObject;
        // This is to set the bottle off at startup
        playerBottleSpriteObject.GetComponent<ActivateBottleScript>().ActivateBottleSprite();
        playerNotifierSpriteObject.GetComponent<ActivateNotifierScript>().ActivateBottleSprite();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0)
        { 
            timeBetweenStepsCounter += Time.deltaTime;
            if (timeBetweenStepsCounter >= timeBetweenSteps)
            {
                timeBetweenStepsCounter = 0f;
                //PlayFootStep();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }

        UpdateDrinkTime();
    }

    /*
    void PlayFootStep()
    {
        //Get a random footstep from the Footsteps list
        var index = Random.Range(0, footsteps.Count);

        //To prevent the same footstep from playing twice in a row we check if the index is the same as the
        //last index and if it is we get a new index until it is different from the last index

        int count = 0;

        while (lastIndex == index)
        {
            index = Random.Range(0, footsteps.Count);
            count++;
            if (count > 10)
                break;
        }

        lastIndex = index;

        audioSource.clip = footsteps[index];
        audioSource.Play();

    }
    */

    void FixedUpdate()
    {
        RotateToMouse();
        playerRB.velocity = new Vector2(movement.x, movement.y).normalized * moveSpeed;
        
        if(!transitionToRealGunHasOccured)
        {
            // Temporary reference to the current scene.
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;

            // This is just to ensure only the RobotDeath scene is affected by this code. Other scenes should not have monsters triggering pressure plates
            if (sceneName == "Level02_ApoNew")
            {
                transitionToRealGunHasOccured = true;
            }
        }
    }

    private void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    private void ShootBullet()
    {
        if (transitionToRealGunHasOccured)
        {
            var bullet = Instantiate(realBulletPrefab, transform.position + (transform.up), transform.rotation) as GameObject;

            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
        } else
        {
            if (!UIManager.instance.CheckIfOutOfWater())
            {
                var bullet = Instantiate(waterBulletPrefab, transform.position + (transform.up), transform.rotation) as GameObject;

                bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;

                UIManager.instance.DecreaseWaterValueInUI();
            }
        }
    }

    private void UpdateDrinkTime()
    {
        timeTillNextDrink -= Time.deltaTime;

        if(timeTillNextDrink < 1 && currentlyDrinking==false && !UIManager.instance.CheckIfOutOfWater())
        {
            playerBottleSpriteObject.GetComponent<ActivateBottleScript>().ActivateBottleSprite();
            currentlyDrinking = true;
        }
        if (timeTillNextDrink < 1 && currentlyDrinking == false && UIManager.instance.CheckIfOutOfWater())
        {
            playerNotifierSpriteObject.GetComponent<ActivateNotifierScript>().ActivateBottleSprite();
            currentlyDrinking = true;
        }
        if (timeTillNextDrink <= 0 && !UIManager.instance.CheckIfOutOfWater())
        {
            timeTillNextDrink = timeBetweenDrinks;
            // Play drinking animation and/or change sprite
            UIManager.instance.DecreaseWaterValueInUI();

            playerBottleSpriteObject.GetComponent<ActivateBottleScript>().ActivateBottleSprite();
            currentlyDrinking = false;
        }
        if (timeTillNextDrink <= 0 && UIManager.instance.CheckIfOutOfWater())
        {
            timeTillNextDrink = timeBetweenDrinks;
            // Play drinking animation and/or change sprite
            UIManager.instance.DecreaseWaterValueInUI();

            playerNotifierSpriteObject.GetComponent<ActivateNotifierScript>().ActivateBottleSprite();
            currentlyDrinking = false;
        }
    }
}
