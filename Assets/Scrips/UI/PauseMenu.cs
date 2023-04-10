using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton, resumeButton, 
        levelSelectButton, mainMenuButton;

    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        levelSelectButton.onClick.AddListener(OnLevelSelectButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void OnLevelSelectButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.LevelSelectSceneName);
    }

    private void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }
}
