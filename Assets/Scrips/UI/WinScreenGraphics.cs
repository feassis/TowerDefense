using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenGraphics : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private Button mainMenuButton, levelSelectButton, nextLevelButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(MainMenu); 
        levelSelectButton.onClick.AddListener(SelectLavel);
        nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }

    private void SelectLavel()
    {
        SceneManager.LoadScene(SceneNames.LevelSelectSceneName);
    }

    private void GoToNextLevel()
    {
        var name = ServiceLocator.GetService<LevelManager>().GetNextLevelName();

        SceneManager.LoadScene(name);
    }

    private void Start()
    {
        ServiceLocator.GetService<LevelManager>().OnWin += HandleWin;
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<LevelManager>().OnWin -= HandleWin;
    }

    private void HandleWin()
    {
        canvas.SetActive(true);
    }
}
