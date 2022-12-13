using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject levelClearPanel;
    private GameObject pausePanel;
    private GameObject gameOverPanel;

    private void Start()
    {
        // Get panels
        levelClearPanel = transform.Find("LevelClearPanel").gameObject;
        pausePanel = transform.Find("PausePanel").gameObject;
        gameOverPanel = transform.Find("GameOverPanel").gameObject;

        // Validate references
        Debug.Assert(levelClearPanel != null);
        Debug.Assert(pausePanel != null);
        Debug.Assert(gameOverPanel != null);

        // Set panels to inactive
        levelClearPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public bool IsMenuActive()
    {
        return levelClearPanel.activeInHierarchy
            || pausePanel.activeInHierarchy
            || gameOverPanel.activeInHierarchy;
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
