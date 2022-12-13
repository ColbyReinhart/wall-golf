using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hazards trigger a game over screen when the ball collides with them
public class Hazard : MonoBehaviour
{
    private PanelController panelController;

    private void Start()
    {
        panelController = GameObject.Find("MenuCanvas").GetComponent<PanelController>();
        Debug.Assert(panelController != null);
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
            other.gameObject.SetActive(false);
            Debug.Assert(panelController != null);
            panelController.OpenGameOverPanel();
        }
    }
}
