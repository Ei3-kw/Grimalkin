/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the stop button that is present on the tablet
 *   and designed to stop the alarm when pressed
 * - To start and stop the gaze tracking and display the heat map
 *   of their gaze to the user when done if in demo mode
 * 
 * Attached to objects in game scene:
 * - The stop button that is attached to the alarm tablet
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopButtonController : MonoBehaviour
{
    // heat map object that will display the heat map data
    public GameObject heatmap; 

    // wall paper that will sit behind the heat map to demonstrate what the user was looking at
    public GameObject heatmap_wallpaper;

    // the tablet that the alarm is played from
    public GameObject tablet;

    // the text on the screen of the ipad telling the user what is happening (e.g. tablet updating...)
    public TextMeshPro ipad_text;

    // the object that will produce the alarm sound when triggered
    public GameObject alarm_sound;

    // the player object in the scene
    public GameObject player;

    // the subtitles UI
    public TextMeshProUGUI subtitles;

    // if the stop button has been clicked yet
    private bool clicked = false;

    // if we are in the demo mode version of the alarm interation
    public bool demo_mode = false;

    /*
     * Called when the stop alarm button is pressed
     * 
     * It will turn off the alarm music and progress to the next stage of the 
     * interaction.
     */
    void OnMouseDown()
    {
        // turn of music
        alarm_sound.SetActive(false);

        // if the button has not alreay been clicked yet
        if (clicked == false)
        {
            clicked = true;
            // begin the next stage of the interaction when tablet updates
            StartCoroutine(updating_process());
        }
    }

    /*
     * If we do not want to wait for the user to have to press
     * the button manually when can have the button pressed for them by
     * calling this function.
     * 
     * Interaction will act as if stop button has been pressed if this function
     * is called EXCEPT we will also be tracking the gaze of the user now
     * so we can show it back to them.
     */
    public void force_start()
    {
        // start tracking where the user is looking so we can develop a heat map of their gaze
        heatmap_wallpaper.SetActive(false);
        clicked = true;

        // begin the next stage of the interaction when tablet updates
        StartCoroutine(updating_process());
    }

    /*
     * Begins the process of the tablet "updating"
     * 
     * During this period the user will be forced to sit and wait and look at the screen
     * while the tablet updates...
     * This will cause the users gaze to wander and look at different things on the screen
     * allowing for a better gaze heat map.
     */
    private IEnumerator updating_process()
    {
        // inform the user what is happening
        ipad_text.text = "Good Morning!";
        yield return new WaitForSeconds(2); // wait
        ipad_text.text = "Updating.";

        // if we are not in demo mode add some story subtitles
        if (!demo_mode) { subtitles.text = "Updating...? Really..? now..?"; }
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating..";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating...";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating.";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Update Complete!";

        // if we are not in demo mode add some story subtitles
        if (!demo_mode) { subtitles.text = "Finally!"; }
        yield return new WaitForSeconds(1); // wait

        // if we are in demo mode
        // show the user the gaze tracking data that was stolen
        if (demo_mode)
        {
            // Load the specified heatmap when the object is clicked
            heatmap_wallpaper.SetActive(true);
            heatmap.GetComponent<AT_heatmap_generator>().show_heat_map();

            // tell the user this data was stolen and wait a bit
            subtitles.text = "<color=red>[EYE TRACKING DATA STOLEN]</color>";
            yield return new WaitForSeconds(5); // wait
            subtitles.text = "";
        }
        // if we are not in demo mode just progress story
        // user should be unaware of gaze tracking
        else
        {
            // progress story
            // communitcate back to story
            player.GetComponent<story_controller>().alarm_off();
        }
        // inform the tablet that the update is finished
        tablet.GetComponent<tablet_controller>().update_done();
        yield return null;
    }
}
