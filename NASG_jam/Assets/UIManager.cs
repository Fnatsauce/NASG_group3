using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject waterLevelIndicator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWaterValueInUI()
    {
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().UpdateWaterValue();
    }

    private void FixedUpdate()
    {
        
    }
}
