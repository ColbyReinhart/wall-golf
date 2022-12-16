using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 0, 0);
    private Rigidbody rb;
    private Collider col;
    public AudioSource backgroundMusic;

    private void Awake()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        backgroundMusic = GetComponent<AudioSource>();
    }

    private void Start()
    {
        backgroundMusic.Play();
    }

    public void resetPosition(bool playMode)
    {
        // Reset phyics
        rb.useGravity = playMode;
        rb.velocity = Vector3.zero;
        col.enabled = playMode;
        transform.position = initialPos;
        rb.angularVelocity = Vector3.zero;
    }
}
