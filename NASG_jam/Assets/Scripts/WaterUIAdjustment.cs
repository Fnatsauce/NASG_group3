using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUIAdjustment : MonoBehaviour
{

    public float currentWaterLevel;
    public float maxWaterLevel = 500;

    public float waterGainFromPickup = 25;

    public RectTransform content;

    // Start is called before the first frame update
    void Start()
    {
        content = gameObject.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, currentWaterLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Maybe do something to disable the option of picking up water when water is full

            currentWaterLevel += waterGainFromPickup;

            if(currentWaterLevel > maxWaterLevel) { currentWaterLevel = maxWaterLevel; }

            content.sizeDelta = new Vector2(content.sizeDelta.x, currentWaterLevel);
            Invoke("UpdateLayout", 0.001f);
        }
    }

    void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
    }


}
