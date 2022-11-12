using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LaserController : MonoBehaviour
{
    public MeshRenderer laser;
    bool OnOff = true;
    private float laserSwitch = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        laserSwitch -= Time.deltaTime;
        double switchTime = Math.Ceiling(laserSwitch);
        if (switchTime == 0)
        {
            laserSwitch = 5;
            OnOff = !OnOff;
            laser.gameObject.SetActive(OnOff);
        }
    }
}
