using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private float activeTime;

    private BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        gridArea = GameObject.FindGameObjectWithTag("GridArea").GetComponent<BoxCollider2D>();
        bounds = gridArea.bounds;
    }
    private void OnEnable()
    {
        StartCoroutine(ItemLifetime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeController>() != null)
        {
            RandomizedPosition();
        }
    }
    private void RandomizedPosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    IEnumerator ItemLifetime()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }
   
    public ItemType GetItemType()
    {
        return itemType;
    }
}

//Enum for food type
public enum ItemType
{
    MassGainer,
    MassBurner,
    ShieldEffect,
    SpeedUp,
    ScoreBoost
}