using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
