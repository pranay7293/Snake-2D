using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum SnakePlayer
{
    Snake1,
    Snake2
}
public class SnakeController : MonoBehaviour
{
    [SerializeField]
    public SnakePlayer player;

    [SerializeField]
    private int initialSize;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public ScoreController scoreController;

    public bool isPaused = false;


    public GameObject shieldeffect;
    public GameObject speedUp;
    public GameObject scoreBoost;
    private bool hasShield;
    private bool hasspeedUp;
    private bool hasScoreboost;
    private float PowerAcivated = 3f;
    private int score = 0;

    public BoxCollider2D gridArea;
    private Bounds bounds;

    private Vector2Int direction1 = Vector2Int.right;
    private Vector2Int direction2 = Vector2Int.left;


    private List<Transform> _segments;
    public Transform segmentPrefab;
    

    private void Start()
    {
        bounds = gridArea.bounds;
        hasShield = false;

        _segments = new List<Transform>();
        _segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab, transform.position, Quaternion.identity));
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            SnakeMovement();
            MovementChange();
        }              
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            if(player == SnakePlayer.Snake1)
            {
                for (int i = _segments.Count - 1; i > 0; i--)
                {
                    _segments[i].position = _segments[i - 1].position;
                }

                this.transform.position = new Vector3(
                    this.transform.position.x + direction1.x,
                    this.transform.position.y + direction1.y,
                    0.0f);
            }
            else if (player == SnakePlayer.Snake2)
            {
                for (int i = _segments.Count - 1; i > 0; i--)
                {
                    _segments[i].position = _segments[i - 1].position;
                }

                this.transform.position = new Vector3(
                    this.transform.position.x + direction2.x,
                    this.transform.position.y + direction2.y,
                    0.0f);
            }

        }
    }    

    private void MovementChange()
    {
        if (transform.position.x > bounds.max.x)
        {
            transform.position = new Vector3(bounds.min.x, transform.position.y, 0.0f);
        }
        else if (transform.position.x < bounds.min.x)
        {
            transform.position = new Vector3(bounds.max.x, transform.position.y, 0.0f);
        }
        else if (transform.position.y < bounds.min.y)
        {
            transform.position = new Vector3(transform.position.x, bounds.max.y, 0.0f);
        }
        else if (transform.position.y > bounds.max.y)
        {
            transform.position = new Vector3(transform.position.x, bounds.min.y, 0.0f);
        }
    }

    private void SnakeMovement()
    {
        if (player == SnakePlayer.Snake1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && direction1.x != 1)
            {
                direction1 = Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && direction1.x != -1)
            {
                direction1 = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && direction1.y != -1)
            {
                direction1 = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && direction1.y != 1)
            {
                direction1 = Vector2Int.down;
            }
            shieldeffect.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction1));
            scoreBoost.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction1));
            speedUp.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction1));
        }
        else if(player == SnakePlayer.Snake2)
        {
            if (Input.GetKeyDown(KeyCode.A) && direction2.x != 1)
            {
                direction2 = Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && direction2.x != -1)
            {
                direction2 = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.W) && direction2.y != -1)
            {
                direction2 = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && direction2.y != 1)
            {
                direction2 = Vector2Int.down;
            }
            shieldeffect.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction2));
            scoreBoost.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction2));
            speedUp.transform.rotation = Quaternion.Euler(0, 0, GetRotationAngleFromDirection(direction2));
        }
    }

    private float GetRotationAngleFromDirection(Vector2Int directionx)
    {
        if (directionx == Vector2Int.up)
        {
            return 90f; // 90 degrees rotation
        }
        else if (directionx == Vector2Int.right)
        {
            return 0f; // No rotation
        }
        else if (directionx == Vector2Int.down)
        {
            return -90f; // -90 degrees rotation
        }
        else if (directionx == Vector2Int.left)
        {
            return 180f; // 180 degrees rotation
        }
        return 0f;
    }

    private void UpdateScore(bool isGreenFood)
    {
        int scoreChange = isGreenFood ? 10 : -10;

        if (hasScoreboost)
        {
            scoreChange *= 2;
        }        
            score += scoreChange;

        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPaused)
        {

            if (collision.CompareTag("GreenFood"))
            {
                UpdateScore(true);
                Grow();
            }
            else if (collision.CompareTag("RedFood"))
            {
                UpdateScore(false);
                Shrink();
            }
            else if (collision.CompareTag("Shield"))
            {
                ActivateShield();
            }
            else if (collision.CompareTag("SpeedUp"))
            {
                ActivateSpeedUp();
            }
            else if (collision.CompareTag("ScoreBoost"))
            {
                ActivateScoreBoost();
            }
            else if (!hasShield && collision.CompareTag("Obstacle") && SceneManager.GetActiveScene().buildIndex == 1)
            {
                scoreController.IncreaseScore(score);
                scoreController.DecreaseLives();
                score = 0;
                scoreText.text = "Score: " + score.ToString();
            }
            else if (!hasShield && collision.CompareTag("Obstacle") && SceneManager.GetActiveScene().buildIndex == 2)
            {
                isPaused = true;
                Invoke(nameof(PlayerWin), 3f);
            }
        }

    }
    private void PlayerWin()
    {
        if (player == SnakePlayer.Snake1)
        {
            GameManager.Instance.SnakeWin(SnakePlayer.Snake2);
        }
        else if (player == SnakePlayer.Snake2)
        {
            GameManager.Instance.SnakeWin(SnakePlayer.Snake1);
        }
        
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void Shrink()
    {
        if (_segments.Count > initialSize)
        {
            Transform lastSegment = _segments[_segments.Count - 1];
            _segments.RemoveAt(_segments.Count - 1);
            Destroy(lastSegment.gameObject);

        }
    }

    public void ResetState()
    {
    for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for(int i =1;i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }


    private void ActivateShield()
    {
        hasShield = true;
        shieldeffect.SetActive(true);
        StartCoroutine(PowerTimerRoutine());
    }   
    
    private void ActivateSpeedUp()
    {
        hasspeedUp = true;
        speedUp.SetActive(true);
        Time.fixedDeltaTime = 0.04f;
        StartCoroutine(PowerTimerRoutine());    
    }
    
    private void ActivateScoreBoost()
    {
        hasScoreboost = true;
        scoreBoost.SetActive(true);
        StartCoroutine(PowerTimerRoutine());
    }    

    private IEnumerator PowerTimerRoutine()
    {
        yield return new WaitForSeconds(PowerAcivated);
        if(hasShield)
        {
            DeactivateShield();
        }
        if(hasspeedUp)
        {
            DeactivateSpeedUp();
        }
        if (hasScoreboost)
        {
            DeactivateScoreBoost();
        }        
    }

    private void DeactivateShield()
    {
        hasShield = false;
        shieldeffect.SetActive(false);
    }
    private void DeactivateSpeedUp()
    {
        hasspeedUp = false;
        Time.fixedDeltaTime = 0.09f;
        speedUp.SetActive(false);
    }
    private void DeactivateScoreBoost()
    {
        hasScoreboost = false;
        scoreBoost.SetActive(false);
    }
}
