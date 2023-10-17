/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the tablet within the scene
 * - To control how the player interacts with this object an to take the nessary
 *   steps if the player does interact
 * - To control the alarm music that plays and have the tablet be the source
 *   of the sound
 * 
 * Attached to objects in game scene:
 * - The alarm tablet that will play the alarm interaction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablet_controller : MonoBehaviour
{
    // the position where we want the camera to play the tablet interaction
    // i.e. infront of tablet
    public Transform camera_pos_for_game;

    // the player camera the the user looks through
    public GameObject player_cam;

    // the current position of the player camera
    public Transform player_cam_pos;

    // the alarm object that plays the alarm music
    public GameObject alarm;

    // the stop button on the tablet that stops the alarm 
    public GameObject stop_button;

    // the "alarm app" that will display the alarm screen to the user
    public GameObject alarm_game;

    // optional UI that is not needed when in the interaction
    public GameObject optional_UI;

    // the player object in the scene
    public GameObject player;
    
    // if the alarm interation is in the end of game demo mode
    private bool demo_mode = false;

    /* 
     * Start is called before the first frame update
     * 
     * When the tablet is created we will have the alarm off
     * as the alarm should only turn on after wake up
     */
    void Start()
    {
        alarm_game.SetActive(false);
    }

    /*
     * Turn on the alarm music for the user to hear
     */
    public void turn_on_alarm()
    {
        alarm.SetActive(true);
    }

    /*
     * Will be called when:
     * - The object is being looked at by the player
     * - And the player is allowed to interact with the object
     * - (i.e. in correct story stage to interact)
     */
    public void look_at()
    {
        // if they user wants to interact with the object
        if (Input.GetKeyDown("e"))
        {
            // The tablet is being turned on now as the user wishes to interact
            StartCoroutine(turn_on());
        }
    }

    /*
     * Will start the end of game demo version of the interaction.
     * 
     * Same as the noraml version but we show the user what gaze tracking data was 
     * stolen (via a heat map)
     */
    public void start_demo()
    {
        demo_mode = true;

        // turn on mouse tracking
        AT_mouse_tracker.StartTracking();
        StartCoroutine(turn_on());
    }

    /*
     * should be called when we interact with the tablet
     * i.e. we are wanting to turn off the alarm.
     * 
     * It will make the user face the tablet and begin the interaction.
     */
    private IEnumerator turn_on()
    {
        // once player starts for the first time glow ends
        gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it

        // disable the movment script and UI
        player.GetComponent<player_controller>().enabled = false;
        optional_UI.SetActive(false);

        // move the camera into position
        // Calculate the position to move the camera to
        Vector3 targetPosition = camera_pos_for_game.position;
        Quaternion targetRotation = camera_pos_for_game.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // move the camera's position to the target position
        camera.position = targetPosition; // new position
        camera.rotation = targetRotation; // new roation

        // unlock the cursor so that the user can click on buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(0.5f); // wait a bit for the "tablet to turn on"
        alarm_game.SetActive(true); // then turn on the tablet

        // if we are in demo mode then we force the interaction to start without
        // having to click the stop button
        if (demo_mode)
        {
            stop_button.GetComponent<StopButtonController>().force_start();
        }
        yield return null;
    }

    /*
     * Turn off the alarm and exit the tablet
     * 
     * This will return the user back to expore the 3d enviroment
     * now that the interaction has ended
     */
    private IEnumerator turn_off()
    {
        // turn off the alarm app
        alarm_game.SetActive(false);
        yield return new WaitForSeconds(0.5f); // wait for "tablet to turn off"
        alarm_game.SetActive(false);

        // exit the tablet
        // relock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // move the camera back into player
        // Calculate the position to move the camera to
        Vector3 targetPosition = player_cam_pos.position;
        Quaternion targetRotation = player_cam_pos.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // Move the camera's position to the target position
        camera.position = targetPosition; // change positon back to players old pos
        camera.rotation = targetRotation; // change roation back to players old roation

        // re enable the movment script and UI
        player.GetComponent<player_controller>().enabled = true;
        optional_UI.SetActive(true);

        // turn red glow back on to replay if desired if in demo mode
        if (demo_mode) { gameObject.GetComponent<Outline>().enabled = true; }
        yield return null;
    }

    /*
     * If the update is finished call this function.
     * 
     * It will tell the tablet to shut down since we have finished
     * interacting with it.
     */
    public void update_done()
    {
        StartCoroutine(turn_off());
    }
}
