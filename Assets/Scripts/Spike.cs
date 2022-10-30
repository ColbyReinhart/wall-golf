using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if the player collides with the spikes destroy it
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
