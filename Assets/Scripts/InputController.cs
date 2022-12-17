using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Component references
    private MoveableObjController moveableObjController;
    private PanelController panelController;

    // Rotation
    public float rotationInterval = 5f;         // Objects rotation this many degrees at a time
    public float holdToRotateDelay = 0.5f;      // How many seconds until hold rotation kicks in?
    public float timeBetweenRotations = 0.05f;  // How many seconds per rotation when holding?
    private float lastRotationTime = 0;         // When did the last rotation happen?
    private float rightArrowHoldDuration = 0;   // How long has the right arrow key been held?
    private float leftArrowHoldDuration = 0;    // How long has the left arrow key been held?

    // Object selection
    private Vector3 mousePos;
    private Vector3 prevMousePos;
    private MoveableObject pointedObj;
    private MoveableObject selectedObj;

    // Misc
    private bool playMode = false;
    private bool paused = false;

    void Start()
    {
        // Initialize references
        pointedObj = null;
        selectedObj = null;

        moveableObjController = GameObject.Find("MoveableObjController").GetComponent<MoveableObjController>();
        panelController = GameObject.Find("MenuCanvas").GetComponent<PanelController>();
    }

    void Update()
    {
        // Don't allow user input if a menu is active
        if (panelController.IsMenuActive()) return;

        // If the user hits the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Update mouse data
        prevMousePos = mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float scrollDelta = Input.mouseScrollDelta.y;

        //
        // Object selection
        //

        if (Input.GetMouseButtonDown(0) && !playMode)
        {
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

            if (!playMode)
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
        }
        
        //
        // Rotation
        //

        // We only care about this if a moveable object is selected
        if (selectedObj != null)
        {
            Quaternion rot = selectedObj.gameObject.transform.rotation;
            bool leftArrowPressed = false;
            bool rightArrowPressed = false;

            // If the user is using the scroll wheel, prioritize that
            if (scrollDelta != 0)
            {
                selectedObj.gameObject.transform.Rotate(rot.x, rot.y, rot.z + Mathf.Round(scrollDelta * rotationInterval));
            }

            // Otherwise, listen for arrow key inputs
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                leftArrowPressed = true;

                // If we just started holding down this key, move it once
                if (leftArrowHoldDuration == 0)
                {
                    selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z + rotationInterval));
                }

                // Increment time held
                leftArrowHoldDuration += Time.deltaTime;

                // If we've held it long enough, start rotataing it 
                if (leftArrowHoldDuration >= holdToRotateDelay)
                {
                    if (Time.realtimeSinceStartup >= lastRotationTime + timeBetweenRotations)
                    {
                        selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z + rotationInterval));
                        lastRotationTime = Time.realtimeSinceStartup;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rightArrowPressed = true;

                // If we just started holding down this key, move it once
                if (rightArrowHoldDuration == 0)
                {
                    selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z - rotationInterval));
                }

                // Increment time held
                rightArrowHoldDuration += Time.deltaTime;

                // If we've held it long enough, start rotataing it 
                if (rightArrowHoldDuration >= holdToRotateDelay)
                {
                    if (Time.realtimeSinceStartup >= lastRotationTime + timeBetweenRotations)
                    {
                        selectedObj.gameObject.transform.Rotate(rot.x, rot.y, Mathf.Round(rot.z - rotationInterval));
                        lastRotationTime = Time.realtimeSinceStartup;
                    }
                }
            }

            if (!leftArrowPressed) leftArrowHoldDuration = 0;
            if (!rightArrowPressed) rightArrowHoldDuration = 0;
        }

        // If the user hits the spacebar, tell the object controller to switch modes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleMode();
        }
    }

    // Helper method to get the nearest multiple of rotationInterval
    private float GetNearestIntervalRot(float rot)
    {
        return Mathf.Round(rot / rotationInterval) * rotationInterval;
    }

    public Vector3 getMouseDelta()
    {
        return mousePos - prevMousePos;
    }

    // Toggle between build and play mode
    public void ToggleMode()
    {
        playMode = !playMode;
        moveableObjController.SetPlayMode(playMode);

        // Deselect the currently selected object
        if (selectedObj != null)
        {
            selectedObj.Deselect();
            selectedObj = null;
        }

        // Make sure the game over menu goes away
        panelController.ToggleGameOverPanel(false);
    }

    // Toggle the pause menu
    public void TogglePause()
    {
        paused = !paused;

        // Toggle pause menu
        panelController.TogglePausePanel(paused);

        // If we paused while the game is in play mode,
        // switch back to build mode
        if (paused && playMode)
        {
            ToggleMode();
        }
    }

    public bool GetPlayMode()
    {
        return playMode;
    }
}
