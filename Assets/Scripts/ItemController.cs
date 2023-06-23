using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private float activeTime;

    public BoxCollider2D gridArea;
    private Bounds bounds;

    private void Start()
    {
        bounds = gridArea.bounds;
        StartCoroutine(ItemLifetime(ItemType.GreenFood));
        StartCoroutine(ItemLifetime(ItemType.RedFood));
        StartCoroutine(ItemLifetime(ItemType.ShieldEffect));
        StartCoroutine(ItemLifetime(ItemType.SpeedUp));
        StartCoroutine(ItemLifetime(ItemType.ScoreBoost));
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
    IEnumerator ItemLifetime(ItemType itemType)
    {
        float activeTime = this.activeTime;

        while (true)
        {
            switch (itemType)
            {
                case ItemType.GreenFood:
                case ItemType.RedFood:
                case ItemType.ShieldEffect:
                case ItemType.SpeedUp:
                case ItemType.ScoreBoost:
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(activeTime);
            RandomizedPosition();
        }
    }   
    public ItemType GetItemType()
    {
        return itemType;
    }
}

//Enum for food type
public enum ItemType
{
    GreenFood,
    RedFood,
    ShieldEffect,
    SpeedUp,
    ScoreBoost
}