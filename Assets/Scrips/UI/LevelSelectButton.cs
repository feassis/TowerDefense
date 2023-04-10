using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private Button levelSelectButtom;
    [SerializeField] private string levelName;

    private void Awake()
    {
        levelSelectButtom.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
