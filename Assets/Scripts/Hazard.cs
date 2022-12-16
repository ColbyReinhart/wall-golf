using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hazards trigger a game over screen when the ball collides with them
public class Hazard : MonoBehaviour
{
    private PanelController panelController;
    private AudioSource collisionSound;

    private void Start()
    {
        panelController = GameObject.Find("MenuCanvas").GetComponent<PanelController>();
        Debug.Assert(panelController != null);
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // panelController likes to randomly go null, so we have to do this.
        if (panelController == null)
        {
            Start();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // It's easier to just teleport the ball away than disable it
            other.transform.position = new Vector3(0, -100, 0);
            panelController.ToggleGameOverPanel(true);
            collisionSound.Play();
        }
    }
}
