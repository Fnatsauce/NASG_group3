using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUIAdjustment : MonoBehaviour
{

    public float currentWaterLevel = 100;
    public float maxWaterLevel = 300;

    public float waterGainFromPickup = 25;

    public RectTransform content;

    // Start is called before the first frame update
    void Start()
    {
        content = gameObject.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, currentWaterLevel);
    }

    public void IncreaseWaterValue()
    {
        // The player picks up a water item
        currentWaterLevel += waterGainFromPickup;

        if (currentWaterLevel > maxWaterLevel) { currentWaterLevel = maxWaterLevel; }

        content.sizeDelta = new Vector2(content.sizeDelta.x, currentWaterLevel);
        Invoke("UpdateLayout", 0.001f);
    }

    public void DecreaseWaterValue()
    {
        // The player takes a sip of water
        currentWaterLevel -= waterGainFromPickup;

        if (currentWaterLevel < 0) {
            GameManager.gameManagerInstance.GameOver(); // TODO: Change this
        }

        content.sizeDelta = new Vector2(content.sizeDelta.x, currentWaterLevel);
        Invoke("UpdateLayout", 0.001f);
    }

    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
    }

}
