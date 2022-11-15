using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 1.0f;

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
