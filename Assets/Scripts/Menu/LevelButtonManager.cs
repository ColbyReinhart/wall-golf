using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    private Button[] levelButtons;

    void Start()
    {
        // Get all buttons, and the current level the player is at
        levelButtons = GetComponentsInChildren<Button>();
        int atLevel = PlayerPrefs.GetInt("atLevel", 1);

        // Disable all buttons for levels the player can't access yet
        for (int i = 0; i < levelButtons.Length; i++)
        {
            LevelButton levelButton = levelButtons[i].GetComponent<LevelButton>();
            levelButtons[i].interactable = levelButton.associatedLevel <= atLevel;
        }
    }
}
