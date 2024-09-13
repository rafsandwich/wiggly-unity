using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollision : MonoBehaviour
{

    private WormMovement wormMovement;
    private GameManager gameManager;

    private void Start()
    {
        wormMovement = GetComponent<WormMovement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Player worm ate some food!");

            // get the food object
            Food food = collision.gameObject.GetComponent<Food>();

            // grow the worm based on the food size
            wormMovement.GrowWorm(food.size);

            // destroy the food
            Destroy(collision.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Worm hit the edge, game over!");
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Segment"))
        {
            Debug.Log("Worm collided with its own segment, game over!");
            gameManager.GameOver();
        }
    }


}
