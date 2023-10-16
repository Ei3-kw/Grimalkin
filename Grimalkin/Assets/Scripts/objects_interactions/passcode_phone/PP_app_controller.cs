/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To allow the user to enter the pin interaction
 *   and if in demo mode show the user the gaze tracking data.
 * - will exit the 3D world and zoom in on the phone for the
 *   duration of the interaction
 * 
 * Attached to objects in game scene:
 * - Pin app on the users phone
 */

using TMPro;
using UnityEngine;

public class PP_app_controller : MonoBehaviour
{
    // the camera that the user looks through
    public Camera mainCamera;

    // the phone model
    public GameObject phoneObject;

    // the phone object that the pin will be displayed on
    public GameObject passcodePhone;

    // the position of the camera in the world before the iteraction started
    public static Vector3 orginalCameraPosition;

    // the camera position for the pin enter interaction
    public Transform newCamPos;

    // the player object in the scene
    public GameObject player;

    // UI elements that are not needed when entering the pin
    public GameObject optional_UI;

    // the message that is to be displayed to the user when confirming order
    public GameObject confirm_text;

    // the message that is to be displayed to the user on the app entered their pin
    public GameObject app_text;

    /*
     * Start will be called before the first frame update
     * 
     * We want the glow to be off when first created.
     * We only want the glow on when it is time to 
     * iteract with the phone in the story
     */
    private void Start()
    {
        // turn off glow
        gameObject.GetComponent<Outline>().enabled = false;
    }

    /*
     * Will be called when:
     * - The object is being looked at by the player
     * - And the player is allowed to interact with the object
     * - (i.e. in correct story stage to interact)
     */
    public void look_at()
    {   
        // if the user is not alreay within the phone
        // and they press e to interact
        if (!PP_phone_parameters.in2DView && Input.GetKeyDown("e"))
        {
            // since this can only happen in the main story, must not be demo mode
            confirm_text.GetComponent<PP_confirm_button_controller>().demo_mode = false;

            // ask the user to confirm the delivery
            app_text.GetComponent<TextMeshPro>().SetText("Confim your delivery");
            // turn off the glow when looked at it
            gameObject.GetComponent<Outline>().enabled = false; 

            // save the old camera position
            orginalCameraPosition = mainCamera.transform.position;

            // Move the camera above the phone and look at it.
            Vector3 desiredCameraPosition = new Vector3(newCamPos.position.x, newCamPos.position.y, newCamPos.position.z);            
            mainCamera.transform.position = desiredCameraPosition;
            mainCamera.transform.LookAt(phoneObject.transform.position);

            // we are now entering the pin
            PP_phone_parameters.in2DView = true;
            passcodePhone.SetActive(true);

            // disable all player controls and excess UI
            player.GetComponent<player_controller>().enabled = false;
            optional_UI.SetActive(false);

            // unlock the cursor so user can enter pin
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /*
     * Start the game in demo mode as opposed to the user
     * coming up and interacting
     * 
     * This version of the game is the exact same as the noraml version
     * but at the end of the game we show the use what gaze tracking data was stolen
     */
    public void start_demo()
    {
        confirm_text.GetComponent<PP_confirm_button_controller>().demo_mode = true;

        // ask the user to confirm the delivery
        app_text.GetComponent<TextMeshPro>().SetText("Confim your delivery");

        // save the old camera position
        orginalCameraPosition = mainCamera.transform.position;

        // Move the camera above the phone and look at it.
        Vector3 desiredCameraPosition = new Vector3(newCamPos.position.x, newCamPos.position.y, newCamPos.position.z);
        mainCamera.transform.position = desiredCameraPosition;
        mainCamera.transform.LookAt(phoneObject.transform.position);

        // we are now about to enter the pin
        PP_phone_parameters.in2DView = true; 
        PP_pin_parameters.codeIsSet = false; // code is no longer set as we will re enter it
        passcodePhone.SetActive(true);

        // disable all player controls and excess UI
        player.GetComponent<player_controller>().enabled = false;
        optional_UI.SetActive(false);

        // unlock the cursor so user can enter pin
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
