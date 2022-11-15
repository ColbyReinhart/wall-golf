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
    private Rigidbody ballRb;
    private Collider ballCol;
    private MoveableObject[] allMoveables;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the ball
        ball = GameObject.FindGameObjectWithTag("Player");
        ballRb = ball.GetComponent<Rigidbody>();
        ballCol = ball.GetComponent<Collider>();

        // Get world coordinate bounds using screen dimensions
        Vector3 bottomLeftBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        WorldBounds bounds;
        bounds.bottomLeftX = bottomLeftBounds.x;
        bounds.bottomLeftY = bottomLeftBounds.y;
        bounds.width = topRightBounds.x - bottomLeftBounds.x;
        bounds.height = topRightBounds.y - bottomLeftBounds.y;

        // Gather all moveable objects
        allMoveables = GetComponentsInChildren<MoveableObject>();

        // Set the world bounds for each moveable object
        foreach (MoveableObject obj in allMoveables)
        {
            obj.SetWorldBounds(bounds);
        }

        SetPlayMode(false);
    }

    public void SetPlayMode(bool playMode)
    {
        // Toggle ball physics
        ballRb.useGravity = playMode;
        ballRb.velocity = Vector3.zero;

        // Toggle ball collision
        ballCol.enabled = playMode;

        // Reset the ball's position when entering edit mode
        if (!playMode)
        {
            ball.GetComponent<Player>().resetPosition();
        }

        // Set play mode for all moveable objects and make sure they
        // are active.
        foreach (MoveableObject obj in allMoveables)
        {
            ///Debug.Log(obj.gameObject.name);///
            obj.gameObject.SetActive(true);
            obj.SetPlayMode(playMode);
        }
    }
}
