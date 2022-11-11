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

    private void Awake()
    {
        highlightFactor = 1.05f;
        SetPlayMode(false);
        ballRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        Debug.Assert(ballRb != null);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity);///
        if (collision.relativeVelocity.magnitude > explosionVelocity)
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
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = play;
        rb.velocity = Vector3.zero;
    }
}
