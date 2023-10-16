/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To display to the user the gaze tracking dat that was stolen
 *   during the pin phone interaction
 * - Should be actiavted in demo mode where we want to show the user
 *   what data we have extracted
 * 
 * Attached to objects in game scene:
 * - Pin app on the users phone
 */

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class PP_gaze_displayer : MonoBehaviour
{

    // speed that sphere will move when showing user their gaze movements
    public float moveSpeed = 2f;

    // main camera within the scene
    public Camera mainCamera;

    // sphere that will follow the users gaze movements
    public GameObject sphere;

    // list of gaze positions gathered
    private List<Vector3> positions = new List<Vector3>();

    // the current position that the sphere is at
    private int currentPositionIndex = 0;

    // phone that the pin interaction is played on
    public GameObject phone;

    // player object in the scene
    public GameObject player;

    // UI elements that are not needed for phone interaction
    public GameObject optional_UI;

    // camera that the user looks through
    public Camera myCamera;

    // phone model used for interaction in scene
    public GameObject phoneObject;

    // phone game object within the scene
    public GameObject passcodePhone;

    // passcode app that is displayed on the phone
    public GameObject app;

    // if we are in the educational end game demo mode
    public bool demo_mode = false;

    // the subtites that display on the bottom of the screen
    public TextMeshProUGUI subtitles;

    // text that will be displayed to the use during the demo
    public GameObject demo_text;

    /*
     * Start will be called before the first frame update.
     * 
     * We will gather all the gaze tracking data.
     * and start to show the user where we tracked their eye location.
     */
    void Start()
    { 
        // The gaze tracking data recorded
        PP_gaze_recorder eyePositionTracker = GameObject.Find("PP_gaze_recorder").GetComponent<PP_gaze_recorder>();
        positions = eyePositionTracker.eyePositions;

        // start the sphere at the first gaze posisition
        currentPositionIndex = 0;
        sphere.SetActive(true);
    }

    /*
     * Update is called once per frame
     * 
     * We will move the sphere around the screen tracking where the 
     * user has been looking throughout the game.
     * 
     * Throughout the demo we will expalin to the user that this
     * is the eye tracking data that was stolen
     */
    void Update()
    {   
        // keep moving the sphere untill we have shown the user
        // all the gaze tracking data
        if (currentPositionIndex < positions.Count)
        {
            // explain to user that this is the data stolen
            subtitles.text = "<color=red>[EYE TRACKING DATA STOLEN]</color>";
            demo_text.SetActive(false); // turn off demo text

            // keep the eye tracking sphere on
            sphere.SetActive(true);

            // find new position to move the eye tracker to
            Vector3 posToMove = new Vector3(positions[currentPositionIndex].x - phone.transform.position.x, positions[currentPositionIndex].y - phone.transform.position.y, mainCamera.transform.position.z  - phone.transform.position.z);
            
            // calcuate the position to display on screen
            posToMove = mainCamera.ScreenToWorldPoint(posToMove);
            sphere.transform.position = posToMove;

            // onto the next position
            currentPositionIndex++;
        }
        else // recording done :) shown the user all the eye tracking data
        {
            StartCoroutine(quiting());
        }
    }

    /*
     * Takes the user through the quiting sequnce where they will exit the phone.
     * 
     * This should be called after all the gaze tracking data is shown
     */
    private IEnumerator quiting()
    {
        yield return new WaitForSeconds(2); // wait
        subtitles.text = "";

        // turn off the gaze tracking sphere
        sphere.SetActive(false);

        // move the camera back into position
        myCamera.transform.position = PP_app_controller.orginalCameraPosition;
        myCamera.transform.LookAt(phoneObject.transform.position);

        // turn off all the passcode interactions
        passcodePhone.SetActive(false);
        app.SetActive(false);

        // disable all player controls and excess UI
        player.GetComponent<player_controller>().enabled = true;
        optional_UI.SetActive(true);

        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // reset the pin phone ready for if the user wants to replay
        PP_phone_parameters.in2DView = false;
        PP_pin_parameters.codeIsSet = false;

        // reset the gaze locations
        PP_gaze_recorder eyePositionTracker = GameObject.Find("PP_gaze_recorder").GetComponent<PP_gaze_recorder>();
        eyePositionTracker.eyePositions.Clear();
        currentPositionIndex = 0;

        // turn self (pin app) as interaction is over
        gameObject.SetActive(false); 
        yield return null;
    }
}
