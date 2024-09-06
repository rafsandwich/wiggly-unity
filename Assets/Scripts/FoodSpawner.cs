using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    //public Vector2 spawnAreaMin;
    //public Vector2 spawnAreaMax;

    public float spawnRadius = 10f; // radius of the game area circle
    public float spawnInterval = 2f;

    public float minFoodSize = 0.05f;
    public float maxFoodSize = 0.3f;

    private void Start()
    {
        InvokeRepeating("SpawnFood", 0f, spawnInterval);
    }

    // Update is called once per frame
    private void SpawnFood()
    {
        //Vector2 spawnPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));

        // spawn in random position within the game area circus
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

        // instantiate at the random position
        GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        float randomSize = Random.Range(minFoodSize, maxFoodSize);
        newFood.transform.localScale = new Vector3(randomSize, randomSize, 1);

        // match collider size to visual size
        newFood.GetComponent<CircleCollider2D>().radius = randomSize / 2;

        // store the size in a variable to pass on when eaten
        newFood.GetComponent<Food>().size = randomSize;

    }
}
