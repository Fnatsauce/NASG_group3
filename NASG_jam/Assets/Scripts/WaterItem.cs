using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player picks up WaterItem
        if (collision.gameObject.tag == "Player" || collision.gameObject.name.Contains("player"))
        {
            Destroy(gameObject);
            UIManager.instance.IncreaseWaterValueInUI();
        }
    }
}
