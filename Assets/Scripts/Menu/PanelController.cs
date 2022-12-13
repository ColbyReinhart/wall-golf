using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject levelClearPanel;
    private GameObject pausePanel;
    private GameObject gameOverPanel;

    void Start()
    {
        // Get panels
        levelClearPanel = GameObject.Find("LevelClearPanel");
        pausePanel = GameObject.Find("PausePanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        // Validate references
        if (levelClearPanel == null)
        {
            Debug.LogError("Could not find LevelClearPanel in LevelClearPanel");
        }
        if (pausePanel == null)
        {
            Debug.LogError("Could not find PausePanel in PausePanel");
        }
        if (gameOverPanel == null)
        {
            Debug.LogError("Could not find GameOverPanel in GameOverPanel");
        }

        // Set panels to inactive
        levelClearPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OpenLevelClearPanel()
    {
        levelClearPanel.SetActive(true);
    }

    public void OpenGameOverPanel()
    {
        Debug.Log("Test");
        gameOverPanel.SetActive(true);
    }

    public void TogglePausePanel(bool paused)
    {
        pausePanel.SetActive(paused);
    }
}
