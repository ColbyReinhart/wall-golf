using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MoveableObject
{
    public float explosionRadius = 10.0f;    // How large is the explosion? (physics, not visually)
    public float explosionMagnitude = 1500.0f;  // How much force will the explosion give? (fades with distance)
    public float explosionVelocity = 8.0f;   // How fast must a collision be before the barrel explodes?
    public GameObject texture;
    public GameObject explosion;

    private Rigidbody ballRb;
    private bool playMode = false;
    private Rigidbody rb;
    private Vector3 startingPositon;
    private Quaternion startingRotation;
    private bool inPlayArea = false;

    private void Awake()
    {
        // Initialize variables
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        startingPositon = transform.position;
        startingRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If we hit something bouncy, we don't want to explode the barrel
        if (collision.collider.material.bounceCombine != PhysicMaterialCombine.Maximum
            && collision.relativeVelocity.magnitude > explosionVelocity && playMode)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we're in the playable area or not
        if (other.gameObject.name == "PlayArea")
        {
            inPlayArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if we're leaving the playable area or not
        if (other.gameObject.name == "PlayArea")
        {
            inPlayArea = false;
        }
    }

    private void Explode()
    {
        // Do the explosion
        ballRb.AddExplosionForce(explosionMagnitude, transform.position, explosionRadius);

        // Spawn an explosion object
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Disable the game object
        gameObject.SetActive(false);
    }

    public override void SetPlayMode(bool play)
    {
        playMode = play;

        // Wake up the barrel if we're starting play mode
        // Also, save or reset the starting transform respectively
        if (playMode)
        {
            startingPositon = transform.position;
            startingRotation = transform.rotation;
            rb.isKinematic = !inPlayArea;
        }
        else
        {
            transform.position = startingPositon;
            transform.rotation = startingRotation;
            rb.isKinematic = true;
        }
    }
}
