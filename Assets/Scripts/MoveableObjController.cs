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

    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        // First, get the ball and tell it to sit still
        ball = GameObject.FindGameObjectWithTag("Player");
        SetPlayMode(false);

        // Get world coordinate bounds using screen dimensions
        Vector3 bottomLeftBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        WorldBounds bounds;
        bounds.bottomLeftX = bottomLeftBounds.x;
        bounds.bottomLeftY = bottomLeftBounds.y;
        bounds.width = topRightBounds.x - bottomLeftBounds.x;
        bounds.height = topRightBounds.y - bottomLeftBounds.y;

        // Set the world bounds for each moveable object
        foreach (MoveableObject obj in GetComponentsInChildren<MoveableObject>())
        {
            obj.SetWorldBounds(bounds);
        }
    }

    public void SetPlayMode(bool playMode)
    {
        // Toggle ball physics
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.useGravity = playMode;
        ballRb.velocity = Vector3.zero;

        // Toggle ball collision
        Collider ballCol = ball.GetComponent<Collider>();
        ballCol.enabled = playMode;

        // Reset the ball's position when entering edit mode
        if (!playMode)
        {
            ball.GetComponent<Player>().resetPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
