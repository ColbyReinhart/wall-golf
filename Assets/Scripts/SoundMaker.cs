using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{

    private AudioSource collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        collisionSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionSound.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collisionSound.Play();
        }
        
       
    }
}
