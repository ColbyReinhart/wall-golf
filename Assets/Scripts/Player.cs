using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        initialPos = transform.position;
    }

    public void resetPosition()
    {
        transform.position = initialPos;
    }
}
