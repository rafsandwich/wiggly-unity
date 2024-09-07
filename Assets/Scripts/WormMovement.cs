using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public float baseSpeed = 2f;
    public float rotationSpeed = 300f;

    public float boundaryRadius = 10f; // same as the background radius, only used if StayWithinBoundary()

    public GameObject wormSegmentPrefab;

    public float baseWormLength = 1f;
    public float baseWormThickness = 0.5f;
    public float growthFactor = 0.1f;
    //public float segmentSpacing = 0.5f; // distance between segments

    private float currentWormLength;
    private float currentWormThickness;

    private List<Transform> segments = new List<Transform>(); // stores the worms segments
    private List<Vector2> previousPositions = new List<Vector2>(); // to track positions for segment following

    private float currentSpeed;

    public TextMeshProUGUI scoreText;

    //CapsuleCollider2D wormCollider;

    void Start()
    {

        // limiting frame rate for constant behaviour in editor and build
        Application.targetFrameRate = 60;

        // set initial worm length and thickness
        currentWormLength = baseWormLength;
        currentWormThickness = baseWormThickness;

        currentSpeed = baseSpeed;

        // apply initial scale to worm
        transform.localScale = new Vector3(currentWormThickness, currentWormThickness, 1); // currentWormLength

        // add the worm head as the first segment
        segments.Add(transform);

        // track initial position
        previousPositions.Add(transform.position);

        UpdateScore();

        //wormCollider = GetComponent<CapsuleCollider2D>();

        //wormCollider.size = new Vector2(currentWormThickness, currentWormLength);
    }

    private void Update()
    {
        // physics moved to FixedUpdate() to prevent speed changes during build

    }


    void FixedUpdate()
    {
        // dealing with mass affecting speed
        //float currentSpeed = baseSpeed / wormSize;

        // moving the worm forward using normalised direction to prevent speed changes due to scale
        Vector3 forwardDirection = transform.up.normalized;
        transform.position += forwardDirection * baseSpeed * Time.deltaTime;
        //transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);

        // rotating the worm based on player input (A/D or Left/Right Arrow keys)
        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        // store the head's current position for the segments to follow
        previousPositions.Insert(0, transform.position);

        // limit the stored positions
        if (previousPositions.Count > segments.Count * 10)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }

        // move each segment to follow the path of the head
        for (int i = 1; i < segments.Count; i++)
        {
            Transform segment = segments[i];

            if (i * 10 < previousPositions.Count) // fix argumentoutofrangeexception upon eating too much food at once
            {
                Vector2 targetPosition = previousPositions[i * 10]; // spacing between segments

                // smoothly move the segment towards the target position
                segment.position = Vector2.Lerp(segment.position, targetPosition, 0.5f);
            }
            //segment.rotation = Quaternion.identity; // if want to lock rotation of segments
        }


        //StayWithinBoundary();
    }

    public void GrowWorm(float foodSize)
    {
        // increase worm length and thickness based on food size
        //currentWormLength += foodSize * growthFactor;
        //currentWormThickness += foodSize * growthFactor * 0.5f; // thickness grows slower than length

        float sizeIncrease = growthFactor * foodSize;
        currentWormThickness += sizeIncrease;

        // scale all segments to the new thickness
        foreach (Transform segment in segments)
        {

            // apply updated scale to the worm
            segment.localScale = new Vector3(currentWormThickness, currentWormThickness, 1); //currentWormLength

        }

        Debug.Log("Current worm thickness is: " + currentWormThickness);


        // add segment based on size of food eaten
        //float segmentSize = Mathf.Clamp(foodSize * growthFactor, 0.1f, 1f);

        // instantiate new segmant at the last segment's position
        Transform lastSegment = segments[segments.Count - 1];
        GameObject newSegment = Instantiate(wormSegmentPrefab, lastSegment.position, Quaternion.identity);

        // scale the segment to match the size of the worm
        newSegment.transform.localScale = new Vector3(currentWormThickness, currentWormThickness, 1);

        segments.Add(newSegment.transform);

        // add spacing for new segments
        previousPositions.Add(newSegment.transform.position);

        UpdateScore();

    }

    void UpdateScore()
    {
        int score = Mathf.RoundToInt(currentWormThickness * 100);
        scoreText.text = "Score: " + score.ToString();
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
