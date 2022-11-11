﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}