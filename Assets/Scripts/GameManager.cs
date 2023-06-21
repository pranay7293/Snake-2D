using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public SnakeController snakeController;
    public ScoreController scoreController;

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
    [SerializeField]
    private TextMeshProUGUI MaxscoreWin;

    public GameObject pauseDisplay;
    public GameObject gameOverDisplay;
    public GameObject SnakeWinDisplay;

    private void Awake()
    {
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
        SoundManager.Instance.PlaySound(Sounds.ExitButtonClick);
        SceneManager.LoadScene(0);
        isGamePaused = false;
        pauseDisplay.SetActive(false);
        snakeController.isgamePaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        SoundManager.Instance.PlaySound(Sounds.Pause);
        isGamePaused = true;
        pauseDisplay.SetActive(true);
        snakeController.isgamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        isGamePaused = false;
        pauseDisplay.SetActive(false);
        snakeController.isgamePaused = false;
        Time.timeScale = 1f; 
    }

    private void RePlayMode()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void GameOver(int Maxscore)
    {
        gameOverDisplay.SetActive(true);
        MaxscoreText.text = "Max Score: " + Maxscore.ToString();
    }

    public void SnakeWin(SnakePlayer player, int Maxscore)
    {
        SnakeWinDisplay.SetActive(true);
        if (player == SnakePlayer.Snake1)
        {
            playerWinText.text = "Snake 1 Wins!!";
            MaxscoreWin.text = "Winner Max Score: " + Maxscore.ToString();
            MaxscoreWin.color = new Color32(0x00, 0x00, 0xFF, 0xFF); 

        }
        else if(player == SnakePlayer.Snake2)
        {
            playerWinText.text = "Snake 2 Wins!!";
            MaxscoreWin.text = "Winner Max Score: " + Maxscore.ToString();
            MaxscoreWin.color = new Color32(0xFF, 0xA5, 0x00, 0xFF);
        }
    }
}
