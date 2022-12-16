using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Laser : Hazard
{
    MeshRenderer laserTexture;
    BoxCollider laserHitbox;
    private bool OnOff = true;
    private bool beginLaser = false;
    public float laserSwitch = 3f;
    private float lst;
    //private AudioSource collisionSound;

    private void Start()
    {
        laserTexture = GetComponent<MeshRenderer>();
        laserHitbox = GetComponent<BoxCollider>();
        lst = laserSwitch;
       // collisionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Space")) { beginLaser = !beginLaser; }
        if (beginLaser == true) {
            lst -= Time.deltaTime;
            double switchTime = Math.Ceiling(lst);
            if (switchTime == 0)
            {
                lst = laserSwitch;
                OnOff = !OnOff;
                laserTexture.enabled = OnOff;
                laserHitbox.enabled = OnOff;
                //collisionSound.Play();
            }
        } else { laserTexture.enabled = true; laserHitbox.enabled = true; }
    }
}
