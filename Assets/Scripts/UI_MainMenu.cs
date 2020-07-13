﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject tutorialMenu;
    public GameObject gameWinMenu;
    public GameObject gameLoseMenu;
    public GameObject gamePauseMenu;

    public bool isGameOver;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (mainMenu.activeSelf)
            {
                mainMenu.SetActive(false);
                tutorialMenu.SetActive(true);
            }
            else if (tutorialMenu.activeSelf)
            {
                tutorialMenu.SetActive(false);
                GameManager.GetInstance().StartGame();
            }

            if (gameWinMenu.activeSelf || gameLoseMenu.activeSelf)
            {
                GameManager.GetInstance().Restart();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.GetInstance().IsGameStarted && !GameManager.GetInstance().IsGamePaused)
        {
            GameManager.GetInstance().PauseGame();
            gamePauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.GetInstance().IsGameStarted && GameManager.GetInstance().IsGamePaused)
        {
            GameManager.GetInstance().ResumeGame();
            gamePauseMenu.SetActive(false);
        }
    }

    public void GameOver() {
        if (!isGameOver)
        {
            isGameOver = true; 
            gameLoseMenu.SetActive(true);
        }

    }

    public void GameWin() {
        if (!isGameOver)
        {
            isGameOver = true;
            gameWinMenu.SetActive(true);
        }

    }
}
