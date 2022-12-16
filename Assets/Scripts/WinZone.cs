using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private AudioSource collisionSound;

    private void Awake()
    {
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collisionSound.Play();

            // Open the level clear panel
            // This only happens once, so we can be lazy about it
            GameObject.Find("MenuCanvas").GetComponent<PanelController>().OpenLevelClearPanel();

            // Advance the atLevel player pref, if applicable
            int atLevel = PlayerPrefs.GetInt("atLevel", 1);
            if (atLevel == GameObject.FindObjectOfType<LevelLoader>().currentLevel)
            {
                PlayerPrefs.SetInt("atLevel", atLevel + 1);
            }
        }
    }

}
