using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int initialSize;

    public GameObject shieldeffect;
    private bool hasShield;
    private float shieldDuration = 4f;
    private float shieldTimer;

    private Vector2Int direction = Vector2Int.right;

    public BoxCollider2D gridArea;
    private Bounds bounds;

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
        SnakeMovement();
        MovementChange();

        if (hasShield)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                // Deactivate the shield
                DeactivateShield();
            }
        }
    }
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            this.transform.position.x + direction.x * speed,
            this.transform.position.y + direction.y * speed,
            0.0f);
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction.x != 1)
        {
            direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction.x != -1)
        {
            direction = Vector2Int.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && direction.y != -1)
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction.y != 1)
        {
            direction = Vector2Int.down;
        }
    }

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GreenFood"))
        {
            Grow();
        }
        else if (collision.CompareTag("RedFood"))
        {
            Shrink();
        }
        else if (collision.CompareTag("Shield"))
        {
            ActivateShield();
        }
        else if (!hasShield && collision.CompareTag("Obstacle"))
        {
            ResetState();           
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

    private void ResetState()
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
        shieldTimer = shieldDuration;
    }
    private void DeactivateShield()
    {
        hasShield = false;
        shieldeffect.SetActive(false);
    }

}
