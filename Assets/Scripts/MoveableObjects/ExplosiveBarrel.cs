using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MoveableObject
{
    public float explosionRadius = 10.0f;    // How large is the explosion? (physics, not visually)
    public float explosionMagnitude = 1500.0f;  // How much force will the explosion give? (fades with distance)
    public float explosionVelocity = 8.0f;   // How fast must a collision be before the barrel explodes?
    public float explosionTime = 1.0f;
    private Rigidbody ballRb;
    public GameObject texture;
    public GameObject explosion;

    private bool isExploded = false;
    private bool playMode = false;
    private Rigidbody rb;

    private void Awake()
    {
        // Initialize variables
        highlightFactor = 1.05f;
        rb = GetComponent<Rigidbody>();
        ballRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        // Set play mode
        SetPlayMode(false);
    }

    private void Update()
    {
        if (isExploded)
        {
            // Count down explosion time
            explosionTime -= Time.deltaTime;

            // If time runs out, destroy the barrel
            if (explosionTime >= 0.0f)
            {
                Destroy(this);
            }
        }

        // This is the best way to disable the rigidbody while in build mode
        if (!playMode)
        {
            rb.Sleep();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > explosionVelocity && playMode)
        {
            // Do the explosion
            ballRb.AddExplosionForce(explosionMagnitude, transform.position, explosionRadius);
            isExploded = true;
            /// TODO: Add explosion sound

            // Destroy the barrel texture, rigidbody, and collider and enable the explosion image
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(texture);
            explosion.SetActive(true);
        }
    }

    public override void SetPlayMode(bool play)
    {
        playMode = play;

        if (playMode)
        {
            rb.WakeUp();
        }
    }
}
