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

        if (panelController == null)
        {
            Debug.LogError("Hazard could not find PanelController in MenuCanvas!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            panelController.OpenGameOverPanel();
        }
    }
}
