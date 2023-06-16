using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int lives;
    public int maxLives = 3;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] liveS;
    private int score = 0;
    private int maxScore = 0;    
    
    public SnakeController snakeController;

    private void Update()
    {
        for (int i = 0; i < liveS.Length; i++)
        {
            if (i < lives)
            {
                liveS[i].sprite = fullHeart;
            }
            else
            {
                liveS[i].sprite = emptyHeart;
            }

            if (i < maxLives)
            {
                liveS[i].enabled = true;
            }
            else
            {
                liveS[i].enabled = false;
            }
        }
    }
    public void IncreaseScore(int points)
    {
        score = points;
        if (score > maxScore)
            maxScore = score;
    }

    public void DecreaseLives()
    {
        lives--;
        if (lives <= 0)
        {
            GameManager.Instance.GameOver(maxScore);            
        }
        else
        {
            snakeController.ResetState();
        }
    }
}
