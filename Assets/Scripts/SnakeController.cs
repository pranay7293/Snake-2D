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
    private float speed;
    [SerializeField]
    private TextMeshProUGUI scoreTextSnake1;
    [SerializeField]
    private TextMeshProUGUI scoreTextSnake2;
    [SerializeField]
    private ScoreController scoreController;

    public GameObject floatingTextPrefab;
    public GameObject shieldeffect;
    public GameObject speedUp;
    public GameObject scoreBoost;

    public bool isgamePaused = false;
    private bool hasShield;
    private bool hasspeedUp;
    private bool hasScoreboost;

    private float PowerAcivated = 3f;
    private int Snake1score = 0;
    private int Snake2score = 0;

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
        if (!isgamePaused)
        {
            SnakeMovement();
            MovementChange();
        }              
    }

    private void FixedUpdate()
    {
        if (!isgamePaused)
        {

            if(player == SnakePlayer.Snake1)
            {
                for (int i = _segments.Count - 1; i > 0; i--)
                {
                    _segments[i].position = _segments[i - 1].position;
                }
                this.transform.position = new Vector3(
                    Mathf.Round(this.transform.position.x + direction1.x),
                    Mathf.Round(this.transform.position.y + direction1.y),
                    0.0f);
            }
            else if (player == SnakePlayer.Snake2)
            {
                for (int i = _segments.Count - 1; i > 0; i--)
                {
                    _segments[i].position = _segments[i - 1].position;
                }
                this.transform.position = new Vector3(
                    Mathf.Round(this.transform.position.x + direction2.x),
                    Mathf.Round(this.transform.position.y + direction2.y),
                    0.0f);
            }
        }
    }    

    private void MovementChange()
    {
        float roundedX = (float)Math.Round(transform.position.x);
        float roundedY = (float)Math.Round(transform.position.y);

        if (roundedX > bounds.max.x)
        {
            transform.position = new Vector3((float)Math.Round(bounds.min.x), roundedY, 0.0f);
        }
        else if (roundedX < bounds.min.x)
        {
            transform.position = new Vector3((float)Math.Round(bounds.max.x), roundedY, 0.0f);
        }
        else if (roundedY < bounds.min.y)
        {
            transform.position = new Vector3(roundedX, (float)Math.Round(bounds.max.y), 0.0f);
        }
        else if (roundedY > bounds.max.y)
        {
            transform.position = new Vector3(roundedX, (float)Math.Round(bounds.min.y), 0.0f);
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
        else if (player == SnakePlayer.Snake2)
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
            return 0; // 180 degrees rotation
        }
        return 0f;
    }

    private void UpdateScore(bool isGreenFood, SnakePlayer player)
    {
        int scoreChange = isGreenFood ? 10 : -10;

        if (hasScoreboost)
        {
            scoreChange *= 2;
        }
        if (player == SnakePlayer.Snake1)
        {
            Snake1score += scoreChange; // Update Snake 1 score
        }
        else if (player == SnakePlayer.Snake2)
        {
            Snake2score += scoreChange; // Update Snake 2 score
        }
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if(player == SnakePlayer.Snake1)
        {
            scoreTextSnake1.text = "Score: " + Snake1score.ToString();
        }
        else if(player == SnakePlayer.Snake2)
        {
            scoreTextSnake2.text = "Score: " + Snake2score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isgamePaused)
        {

            if (collision.CompareTag("GreenFood"))
            {
                if (player == SnakePlayer.Snake1)
                {
                    SoundManager.Instance.Play(Sounds.GreenFood);
                    UpdateScore(true, SnakePlayer.Snake1);
                    DisplayFloatingText("Snake1 player eat Green Food", new Vector3(-12, -12, 0));

                }
                else if (player == SnakePlayer.Snake2)
                {
                    SoundManager.Instance.Play(Sounds.GreenFood);
                    UpdateScore(true, SnakePlayer.Snake2);
                    DisplayFloatingText("Snake2 player eat Green Food", new Vector3(-12, -12, 0));

                }
                Grow();
            }
            else if (collision.CompareTag("RedFood"))
            {
                if (player == SnakePlayer.Snake1)
                {
                    SoundManager.Instance.Play(Sounds.RedFood);
                    UpdateScore(false, SnakePlayer.Snake1);
                    DisplayFloatingText("Snake1 player eat Red Food", new Vector3(-12, -13, 0));

                }
                else if (player == SnakePlayer.Snake2)
                {
                    SoundManager.Instance.Play(Sounds.RedFood);
                    UpdateScore(false, SnakePlayer.Snake2);
                    DisplayFloatingText("Snake2 player eat Red Food", new Vector3(-12, -13, 0));
                }
                Shrink();
            }
            else if (collision.CompareTag("Shield"))
            {
                if (hasShield)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has a shield", new Vector3(12, -12, 0));
                }
                else if (hasspeedUp || hasScoreboost)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has an active power-up", new Vector3(12, -12, 0));
                }
                else
                {
                    ActivateShield();
                    SoundManager.Instance.Play(Sounds.Shield);
                    DisplayFloatingText("Snake player's Shield Power is activated", new Vector3(12, -12, 0));
                }
            }
            else if (collision.CompareTag("SpeedUp"))
            {
                if (hasspeedUp)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has a speed boost", new Vector3(12, -13, 0));
                }
                else if (hasShield || hasScoreboost)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has an active power-up", new Vector3(12, -13, 0));
                }
                else
                {
                    ActivateSpeedUp();
                    SoundManager.Instance.Play(Sounds.SpeedUp);
                    DisplayFloatingText("Snake player's SpeedUp Power is activated", new Vector3(12, -13, 0));

                }
            }
            else if (collision.CompareTag("ScoreBoost"))
            {
                if (hasScoreboost)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has a score boost", new Vector3(12, -14, 0));
                }
                else if (hasShield || hasspeedUp)
                {
                    SoundManager.Instance.Play(Sounds.Error);
                    DisplayFloatingText("Snake player already has an active power-up", new Vector3(12, -14, 0));
                }
                else
                {
                    ActivateScoreBoost();
                    SoundManager.Instance.Play(Sounds.ScoreBoost);
                    DisplayFloatingText("Snake player's ScoreBoost Power is activated", new Vector3(12, -14, 0));

                }
            }
            else if (!hasShield && collision.CompareTag("Obstacle") && SceneManager.GetActiveScene().buildIndex == 1)
            {
                scoreController.MaxScore(Snake1score, SnakePlayer.Snake1);
                scoreController.DecreaseLives(SnakePlayer.Snake1);
                SoundManager.Instance.Play(Sounds.SelfBite);
                DisplayFloatingText("Oh God! Snake bites itself", new Vector3(0, -12, 0));
                Snake1score = 0;
                scoreTextSnake1.text = "Score: " + Snake1score.ToString();
            }
            else if (!hasShield && collision.CompareTag("Obstacle") && SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (player == SnakePlayer.Snake1)
                {
                    scoreController.MaxScore(Snake1score, SnakePlayer.Snake1);
                    scoreController.DecreaseLives(SnakePlayer.Snake2);
                    SoundManager.Instance.Play(Sounds.SnakeBite);
                    DisplayFloatingText("Hey! Snake 1 bites Snake 2", new Vector3(0, -12, 0));
                    Snake2score = 0;
                    scoreTextSnake1.text = "Score: " + Snake2score.ToString();
                }
                else if (player == SnakePlayer.Snake2)
                {
                    scoreController.MaxScore(Snake2score, SnakePlayer.Snake2);
                    scoreController.DecreaseLives(SnakePlayer.Snake1);
                    SoundManager.Instance.Play(Sounds.SnakeBite);
                    DisplayFloatingText("Hey! Snake 2 bites Snake 1", new Vector3(0, -12, 0));
                    Snake1score = 0;
                    scoreTextSnake2.text = "Score: " + Snake1score.ToString();
                }
            }
        }
    }
    public void DisplayFloatingText(string text, Vector3 position)
    {
        GameObject prefab = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = text;
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

    public void ResetState(SnakePlayer player)
    {        
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
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
        speedUp.SetActive(false);
        Time.fixedDeltaTime = 0.09f;

    }
    private void DeactivateScoreBoost()
    {
        hasScoreboost = false;
        scoreBoost.SetActive(false);
    }
}
