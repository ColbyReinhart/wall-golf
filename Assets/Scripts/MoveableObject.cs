using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public GameObject player;
    public Material highlightMat;
    public GameObject inputController;

    // We achieve highlighting without having to mess with shaders
    // through this hack. A highlight is just a copy of the object
    // moved slightly behind it, scaled slightly larger, and colored
    // with a different material. We make this highlight a child of
    // the actual MoveableObject so that it follows the MoveableObject.
    void Start()
    {
        // Make a new empty game object which is a child of the moveable object
        highlight = new GameObject();
        highlight.transform.SetParent(this.transform);

        // Make it the same shape as the parent, with the "highlight" material
        highlight.AddComponent<MeshRenderer>();
        highlight.AddComponent<MeshFilter>();
        highlight.GetComponent<MeshRenderer>().material = highlightMat;
        highlight.GetComponent<MeshFilter>().mesh = this.GetComponent<MeshFilter>().mesh;

        // Preserve position and rotation
        highlight.transform.position = transform.position;
        highlight.transform.rotation = transform.rotation;

        // Make it slightly larger than the parent
        Vector3 parentDimensions = this.GetComponent<Collider>().bounds.size;
        float widthRatio = (transform.localScale.x / transform.localScale.y);
        highlight.transform.localScale = new Vector3(highlightFactor, (highlightFactor - 1f) * widthRatio + 1f, 1f); ;

        // Move it behind the parent
        Vector3 oldPos = transform.position;
        highlight.transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z + 0.1f);

        // The highlight won't initially appear
        highlight.SetActive(false);
    }

    void Update()
    {

    }

    private void OnMouseDrag()
    {
        if (isSelected)
        {
            // Get the position of where the object is to be moved
            Vector3 newPos = transform.position + inputController.GetComponent<InputController>().getMouseDelta();

            // Make sure this is within legal bounds
            if (newPos.x > bounds.bottomLeftX + bounds.width) newPos.x = bounds.bottomLeftX + bounds.width;
            else if (newPos.x < bounds.bottomLeftX) newPos.x = bounds.bottomLeftX;

            if (newPos.y > bounds.bottomLeftY + bounds.height) newPos.y = bounds.bottomLeftY + bounds.height;
            else if (newPos.y < bounds.bottomLeftY) newPos.y = bounds.bottomLeftY;

            // Move the object to the new position
            transform.position = newPos;
        }
    }

    public void Select()
    {
        isSelected = true;
        highlight.SetActive(isSelected);
    }

    public void Deselect()
    {
        isSelected = false;
        highlight.SetActive(isSelected);
    }

    // This sets the world bounds "according to the moveable object", meaning that
    // the bounds are such that is it guaranteed that the object will still be in
    // frame regardless of it's orientation and/or size as long as it remains within
    // these bounds.
    public void SetWorldBounds(MoveableObjController.WorldBounds worldBounds)
    {
        bounds.bottomLeftX = worldBounds.bottomLeftX + (transform.localScale.x / 2f);
        bounds.bottomLeftY = worldBounds.bottomLeftY + (transform.localScale.y / 2f);
        bounds.width = worldBounds.width - transform.localScale.x;
        bounds.height = worldBounds.height - transform.localScale.y;
    }

    private bool isSelected;
    private GameObject highlight;
    private MoveableObjController.WorldBounds bounds;
    private const float highlightFactor = 1.05f;
}
