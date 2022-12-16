using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public GameObject highlight;

    private InputController inputController;
    private bool isSelected;
    private MoveableObjController.WorldBounds bounds;
    private AudioSource collisionSound;
    private const float minVolumeSpeed = 3f;
    private const float maxVolumeSpeed = 15f;

    void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnMouseDrag()
    {
        if (isSelected)
        {
            // Get the position of where the object is to be moved
            Vector3 newPos = transform.position + inputController.getMouseDelta();

            // Make sure this is within legal bounds
            if (newPos.x > bounds.bottomLeftX + bounds.width) newPos.x = bounds.bottomLeftX + bounds.width;
            else if (newPos.x < bounds.bottomLeftX) newPos.x = bounds.bottomLeftX;

            if (newPos.y > bounds.bottomLeftY + bounds.height) newPos.y = bounds.bottomLeftY + bounds.height;
            else if (newPos.y < bounds.bottomLeftY) newPos.y = bounds.bottomLeftY;

            // Move the object to the new position
            transform.position = newPos;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // If the ball is going fast enough, play a sound
      //  if (collision.relativeVelocity.magnitude >= minVolumeSpeed)
       // {
            // Volume is dependent on how fast the ball is moving
            collisionSound.volume = (collision.relativeVelocity.magnitude - minVolumeSpeed) / (maxVolumeSpeed - minVolumeSpeed);
            collisionSound.Play();
      //  }
    }


    public void Select()
    {
        isSelected = true;
        highlight.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        highlight.SetActive(false);
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

    // Override this if your object needs to do something special when toggling modes
    public virtual void SetPlayMode(bool play) { }
}
