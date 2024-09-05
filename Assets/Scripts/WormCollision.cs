using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollision : MonoBehaviour
{

    private WormMovement wormMovement;

    private void Start()
    {
        wormMovement = GetComponent<WormMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Player worm ate some food!");
            wormMovement.GrowWorm(1);
            Destroy(collision.gameObject);
        }
    }
}
