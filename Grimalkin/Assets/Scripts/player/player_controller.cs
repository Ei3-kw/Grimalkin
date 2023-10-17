/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the player throughout the game this will involve:
 * - Controling the camera as the player looks around (responding to mouse inputs)
 * - Communicating to the objects that the player is looking at
 * - Communicating to relevent UI's when the player preforms certain activities
 * - Controling the movement of the player (responding to player movement key inputs)
 * - Communicating to objects when the player attempts to interact with them
 *   through user inputs
 * 
 * Attached to objects in game scene:
 * - Player
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_controller : MonoBehaviour
{
    // General scene settings
    Vector2 screenSize; // size of screen user has
    public Vector2 pointerPos; // position where "gaze pointer" is pointing
    private string story_stage = "None"; // current part of the story we are in

    // General player movement control settings
    public CharacterController character; // the charcter object that will move around the scene
    public float camSens; // mouse sens (can be adjusted in game pause menu)
    public float moveSpeed; // how fast the player can walk around
    public float visMoveSpeed; // this is how fast the "simulated" eye movement moves
    public Vector2 visRatio; // how much the screen will move when user eye movement is registerd

    // Control settings related to where they player is looking
    public Transform head; // location of where the player camera is situated relative to the player
    public bool simVision = true; // if we wish to simulate gaze with mouse pointer
    public RectTransform visionPointer; // the pointer that is on the screen to show where player is looking

    // Camera settings and parameters
    public Camera cam; // the main camera that the player will see through
    public float rotationX; // x rotation of the camera
    public float rotationY; // y rotation of the camera
    RaycastHit hit; // the object that the player is currently looking at

    // Audio effects
    public AudioSource footStep; // when player moves
    private bool stepSoundOn = false; // if we are playing the foot step sound currently

    // Player and camera transform parameters
    Vector2 inputAxes; // axis that the player exists on
    Vector2 visInputAxes; // axis that the player camera exists on

    // UI elements
    // the gaze pointer is positioned on the screen (simulating where player is looking)
    public GameObject eye_pointer;
    public GameObject interaction_UI; // UI element that tells user if that can interact with an obj

    // Objects the player can interact with in the game
    public GameObject computer; // for website interaction
    public GameObject coffee_cup; // for start of game interaction
    public GameObject camping_wall_painting; // for start of game interaction
    public GameObject bed; // when player is going to sleep
    public GameObject tablet; // for heat map alarm interaction
    public GameObject passcode_phone; // for pin interaction
    public GameObject boxes; // for collecting packages

    // Items to pick up for camping during social media phone interaction
    public GameObject shirt;
    public GameObject laptop;
    public GameObject waterbottle;

    // Objects that will show up after the user has completed the main story 
    public List<GameObject> end_game_objs; // these are the "explain the interaction" objs


    /* 
     * Start is called before the first frame update
     * 
     * We wish to initalise the player to the starting settings
     * and ensure that all elements within the scene are ready
     * for when the user begins the game and can control the player
     * 
     */
    private void Start()
    {
        // record the users screen size
        screenSize = new Vector2(Screen.width,Screen.height);

        // set the gaze pointer to be in the exact middle of the screen
        pointerPos = screenSize/2;
        Cursor.lockState = CursorLockMode.Locked; // and lock it so it cannot move

        // turn the UI element off to start with (since we will not be looking at an object
        // we can interact with initally)
        interaction_UI.SetActive(false); 
    }

    /* 
     * Update is called every frame
     * 
     * We will do three main things each frame:
     * - Update the player position in response to the player movement inputs
     * - Update the position of the "gaze pointer" in response to the player inputs
     *   (and also their gaze once implemented)
     * - If the player is looking at an object, communicate to that object that the 
     *   player is looking at it
     */
    private void Update()
    {
        // if we are simulating vision with mouse position
        if (simVision)
        {
            // move the vision pointer to the new most position smoothly
            pointerPos.x = Mathf.Clamp(pointerPos.x + visInputAxes.x * visMoveSpeed * 100 * Time.deltaTime, 0, screenSize.x);
            pointerPos.y = Mathf.Clamp(pointerPos.y + visInputAxes.y * visMoveSpeed * 100 * Time.deltaTime, 0, screenSize.y);
            visionPointer.position = new Vector3(pointerPos.x ,pointerPos.y, 0 );
        }

        // record how much the mouse has moved in the set period of time
        // (and therefore how much we need to move the camera relative to set mouse sens)
        float mouseX = Input.GetAxis("Mouse X") * camSens * 1000 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * camSens * 1000 * Time.deltaTime;

        // determine how to rotate the camera in order to match the mouse movement
        rotationX -= mouseY ;
        // cap the camera movement to certain bounds to ensure that there is no crazy movement
        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
        rotationY = Mathf.Clamp(((pointerPos.x/screenSize.x) -0.5f)* visRatio.x, -30f, 30f);
        // move the head relative to the cameras new rotation
        head.localRotation = Quaternion.Euler(rotationX - Mathf.Clamp(((pointerPos.y/screenSize.y) -0.5f)* visRatio.y, -15f, 15f), rotationY, 0f );
        transform.Rotate(Vector3.up * mouseX);
        
        // Update what the new player position should be after this frames key presses
        UpdateAxes(); // get the current player inputs
        
        // get the expected change in position relative to the user inputs
        Vector3 move = inputAxes.x * transform.forward + inputAxes.y * transform.right;
        character.Move(move * moveSpeed * Time.deltaTime); // move the player

        // control the foot step sound effects
        if (move.magnitude > 0) { // if the player is moving
            if (!stepSoundOn){ // turn on the sound if not already on
                footStep.Play();
                stepSoundOn = true;
            }
        } else { // if the player is not moving
            if (stepSoundOn){ // turn sound off if not already off
                footStep.Stop();
                stepSoundOn = false;
            }
        }
        // update the players position after movement
        transform.position = new Vector3 (transform.position.x, 0, transform.position.z);

        // check what the player is looking at and record it
        // for each object, send the object a message that we are looking at it
        // for certain objects (e.g. interactiable objects) we will change
        // certain UI elements or trigger other interactions when looked at
        Ray looking_at = cam.ScreenPointToRay(pointerPos);

        // for each of the objects, we will first check if we are "allowed"
        // to interact with them (i.e. we are in the correct stage of the story)
        // if we are we can send them a message
        if (Physics.Raycast(looking_at, out hit))
        {
            ////////////////////////////////////////////////////////
            ///////////////////// computer /////////////////////////
            ////////////////////////////////////////////////////////
            if (story_stage == "website_game" && hit.collider.gameObject == computer) 
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to interact with computer";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<OS_computer_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            //////////////////////////////////////////////////////
            ///////////////////// coffee /////////////////////////
            //////////////////////////////////////////////////////
            else if (story_stage == "coffee" && hit.collider.gameObject == coffee_cup)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to drink coffee";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<coffee_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            ///////////////////////////////////////////////////
            ///////////////////// bed /////////////////////////
            ///////////////////////////////////////////////////
            else if (story_stage == "bed_time" && hit.collider.gameObject == bed)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] go to sleep";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<bed_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            //////////////////////////////////////////////////////
            ///////////////////// tablet /////////////////////////
            //////////////////////////////////////////////////////
            else if (story_stage == "wake_up" && hit.collider.gameObject == tablet)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] turn on tablet";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<tablet_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            /////////////////////////////////////////////////////
            ///////////////////// shirt /////////////////////////
            /////////////////////////////////////////////////////
            else if (story_stage == "getting_items" && hit.collider.gameObject == shirt)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to pick up clothes";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<camping_item_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            //////////////////////////////////////////////////////
            ///////////////////// laptop /////////////////////////
            //////////////////////////////////////////////////////
            else if (story_stage == "getting_items" && hit.collider.gameObject == laptop)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to pick up laptop";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<camping_item_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            ////////////////////////////////////////////////////////
            ///////////////////// water bottle /////////////////////
            ////////////////////////////////////////////////////////
            else if (story_stage == "getting_items" && hit.collider.gameObject == waterbottle)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to pick up water bottle";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<camping_item_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }

            ////////////////////////////////////////////////////////
            ///////////////////// end game objs ////////////////////
            ////////////////////////////////////////////////////////
            else if (story_stage == "end_game_interactions" && end_game_objs.Contains(hit.collider.gameObject))
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to interact";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<demo_object_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }

            /////////////////////////////////////////////////////
            ///////////////////// boxes /////////////////////////
            /////////////////////////////////////////////////////
            else if (story_stage == "find_boxes" && hit.collider.gameObject == boxes)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] check boxes";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<boxes_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
            ////////////////////////////////////////////////////////
            ///////////////////// passcode /////////////////////////
            ////////////////////////////////////////////////////////
            else if (story_stage == "ender_passcode" && hit.collider.gameObject == passcode_phone)
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] look at phone";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the object it is being looked at
                hit.collider.gameObject.GetComponent<PP_app_controller>().look_at();

                // set the eye pointer to be fully coloured to indicate an interactable object
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().color = newColor;
            }

            ////////////////////////////////////////////////////////
            ///////////////////// Other obj ////////////////////////
            ////////////////////////////////////////////////////////
            else // if they are not looking at any object of interest
            {
                interaction_UI.SetActive(false); // turn the UI element off

                // set the eye pointer to be hald coloured to indicate a NON interactable object
                Color newColor = new Color(1, 1, 1, 0.5f);
                eye_pointer.GetComponent<Image>().color = newColor;
            }
        }
        ////////////////////////////////////////////////////////
        ///////////////////// NO obj /////////////////////////
        ////////////////////////////////////////////////////////
        else // if they are not looking at any object at all
        {
            interaction_UI.SetActive(false); // turn the UI element off

            // set the eye pointer to be hald coloured to indicate a NON interactable object
            Color newColor = new Color(1, 1, 1, 0.5f);
            eye_pointer.GetComponent<Image>().color = newColor;
        }
    }

    /* 
     * Record the user inputs that in impact:
     * - Player Movement
     *    - W A S D
     * - "simulated gaze" Movement (only if eyes are not being tracked)
     *    - arrow keys 
     * 
     * And keep track of the expected changes for next frame update
     */
    private void UpdateAxes()
    {
        // Player movment
        inputAxes = Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            inputAxes.x += 1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputAxes.x -= 1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputAxes.y -= 1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputAxes.y += 1;
        }
        // "Simulated gaze" position on screen
        if (simVision){
                visInputAxes = Vector2.zero;
            if(Input.GetKey(KeyCode.UpArrow)){
                visInputAxes.y += 1;
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                visInputAxes.y -= 1;
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                visInputAxes.x -= 1;
            }
            if(Input.GetKey(KeyCode.RightArrow)){
                visInputAxes.x += 1;
            }
        }
        
    }

    /* 
     * Allow the story controller to communicate with the player
     * to tell the player what stage of the story they are in
     * i.e. so we know what objects the player is allowed to interact with
     */
    public void set_story_stage(string stage)
    {
        story_stage = stage;
    }
}
