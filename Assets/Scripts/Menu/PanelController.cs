using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject levelClearPanel;

    void Start()
    {
        // Set panels to inactive
        levelClearPanel.SetActive(false);
    }

    public void OpenLevelClearPanel()
    {
        levelClearPanel.SetActive(true);
    }
}
