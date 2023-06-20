using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject massGainer;
    [SerializeField]
    private GameObject massBurner;
    [SerializeField]
    private GameObject shieldEffect;
    [SerializeField]
    private GameObject speedUp;
    [SerializeField]
    private GameObject scoreBoost;

    public BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        bounds = gridArea.bounds;
        InvokeRepeating(nameof(RandomSpawnMassGainer), 1f, 8f);
        InvokeRepeating(nameof(RandomSpawnMassBurner), 3f, 6f);
        InvokeRepeating(nameof(RandomSpawnShield), 6f, 4f);
        InvokeRepeating(nameof(RandomSpawnSpeedUp), 8f, 4f);
        InvokeRepeating(nameof(RandomSpawnScoreBoost), 10f, 4f);
    }
    private void RandomSpawnMassGainer()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(massGainer, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
    }

    private void RandomSpawnMassBurner()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(massBurner, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
    }
    private void RandomSpawnShield()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(shieldEffect, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
    }

    private void RandomSpawnSpeedUp()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(speedUp, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
    }

    private void RandomSpawnScoreBoost()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(scoreBoost, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
    }  

}