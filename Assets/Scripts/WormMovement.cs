using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float rotationSpeed = 300f;

    public float boundaryRadius = 10f; // same as the background radius, only used if StayWithinBoundary()

    public GameObject wormSegmentPrefab;
    private List<Transform> segments = new List<Transform>();
    public int initialSize = 1;

    void Start()
    {
        segments.Add(this.transform);
        GrowWorm(initialSize);
    }

    void Update()
    {
        // moving the snake forward
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        // rotating the snake based on player input (A/D or Left/Right Arrow keys)
        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        //StayWithinBoundary();

        UpdateWormSegments();
    }

    private void GrowWorm(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // instantiate new segment and add it to the list
            Transform newSegment = Instantiate(wormSegmentPrefab).transform;
            segments.Add(newSegment);
        }
    }

    private void UpdateWormSegments()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            // move each segment to the position of the segment ahead of it
            segments[i].position = segments[i - 1].position;
        }
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
