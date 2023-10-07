using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 startPosition; 
    public Vector3 endPosition;  
    public int numberOfFrames = 60; // Number of frames to complete the movement
    private int currentFrame = 0; // Current frame count
    private Vector3 currentPosition;
    private Vector3[] waypoints; // Array to store intermediate waypoints
    private int currentWaypoint = 0; // Index of the current waypoint
    private bool isMoving = false; // Flag to check if the camera is moving

    void Start()
    {
        // Calculate waypoints based on the number of frames
        waypoints = new Vector3[numberOfFrames + 1];

        for (int i = 0; i <= numberOfFrames; i++)
        {
            float t = i / (float)numberOfFrames;
            waypoints[i].x = startPosition.x + t*(endPosition.x - startPosition.x);
            waypoints[i].y = startPosition.y + t*(endPosition.y - startPosition.y);
            waypoints[i].z = startPosition.z + t*(endPosition.z - startPosition.z);
            // waypoints[i] = Vector3.Lerp(startPosition, endPosition, t);
        }

        currentPosition = startPosition;

        // Start moving the camera
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving)
            return;


        transform.position = waypoints[currentWaypoint];

        currentWaypoint++;

        // Check if we have reached the end of the waypoints
        if (currentWaypoint >= waypoints.Length)
        {
            isMoving = false;
        }

    }
}
