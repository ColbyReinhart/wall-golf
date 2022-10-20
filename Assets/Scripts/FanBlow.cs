using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        calculateForceVec();
        Debug.Log(transform.parent.transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.transform.localRotation.z != zRot)
        {
            calculateForceVec();
        }
    }

    private void calculateForceVec()
    {
        zRot = transform.parent.transform.rotation.z;
        forceVec = new Vector3(Mathf.Cos(zRot), Mathf.Sin(zRot), 0f);
        forceVec *= fanMagnitude;
        Debug.Log(forceVec);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(forceVec);
    }

    private Vector3 forceVec;
    private float zRot;
    private const float fanMagnitude = 20f;
}
