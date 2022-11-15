using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int totalLevels = 3; // How many levels are there?
    public int currentLevel = 0;

    public void NextLevel()
    {
        ++currentLevel;

        if (currentLevel <= totalLevels)
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
        else
        {
            LoadTitleScreen();
        }
    }

    public void PlayAgain()
    {
        Load(currentLevel);
    }

    public void Load(int whatLevel)
    {
        SceneManager.LoadScene("Level" + whatLevel);
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
