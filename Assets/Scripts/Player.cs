using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 0, 0);
    private Rigidbody rb;

    private void Awake()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void resetPosition()
    {
        transform.position = initialPos;
        rb.angularVelocity = Vector3.zero;
    }
}
