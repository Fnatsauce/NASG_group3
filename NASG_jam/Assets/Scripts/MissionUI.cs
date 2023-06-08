using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    private TMP_Text m_TextComponent;

    private int amountOfRemainingDryFriends = 10;

    private void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();

        m_TextComponent.text = amountOfRemainingDryFriends.ToString();
    }

    public void DecreaseAmountOfDryFriendsAndUpdateMissionText()
    {
        amountOfRemainingDryFriends -= 1;

        if (amountOfRemainingDryFriends <= 0)
        {
            // Initiate level switch!

            // Temp fix before level transition:
            amountOfRemainingDryFriends = 0;
        }

        m_TextComponent.text = amountOfRemainingDryFriends.ToString();
    }
}
