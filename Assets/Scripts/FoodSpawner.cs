using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating("SpawnFood", 0f, spawnInterval);
    }

    // Update is called once per frame
    private void SpawnFood()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));

        GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        float randomSize = Random.Range(0.05f, 0.3f);
        newFood.transform.localScale = new Vector3(randomSize, randomSize, 1);
    }
}
