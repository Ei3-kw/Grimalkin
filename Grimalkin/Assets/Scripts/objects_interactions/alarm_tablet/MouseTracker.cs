/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To record / keep track of the "gaze" (mouse) positions of the user
 *   over a period of time when requested. and return this data
 *   when requested
 * 
 * Attached to objects in game scene:
 * - The tablet that will track the gaze during the alarm interaction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracker for heatmap
public class MouseTracker : MonoBehaviour
{
    private static bool isTracking = true;
    private static List<Vector2> mousePositions = new List<Vector2>();

    /*
     * Update is called at the start of every frame
     * 
     * Each frame we will check if we are currently tracking the 
     * "gaze" (mouse position) if we are, we will record the mouse
     * position at the current frame and add it to the list of positions
     */
    void Update()
    {
        // if we are currently tracking the mouse
        if (isTracking)
        {
            // Track mouse position within image boundaries.           
            Vector2 mousePosition = Input.mousePosition;

            // add position to the list of positions
            mousePositions.Add(mousePosition);
        }
    }

    /*
     * If we wish to enable "gaze" (mouse) tracking.
     * 
     * This should be run when the tracking starts
     * (when the tablet is opened)
     */
    public static void StartTracking()
    {
        isTracking = true;

        // Clear the previous data if needed.
        mousePositions.Clear();
    }

    /*
     * If we wish to disable "gaze" (mouse) tracking.
     * 
     * This should be run when the tracking ends
     * (when the tablet is closded)
     */
    public static void StopTracking()
    {
        isTracking = false;
    }

    /*
     * Will return the current list of mouse positions gathered over the tracking period.
     * This should be called when we are wanting to gernate the heat map
     *
     * Returns: the current list of mouse positions gathered over the tracking period
     */
    public static List<Vector2> GetMousePositions()
    {
        return mousePositions;
    }
}
