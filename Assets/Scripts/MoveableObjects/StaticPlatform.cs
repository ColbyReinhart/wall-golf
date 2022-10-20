using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MoveableObject
{
    private AudioSource collisionSound;
    private const float minVolumeSpeed = 3f;
    private const float maxVolumeSpeed = 15f;

    private void Awake()
    {
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ball is going fast enough, play a sound
        if (collision.relativeVelocity.magnitude >= minVolumeSpeed)
        {
            // Volume is dependent on how fast the ball is moving
            collisionSound.volume = (collision.relativeVelocity.magnitude - minVolumeSpeed) / (maxVolumeSpeed - minVolumeSpeed);
            collisionSound.Play();
        }
    }
}
