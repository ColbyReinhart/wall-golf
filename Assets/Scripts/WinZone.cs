using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public PanelController panelController;
    public LevelLoader levelLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The player shouldn't be able to interact with the
            // level after they win.
            GameObject.Find("InputController").SetActive(false);

            // Open the level clear panel
            panelController.OpenLevelClearPanel();

            // Advance the atLevel player pref, if applicable
            int atLevel = PlayerPrefs.GetInt("atLevel", 1);
            if (atLevel == levelLoader.currentLevel)
            {
                PlayerPrefs.SetInt("atLevel", atLevel + 1);
            }
        }
    }
}
