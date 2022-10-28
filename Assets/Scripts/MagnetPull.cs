using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPull : MonoBehaviour
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

    private void calculateForceVec()
    {
        zRot = transform.parent.transform.rotation.eulerAngles.z;

        // We can just rotate by 180 degrees to get a pull instead of a push
        float invertedZRot = zRot > 0 ? zRot - 180 : zRot + 180;

        float zRotRadians = Mathf.PI * invertedZRot / 180.0f;
        forceVec = new Vector3(Mathf.Cos(zRotRadians), Mathf.Sin(zRotRadians), 0f);
        forceVec *= magnetMagnitude;

        Debug.Log(invertedZRot);
        Debug.Log(forceVec);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(forceVec);
    }

    private Vector3 forceVec;
    private float zRot;
    private const float magnetMagnitude = 20f;
}
