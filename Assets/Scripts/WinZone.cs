using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public PanelController panelController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The player shouldn't be able to interact with the
            // level after they win.
            GameObject.Find("InputController").SetActive(false);

            panelController.OpenLevelClearPanel();
        }
    }
}
