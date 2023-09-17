using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracker for heatmap
public class MouseTracker : MonoBehaviour
{
    private static bool isTracking = true;
    private static List<Vector2> mousePositions = new List<Vector2>();
    public RectTransform rectTransform;

    void Update()
    {
        if (isTracking)
        {
            // Track mouse position within image boundaries.
            //Debug.Log("Mouse Positions Count: " + Input.mousePosition);
            
            Vector2 mousePosition = Input.mousePosition;
            //Vector2 imagePosition = Camera.main.WorldToScreenPoint(transform.position);
            // Debug.Log("mouse position: "+mousePosition);
            // Debug.Log("image position: "+imagePosition);

            // mousePosition=Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log(mousePosition);
           
            Vector2 PositionToCheck=Camera.main.ScreenToWorldPoint(mousePosition);
            // Check if the mousePosition is within the image boundaries.
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, PositionToCheck))
            {
                //Debug.Log("in range!!");
                mousePositions.Add(mousePosition);
            }
            //Debug.Log("out of range");
        }
    }

    public void StartTracking()
    {
        isTracking = true;
        // Clear the previous data if needed.
        mousePositions.Clear();
    }

    public static void StopTracking()
    {
        isTracking = false;
    }

    public static  List<Vector2> GetMousePositions()
    {
        return mousePositions;
    }
}
