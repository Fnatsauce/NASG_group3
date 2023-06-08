using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized*force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.name.Contains("wall"))
        {
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Player" || collision.gameObject.name.Contains("player"))
        {
            Destroy(gameObject);
            // Maybe make the player make a sound or something?
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.name.Contains("wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.name.Contains("player"))
        {
            Destroy(gameObject);
            // Maybe make the player make a sound or something?
        }
    }
}
