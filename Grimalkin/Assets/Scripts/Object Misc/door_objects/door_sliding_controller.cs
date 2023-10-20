/* 
 * Project Grimalkin
 * Author: Ella Wang
 * 
 * Purpose:
 * - handel sliding doors openeing and closing
 *   
 * 
 * Attached to objects in game scene:
 * - sliding doors in scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_sliding_controller : MonoBehaviour
{
    public float doorWidth = 2.0f; // The width of the door.
    public float doorWidth2 = 0.0f; // The width of the door.
    public float slidingSpeed = 2.0f; // The speed at which the door slides.
    public Transform Player; // Reference to the player.
    public float dir = -1; // direction to move the door to open
    private Vector3 closedPosition;
    private Vector3 openPosition;
    public bool isOpen = false;

    /*
     * Will be called before first frame
     * 
     * Record the closed and opened postions of the door within
     * the scene
     */
    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + dir * new Vector3(doorWidth, 0f, doorWidth2); // Calculate the open position to the left.
    }

    /*
     * Will be called every frame
     * 
     * If the door is open and we want it to close, move it closer to closed position
     * If the door is closed and we want it to open, move it closer to the open position.
     */
    private void Update()
    {
        if (isOpen)
        {
            // Slide the door towards the open position.
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slidingSpeed * Time.deltaTime);
        }
        else
        {
            // Slide the door back to the closed position.
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, slidingSpeed * Time.deltaTime);
        }
    }

    /*
     * If the player is pointing / looking at the door
     * and they are close enough to the door
     * and they interact with the door
     * 
     * then change the current state of the door (i.e. open to closed
     * or closed to open)
     */
    void OnMouseOver()
    {   
        if (Input.GetKeyDown("e"))
            // Check if the player is close enough to interact with the door.
            if (Player != null)
            {
                float distance = Vector3.Distance(transform.position, openPosition);
                if (distance < 15.0f)
                {
                    // Toggle the door's state when clicked.
                    isOpen = !isOpen;
                }
            }
    }
}
