using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreenGraphics : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private Button mainMenuButton, levelSelectButton, retryButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(MainMenu);
        levelSelectButton.onClick.AddListener(SelectLavel);
        retryButton.onClick.AddListener(RetryLevel);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }

    private void SelectLavel()
    {
        SceneManager.LoadScene(SceneNames.LevelSelectSceneName);
    }

    private void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        ServiceLocator.GetService<LevelManager>().OnDefeat += HandleDefeat;
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<LevelManager>().OnDefeat -= HandleDefeat;
    }

    private void HandleDefeat()
    {
        canvas.SetActive(true);
    }
}
