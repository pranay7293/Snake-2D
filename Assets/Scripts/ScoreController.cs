using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private SnakePlayer player;

    public int snake1Lives;
    public int snake2Lives;

    public int maxLives = 3;
    public Sprite emptyHeart;
    public Sprite Snake1fullHeart;
    public Sprite Snake2fullHeart;
    public Image[] liveS;
    private int snake1score = 0;
    private int snake2score = 0;
    private int Snake1maxScore = 0;
    private int Snake2maxScore = 0;


    public SnakeController snakeController;
    public GameManager gameManager;

    private void Start()
    {
        snake1Lives = maxLives;
        snake2Lives = maxLives;
    }

    private void Update()
    {
        UpdateLivesUI();
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < liveS.Length; i++)
        {
            if (i < maxLives)
            {
                if (i < snake1Lives)
                {
                    liveS[i].sprite = Snake1fullHeart;
                    liveS[i].enabled = true;
                }
                else
                {
                    liveS[i].sprite = emptyHeart;
                    liveS[i].enabled = true;
                }
            }
            else if (i < maxLives * 2)
            {
                int snake2Index = i - maxLives;
                if (snake2Index < snake2Lives)
                {
                    liveS[i].sprite = Snake2fullHeart;
                    liveS[i].enabled = true;
                }
                else
                {
                    liveS[i].sprite = emptyHeart;
                    liveS[i].enabled = true;
                }
            }
            else
            {
                liveS[i].sprite = emptyHeart;
                liveS[i].enabled = true;
            }
        }
    }

    public void SetSnakeController(SnakeController controller)
    {
        snakeController = controller;
    }


    public void MaxScore(int points, SnakePlayer player)
    {
        if (player == SnakePlayer.Snake1)
        {
            snake1score = points;
            if (snake1score > Snake1maxScore)
                Snake1maxScore = snake1score;
        }
        else if (player == SnakePlayer.Snake2)
        {
            snake2score = points;
            if (snake2score > Snake2maxScore)
                Snake2maxScore = snake2score;
        }
    }

    public void DecreaseLives(SnakePlayer player)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            snake1Lives--;
            if (snake1Lives == 0)
            {
                snakeController.isgamePaused = true;
                if(Snake1maxScore >= 200)
                {
                    StartCoroutine(DelayedSnakeWin1(3.0f));
                }
                else
                {
                    StartCoroutine(DelayedGameOver(3.0f));
                }
            }
            else
            {
                snakeController.isgamePaused = true;
                StartCoroutine(DelayedResetSnake(3.0f));
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (player == SnakePlayer.Snake1)
            {
                snake1Lives--;
                if (snake1Lives == 0)
                {
                    snakeController.isgamePaused = true;
                    StartCoroutine(DelayedSnakeWin2(3.0f));
                }
                else
                {
                    snakeController.Snake1score = 0;
                    snakeController.UpdateScoreUI();
                }

            }
            else if (player == SnakePlayer.Snake2)
            {
                snake2Lives--;
                if (snake2Lives == 0)
                {
                    snakeController.isgamePaused = true;
                    StartCoroutine(DelayedSnakeWin1(3.0f));
                }
                else
                {
                    snakeController.Snake2score = 0;
                    snakeController.UpdateScoreUI();
                }
            }
        }  
    }

    IEnumerator DelayedSnakeWin1(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameManager.SnakeWin(SnakePlayer.Snake1, Snake1maxScore);
        SoundManager.Instance.PlaySound(Sounds.SnakeWin);
    }
    IEnumerator DelayedSnakeWin2(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameManager.SnakeWin(SnakePlayer.Snake2, Snake2maxScore);
        SoundManager.Instance.PlaySound(Sounds.SnakeWin);
    }

    IEnumerator DelayedGameOver(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameManager.GameOver(Snake1maxScore);
        SoundManager.Instance.PlaySound(Sounds.GameOver);
    }
    IEnumerator DelayedResetSnake(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        snakeController.ResetSnake();
        snakeController.isgamePaused = false;
        SoundManager.Instance.PlaySound(Sounds.Respawn);
    }
}
