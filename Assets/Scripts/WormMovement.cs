using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    public float boundaryRadius = 10f; // same as the background radius

    void Start()
    {
        
    }

    void Update()
    {
        // moving the snake forward
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        // rotating the snake based on player input (A/D or Left/Right Arrow keys)
        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        //StayWithinBoundary();
    }

    void StayWithinBoundary()
    {
        // calculate the distance from the center of the game area (0,0)
        float distanceFromCenter = Vector2.Distance(Vector2.zero, transform.position);

        // if the user worm is outside the boundary, move it back inside
        if (distanceFromCenter > boundaryRadius)
        {
            // get the direction back towards the center
            Vector2 directionToCenter = (Vector2.zero - (Vector2)transform.position).normalized;

            // move the worm back inside the boundary
            transform.position = (Vector2.zero + directionToCenter * boundaryRadius);
        }
    }
}
