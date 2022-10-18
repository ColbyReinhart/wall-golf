using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject moveableObjController;

    // Start is called before the first frame update
    void Start()
    {
        pointedObj = null;
        selectedObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Update mouse position
        prevMousePos = mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check what mouse is looking at
        // https://answers.unity.com/questions/1316731/mouse-click-raycast-colliders.html
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // pointedObj will be null if we hit something that's not a moveable object
            pointedObj = hit.collider.gameObject.GetComponent<MoveableObject>();
        }
        else
        {
            pointedObj = null;
        }

        if (Input.GetMouseButtonDown(0) && !playMode)
        {
            // If we're left clicking and pointing at a moveable object,
            // deselect the old one (if applicable) and select the new
            // moveable object
            if (pointedObj != selectedObj && pointedObj != null)
            {
                if (selectedObj != null)
                {
                    selectedObj.Deselect();
                }

                selectedObj = pointedObj;
                selectedObj.Select();
            }

            // If we're left clicking on nothing,
            // deselect the selected item
            else if (pointedObj == null && selectedObj != null)
            {
                selectedObj.Deselect();
                selectedObj = null;
            }
        }

        // Check for arrow key inputs
        if (selectedObj != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Quaternion rot = selectedObj.gameObject.transform.rotation;
                selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z + rotateFactor));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Quaternion rot = selectedObj.gameObject.transform.rotation;
                selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z - rotateFactor));
            }
        }

        // If the user hits the spacebar, tell the object controller to switch modes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playMode = !playMode;
            moveableObjController.GetComponent<MoveableObjController>().SetPlayMode(playMode);

            // Deselect the currently selected object
            if (selectedObj != null)
            {
                selectedObj.Deselect();
                selectedObj = null;
            }
        }
    }

    public Vector3 getMouseDelta()
    {
        return mousePos - prevMousePos;
    }

    private Vector3 mousePos;
    private Vector3 prevMousePos;
    private MoveableObject pointedObj;
    private MoveableObject selectedObj;
    private bool playMode = false;
    private const float rotateFactor = 5f;
}
