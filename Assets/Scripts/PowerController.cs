using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    [SerializeField]
    private PowerUpType powertype;

    [SerializeField]
    private float activeTime = 4f;

    private BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        gridArea = GameObject.FindGameObjectWithTag("GridArea").GetComponent<BoxCollider2D>();
        bounds = gridArea.bounds;
    }

    private void OnEnable()
    {
        StartCoroutine(PowerLifetime());
    }

    private void RandomizePosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeController>() != null)
        {
            RandomizePosition();
        }
    }

    IEnumerator PowerLifetime()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

}
//Enum for food type
public enum PowerUpType
{
    ShieldEffect,
    SpeedUp,
    ScoreBoost
}
