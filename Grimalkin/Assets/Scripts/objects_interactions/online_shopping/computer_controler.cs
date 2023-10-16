/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control how the computer acts within the scene
 * - To respond to player when they player interacts with the computer
 * - To progress the story when the computer is relevant to the story
 * - To display and interact with the shopping website interaction
 * - And to show the start screen for this interaction
 * 
 * Attached to objects in game scene:
 * - Computer monitor object in scene
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class computer_controler : MonoBehaviour
{
    // if the online shopping game is to be played in "demo mode" which is the educational
    // mode at the end of the game where the gaze tracking is explained
    private bool demo_mode = false;

    // location where the website game should be on the computer screen
    // (where should the "browser window" go)
    public Transform game_location;

    // objects on computer screen before online shopping starts
    public GameObject start_game_button; // button to begin the shopping game
    public GameObject start_text_message; // text on the screen before user starts shopping

    public GameObject website_game_prefab; // prefab of the website game
    private GameObject current_website_game; // instance of the website game that is currently active (user is using)

    // objects on computer screen after online shopping ends
    public GameObject end_game_screen; // splash screen for cart checkout
    public TextMeshPro checkout_text; // informs user how much they paid etc.
    public GameObject gaze_price_text; // informs the user how their gaze was tracked

    // player related variables and objects
    public GameObject player; // the player object in the scene
    public GameObject player_cam; // the main player camera that the user is looking through
    public Transform camera_pos_for_game; // location where the camera should be to play online shopping game
    public Transform player_cam_pos; // old cam pos before game

    // UI Elements
    public GameObject optional_UI; // UI elements that are optional and should be off when interacting with computer
    public GameObject notifcations; // UI to adjust notifications

    /*
     * Start is called before the first frame update
     * 
     * Will set the computer up read to be played
     */
    void Start()
    {
        // turn on since the user hasnt started game yet
        start_game_button.SetActive(true);

        // we will only turn this on when the usee has turned the computer 'on'
        start_text_message.SetActive(false);

        // we only want this when the shopping is over
        end_game_screen.SetActive(false);
    }

    /*
    * Will be called when:
    * - The object is being looked at by the player
    * - And the player is allowed to interact with the object
    * - (i.e. in correct story stage to interact)
    */
    public void look_at()
    {
        // if the user wants to interact with the object
        if (Input.GetKeyDown("e"))
        {
            // remove task notifaction
            notifcations.GetComponent<notification_controller>().remove_notif();

            // once player starts interacting for the first time glow ends
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
            start_text_message.SetActive(true);

            // disable the movment script and UI since we are looking at computer
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);

            // move the camera into position
            // Calculate the position to move the camera to
            Vector3 targetPosition = camera_pos_for_game.position;
            Quaternion targetRotation = camera_pos_for_game.rotation;
            Transform camera = player_cam.GetComponent<Transform>();

            // Move the camera's position to the target position
            camera.position = targetPosition; // set new position
            camera.rotation = targetRotation; // set new rotation

            // unlock the cursor so the user can click on items on screen
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /*
     * Will be called when the "demo version" of the game should begin
     * 
     * It will set up the game but in a slighlty differnt way so the demo 
     * works correctly
     */
    public void start_demo()
    {
        // ensure that the game keeps track that we are playing in demo mode
        demo_mode = true;

        // once player starts for the first time glow ends
        gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
        start_text_message.SetActive(true);

        // disable the movment script and UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // move the camera into position
        // Calculate the position to move the camera to
        Vector3 targetPosition = camera_pos_for_game.position;
        Quaternion targetRotation = camera_pos_for_game.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // Move the camera's position to the target position
        camera.position = targetPosition; // set new position
        camera.rotation = targetRotation; // set new rotation

        // unlock the cursor so the user can click on items on screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /*
     * Start the "online shopping game"
     * 
     * i.e. bring up the online shopping website and allow them to purcahse items etc.
     */
    public void start_game()
    {
        // make start game button go awawy
        start_game_button.SetActive(false);
        start_text_message.SetActive(false);

        // start the game
        current_website_game = Instantiate(website_game_prefab, game_location.position, game_location.rotation, this.transform);
        // make sure the website keeps track of the computer
        current_website_game.GetComponent<website_controler>().set_computer(gameObject);
    }

    /*
     * End the "online shopping game"
     * 
     * i.e. remove up the online shopping website and show user a checkout screen
     * describing how much money they have spent
     * 
     * total_inital_cost: price that everything you bought would have cost BEFORE gaze tracking
     *                    surcharge
     *                    
     * total_extra_paid: Amout of extra money paid due to gaze tracking surcharge (looking at items more)
     */
    public void end_game(int total_inital_cost, int total_extra_paid)
    {
        // destory the game "remove the website from the screen"
        Destroy(current_website_game);

        // show end of game screen showing the user the checkout screen
        end_game_screen.SetActive(true);
        int final_cost = total_inital_cost + total_extra_paid;
        if (demo_mode)
        { //  if we are in demo mode show the user info about how much extra they had to pay due to gaze tracking
            checkout_text.text = $"Shopping Cart Checkout\n\nOriginal Cost: <color=green>${total_inital_cost} </color>\nGaze Interest Fee: <color=red>+${total_extra_paid} </color>\nTotal Cost: <color=red>${final_cost} </color>";
            gaze_price_text.SetActive(true);
        }
        else
        { // if we are not in demo mode all we show is the final price (User is not told about gaze tracking)
            gaze_price_text.SetActive(false);
            checkout_text.text = $"Shopping Cart Checkout\n\nTotal Cost: <color=green>${final_cost} </color>";
        }
    }

    /*
     * re open the 'start screen' i.e. the blank computer screen and then
     * exit the computer.
     * 
     * This should be called after the user chooses to quick to desktop. as after
     * the online shopping is done we want the user to progress with the story.
     */
    public void open_start_screen()
    {
        // remove end game screen "checkout"
        end_game_screen.SetActive(false);

        // make button come back that were present on the desktop screen
        start_game_button.SetActive(true);
        start_text_message.SetActive(true);

        // exit the gameback into the 3d enviroment
        start_text_message.SetActive(false);
        // relock the cursor (can no longer see pointer)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // move the camera back into player
        // Calculate the position to move the camera to
        Vector3 targetPosition = player_cam_pos.position;
        Quaternion targetRotation = player_cam_pos.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // move the camera's position to the target position
        camera.position = targetPosition; // back to old position
        camera.rotation = targetRotation; // back to old rotaion

        // re enable the movment script and UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);

        if (demo_mode) // if we are only playing a demo
        {
            // turn the glow back on
            // since user is allowed to replay the demo as much as they want
            gameObject.GetComponent<Outline>().enabled = true; 
        }
        else // if we are in the main story
        {
            // communitcate back to story that the online shopping has been done
            player.GetComponent<story_controller>().finished_website_game();
        }
    }
}
