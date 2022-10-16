using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prevMousePos = mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3 getMousePos()
    {
        return mousePos;
    }

    public Vector3 getMouseDelta()
    {
        return mousePos - prevMousePos;
    }

    private Vector3 mousePos;
    private Vector3 prevMousePos;
}
