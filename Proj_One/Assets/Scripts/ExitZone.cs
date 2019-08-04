using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    AudioSource ExitSound;
    private void Awake()
    {
        ExitSound = GetComponent<AudioSource>();
        gameObject.layer = 2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ExitSound.Play();
            GameManager.gm.LoadNextScene();
        }
    }
}
