using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public float baseSpeed = 2f;
    public float rotationSpeed = 300f;

    public float boundaryRadius = 10f; // same as the background radius, only used if StayWithinBoundary()

    //public GameObject wormSegmentPrefab;
    //private List<Transform> segments = new List<Transform>();
    //public int initialSize = 1;

    public Transform wormHead;

    private int wormSize = 1;

    void Start()
    {

    }

    void Update()
    {   
        // dealing with mass affecting speed
        float currentSpeed = baseSpeed / wormSize;

        // moving the snake forward
        wormHead.Translate(Vector2.up * currentSpeed * Time.deltaTime);

        // rotating the snake based on player input (A/D or Left/Right Arrow keys)
        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        //StayWithinBoundary();
    }

    public void GrowWorm(int addedSize)
    {
        wormSize += addedSize;
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
