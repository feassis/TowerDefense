using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button QuitGameButton;
    [SerializeField] private Button levelSelectButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        QuitGameButton.onClick.AddListener(OnQuitGameButtonClicked);
        levelSelectButton.onClick.AddListener(OnLevelSelectButtonClicked);
    }

    private void OnLevelSelectButtonClicked()
    {
        SceneManager.LoadScene(SceneNames.LevelSelectSceneName);
    }

    private void OnQuitGameButtonClicked()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene(SceneNames.NewGameSceneName);
    }
}
