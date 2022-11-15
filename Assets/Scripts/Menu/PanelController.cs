using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject levelClearPanel;
    private GameObject pausePanel;

    void Start()
    {
        // Get panels
        levelClearPanel = GameObject.Find("LevelClearPanel");
        pausePanel = GameObject.Find("PausePanel");

        // Set panels to inactive
        levelClearPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OpenLevelClearPanel()
    {
        levelClearPanel.SetActive(true);
    }

    public void TogglePausePanel(bool paused)
    {
        pausePanel.SetActive(paused);
    }
}
