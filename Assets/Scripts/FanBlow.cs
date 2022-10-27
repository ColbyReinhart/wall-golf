using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        calculateForceVec();
    }

    // Update is called once per frame
    void Update()
    {
        // If the rotation has changed, update the force vector
        if (transform.parent.transform.rotation.eulerAngles.z != zRot)
        {
            calculateForceVec();
        }
    }

    // Convert the rotation of the fan into a force vector
    private void calculateForceVec()
    {
        zRot = transform.parent.transform.rotation.eulerAngles.z;
        float zRotRadians = Mathf.PI * zRot / 180.0f;
        forceVec = new Vector3(Mathf.Cos(zRotRadians), Mathf.Sin(zRotRadians), 0f);
        forceVec *= fanMagnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(forceVec);
    }

    private Vector3 forceVec;
    private float zRot;
    private const float fanMagnitude = 20f;
}
