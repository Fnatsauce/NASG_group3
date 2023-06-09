using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public static UIManager instance;

    [SerializeField] private GameObject waterLevelIndicator;
    [SerializeField] private GameObject DryFriendAmount;

    private bool weAreInLevel1 = false;

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
        // Temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // This is just to ensure only the RobotDeath scene is affected by this code. Other scenes should not have monsters triggering pressure plates
        if (sceneName == "Level01")
        {
            weAreInLevel1 = true;
        }
        else
        {
            weAreInLevel1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfOutOfWater()
    {
        return waterLevelIndicator.GetComponent<WaterUIAdjustment>().CheckIfOutOfWater();
    }

    public void FillWaterValueInUI()
    {
        // I hate this code xD
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
        waterLevelIndicator.GetComponent<WaterUIAdjustment>().IncreaseWaterValue();
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
        if (weAreInLevel1)
        {
            DryFriendAmount.GetComponent<MissionUI>().DecreaseAmountOfDryFriendsAndUpdateMissionText();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
