using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Laser : MonoBehaviour
{
    MeshRenderer laserTexture;
    BoxCollider laserHitbox;
    private bool OnOff = true;
    private bool beginLaser = false;
    public float laserSwitch = 3f;
    private float lst;

    private void Start()
    {
        laserTexture = GetComponent<MeshRenderer>();
        laserHitbox = GetComponent<BoxCollider>();
        lst = laserSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Space")) { beginLaser = !beginLaser; lst = laserSwitch; OnOff = true; }
        if (beginLaser == true) {
            lst -= Time.deltaTime;
            double switchTime = Math.Ceiling(lst);
            if (switchTime == 0)
            {
                lst = laserSwitch;
                OnOff = !OnOff;
                laserTexture.enabled = OnOff;
                laserHitbox.enabled = OnOff;
            }
        } else { laserTexture.enabled = true; laserHitbox.enabled = true; }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if the player collides with the laser destroy it
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
