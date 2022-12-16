using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPos;
    private Rigidbody rb;

    private void Awake()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void resetPosition(bool playMode)
    {
        // Reset phyics
        rb.isKinematic = !playMode;
        transform.position = initialPos;
        transform.rotation = Quaternion.identity;
    }
}
