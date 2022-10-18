using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjController : MonoBehaviour
{
    public struct WorldBounds
    {
        public float bottomLeftX;
        public float bottomLeftY;
        public float width;
        public float height;
    }

    private ArrayList moveables;

    // Start is called before the first frame update
    void Start()
    {
        // Get world coordinate bounds using screen dimensions
        Vector3 bottomLeftBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        WorldBounds bounds;
        bounds.bottomLeftX = bottomLeftBounds.x;
        bounds.bottomLeftY = bottomLeftBounds.y;
        bounds.width = topRightBounds.x - bottomLeftBounds.x;
        bounds.height = topRightBounds.y - bottomLeftBounds.y;

        Debug.Log(bottomLeftBounds.x);
        Debug.Log(bottomLeftBounds.y);
        Debug.Log(topRightBounds.x - bottomLeftBounds.x);
        Debug.Log(topRightBounds.y - bottomLeftBounds.y);

        // Create a list of moveable object children and tell them the world bounds
        moveables = new ArrayList();
        foreach (Transform child in transform)
        {
            child.GetComponent<MoveableObject>().SetWorldBounds(bounds);
            moveables.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
