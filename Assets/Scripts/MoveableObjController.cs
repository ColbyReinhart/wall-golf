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

    private Player player;
    private MoveableObject[] allMoveables;

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

        // Assign references
        player = GameObject.FindObjectOfType<Player>();
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
        // Reset the ball's position when entering edit mode
        player.resetPosition(playMode);

        // Set play mode for all moveable objects and make sure they
        // are active.
        foreach (MoveableObject obj in allMoveables)
        {
            obj.gameObject.SetActive(true);
            obj.SetPlayMode(playMode);
        }
    }
}
