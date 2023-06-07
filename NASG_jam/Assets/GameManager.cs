using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManagerInstance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
