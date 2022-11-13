using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    private static Game_Manager instance = null;
    public static Game_Manager sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Game_Manager>();
            }
            return instance;
        }
    }

    #region Variables
    public static bool _gameIsPaused = false;
    public static bool _gameHasEnded = false;

    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _endingPanel;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game is Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameIsPaused && ControllerManager.EscapeKey) PauseGame();
        if (_gameIsPaused && ControllerManager.EscapeKey) ResumeGame();
        if (IA_Manager.sharedInstance.NbCheater == 0 && !_gameHasEnded) GameEnding();

    }
    void GameEnding()
    {
        _gameHasEnded = true;
        _endingPanel.SetActive(true);
    }
    public void StayAfterEnding()
    {
        _endingPanel.SetActive(false);
    }
    public void PauseGame()
    {
        ControllerManager.EscapeKey = false;
        ControllerManager.leftClick = false;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        _gameIsPaused = true;
        _pauseMenu.SetActive(true);

    }
    public void ResumeGame()
    {
        ControllerManager.EscapeKey = false;
        ControllerManager.leftClick = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        _gameIsPaused = false;
        _pauseMenu.SetActive(false);
    }
    public void LoadMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
        Debug.Log("Load Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
