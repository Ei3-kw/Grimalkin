using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float doorWidth = 2.0f; // The width of the door.
    public float doorWidth2 = 0.0f; // The width of the door.
    public float slidingSpeed = 2.0f; // The speed at which the door slides.
    public Transform Player; // Reference to the player.
    public float dir = -1;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    public bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + dir * new Vector3(doorWidth, 0f, doorWidth2); // Calculate the open position to the left.
    }

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
