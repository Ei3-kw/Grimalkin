using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracker for heatmap
public class MouseTracker : MonoBehaviour
{
    private static bool isTracking = true;
    private static List<Vector2> mousePositions = new List<Vector2>();
    public RectTransform rectTransform;

    public Camera camera1;

    void Update()
    {
        if (isTracking)
        {
           // Debug.Log("WE INNN");
            // Track mouse position within image boundaries.
            //Debug.Log("Mouse Positions Count: " + Input.mousePosition);
            
            Vector2 mousePosition = Input.mousePosition;
            //Vector2 imagePosition = Camera.main.WorldToScreenPoint(transform.position);
            // Debug.Log("mouse position: "+mousePosition);
            // Debug.Log("image position: "+imagePosition);

            // mousePosition=Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log(mousePosition);

            Vector3 mouse3d = new Vector3(mousePosition.x, mousePosition.y, camera1.transform.position.z);
            //Debug.Log("WE INNN2");

            Vector3 PositionToCheck = camera1.ScreenToWorldPoint(mouse3d);
            // Check if the mousePosition is within the image boundaries.

            mousePositions.Add(mousePosition);
            //Debug.Log("WE INNN3");
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, PositionToCheck))
            {
                //Debug.Log("in range!!" + mousePosition + PositionToCheck);
               // mousePositions.Add(mousePosition);
            }
            else
            {
               //Debug.Log("out of range" + mousePosition + PositionToCheck);

                Vector3[] v = new Vector3[4];
                rectTransform.GetWorldCorners(v);

            }
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
