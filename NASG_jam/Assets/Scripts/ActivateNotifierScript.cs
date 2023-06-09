using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNotifierScript : MonoBehaviour
{
    public void ActivateBottleSprite()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true); 
        }
    }
}
