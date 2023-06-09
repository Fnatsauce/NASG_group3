using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioClip shootSound;
    private AudioSource audioSource;

    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> footsteps;

    [SerializeField] float timeBetweenSteps = 0.5f;
    private float timeBetweenStepsCounter;

    //private int lastIndex = 0;
    

    private void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
            // Use real gun assets
        } else
        {
            var bullet = Instantiate(waterBulletPrefab, transform.position + (transform.up), transform.rotation) as GameObject;

            audioSource.clip = shootSound;
            audioSource.Play();

            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;

            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
