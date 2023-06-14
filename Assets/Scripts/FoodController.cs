using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public BoxCollider2D FoodgridArea;
    public GameObject ShieldEnable;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()  
    {
        Bounds bounds = this.FoodgridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RandomizePosition();
            Invoke("ShieldActive", 2f);
        }
    }

    private void ShieldActive()
    {
        ShieldEnable.SetActive(true);
    }
}
