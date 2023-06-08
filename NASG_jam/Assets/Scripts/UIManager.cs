using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject waterLevelIndicator;
    [SerializeField] private GameObject DryFriendAmount;

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

    public void IncreaseWaterValueInUI()
    {
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
    }

    public void DecreaseWaterValueInUI()
    {
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().DecreaseWaterValue();
    }

    public void DecreaseDryFriendAmountInUI()
    {
        DryFriendAmount.GetComponent<MissionUI>().DecreaseAmountOfDryFriendsAndUpdateMissionText();
    }

    private void FixedUpdate()
    {
        
    }
}
