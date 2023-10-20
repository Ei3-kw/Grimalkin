/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To allow the user to exit the pin interaction
 *   and if in demo mode show the user the gaze tracking data.
 * 
 * Attached to objects in game scene:
 * - Pin app's confirm button on the users phone
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PP_confirm_button_controller : MonoBehaviour
{
    // the camera that the user looks through
    public Camera myCamera;

    // the phone model
    public GameObject phoneObject;

    // the time in between cofirming the order, and the phone turning off
    public float textDisplayTime = 1.5f; 

    // the message that is to be displayed to the user when confirming order
    public TextMeshPro message;

    // the phone object that the pin will be displayed on
    public GameObject passcodePhone;

    // the passocde phone app that will run on the phone and require the pin
    public GameObject app;

    // the player object in the scene
    public GameObject player;

    // UI elements that are not needed when entering the pin
    public GameObject optional_UI;

    // the object that will show the user how their gaze was tracked
    public GameObject PP_gaze_displayer;

    // keep track of if we are in the educational demo version of the interaction
    public bool demo_mode = false;

    /*
     * When the confirm order button is pressed
     * 

     */
    private void OnMouseDown()
    {
        StartCoroutine(ShowTextAndMoveCamera());
    }

    /*
     * We should tell the user that their order is confirmed
     * (then if in demo mode show them how their eyes were tracked)
     * And finally exit the phone back to the 3D enviroment
     */
    private IEnumerator ShowTextAndMoveCamera()
    {
        // tell the user the order is confirmed on the phone screen
        message.SetText("Your delivery is <color=#005500>confirmed!</color>"); 
        yield return new WaitForSeconds(textDisplayTime); // Wait for the specified time
        
        // if we are in educational demo
        if (demo_mode)
        {
            // show the user how their eyes were tracked
            PP_gaze_displayer.GetComponent<PP_gaze_displayer>().demo_mode = true;
            PP_gaze_displayer.SetActive(true);
            app.SetActive(false);
        }
        // if we are in the main story
        // exit the user from the game back to the 3D world
        else 
        {
            // erase eye tracking data
            PP_pin_parameters.codeIsSet = false;
            PP_gaze_recorder eyePositionTracker = GameObject.Find("PP_gaze_recorder").GetComponent<PP_gaze_recorder>();
            eyePositionTracker.eyePositions.Clear();

            // move the camera back to original position
            myCamera.transform.position = PP_app_controller.orginalCameraPosition;
            myCamera.transform.LookAt(phoneObject.transform.position);
            passcodePhone.SetActive(false);
            app.SetActive(false);

            // re enable all player controls and excess UI
            player.GetComponent<player_controller>().enabled = true;
            optional_UI.SetActive(true);

            // back to 3D view
            PP_phone_parameters.in2DView = false;

            // lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<story_controller>().code_entered();
        }
    }
}
