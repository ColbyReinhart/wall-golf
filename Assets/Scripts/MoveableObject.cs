using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public GameObject player;
    public Material highlightMat;
    public GameObject mouseController;

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
        highlight.transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z + 1);

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
            transform.position += mouseController.GetComponent<MouseController>().getMouseDelta();
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

    private bool isSelected;
    private GameObject highlight;
    private const float highlightFactor = 1.05f;
}
