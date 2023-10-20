/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To show a heat map to display to the user of their gaze tracking
 *   data throughout the set period.
 * 
 * Attached to objects in game scene:
 * - Heat map object that lives within the alarm app within the tablet
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class AT_heatmap_controller : MonoBehaviour
{
    // keeps track of the heat map object
    public GameObject heatmap;

    /*
     * Will show the current heat map to the user as an overlay on the tablet
     */
    public void showHeatMap()
    {
        // Show the heat map to the user
        heatmap.GetComponent<AT_heatmap_generator>().show_heat_map();
    }   
}
