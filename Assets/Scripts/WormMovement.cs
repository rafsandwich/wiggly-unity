using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public float baseSpeed = 2f;
    public float rotationSpeed = 300f;

    public float boundaryRadius = 10f; // same as the background radius, only used if StayWithinBoundary()

    //public GameObject wormSegmentPrefab;

    public float baseWormLength = 1f;
    public float baseWormThickness = 0.5f;
    public float growthFactor = 0.1f;

    private float currentWormLength;
    private float currentWormThickness;

    void Start()
    {
        // set initial worm length and thickness
        currentWormLength = baseWormLength;
        currentWormThickness = baseWormThickness;

        // apply initial scale to worm
        transform.localScale = new Vector3(currentWormThickness, currentWormLength, 1);
    }

    void Update()
    {   
        // dealing with mass affecting speed
        //float currentSpeed = baseSpeed / wormSize;

        // moving the worm forward
        transform.Translate(Vector2.up * baseSpeed * Time.deltaTime);

        // rotating the worm based on player input (A/D or Left/Right Arrow keys)
        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        //StayWithinBoundary();
    }

    public void GrowWorm(float foodSize)
    {
        // increase worm length and thickness based on food size
        currentWormLength += foodSize * growthFactor;
        currentWormThickness += foodSize * growthFactor * 0.5f; // thickness grows slower than length

        // apply updated scale to the worm
        transform.localScale = new Vector3(currentWormThickness, currentWormLength, 1);

        Debug.Log("Current worm length is: " + currentWormLength);
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
