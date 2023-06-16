using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    public SnakeController snakeController;

    public Button pauseButton;
    public Button continueButton;
    public Button exitButton;
    public Button replyButton;
    public Button exitButton2;
    public Button exitButton3;
    public Button replyAgain;


    [SerializeField]
    private TextMeshProUGUI MaxscoreText;
    [SerializeField]
    private TextMeshProUGUI playerWinText;

    public GameObject pauseDisplay;
    public GameObject gameOverDisplay;
    public GameObject SnakeWinDisplay;


    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExittoLobby);
        replyButton.onClick.AddListener(RePlayMode);
        exitButton2.onClick.AddListener(ExittoLobby);
        replyAgain.onClick.AddListener(RePlayMode);
        exitButton3.onClick.AddListener(ExittoLobby);
    }

    private void ExittoLobby()
    {
        SceneManager.LoadScene(0);
        isPaused = false;
        pauseDisplay.SetActive(false);
        snakeController.isPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseDisplay.SetActive(true);
        snakeController.isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseDisplay.SetActive(false);
        snakeController.isPaused = false;
        Time.timeScale = 1f; 
    }
    private void RePlayMode()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void GameOver(int Maxscore)
    {
        gameOverDisplay.SetActive(true);
        snakeController.isPaused = true;
        MaxscoreText.text = "Max Score: " + Maxscore.ToString();
    }
    public void SnakeWin(SnakePlayer player)
    {
        SnakeWinDisplay.SetActive(true);
        if (player == SnakePlayer.Snake1)
        {
            ChangeText("Snake 1 Wins!!");
        }
        else if(player == SnakePlayer.Snake2)
        {
            ChangeText("Snake 2 Wins!!");
        }
    }

    private void ChangeText(string text)
    {
        playerWinText.text = text;
    }
}
