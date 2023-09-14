using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.name.Contains("wall")) {
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Enemy" || collision.gameObject.name.Contains("Enemy"))
        {
            Destroy(gameObject);
            UIManager.instance.DecreaseDryFriendAmountInUI();
            collision.gameObject.GetComponent<EnemyAIShooting>().EnemyDies();
        }
        // More collision options below
    }
}
