using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class computer_controler : MonoBehaviour
{
    public GameObject website_game_prefab;
    public GameObject start_game_button;
    public GameObject start_text_message;

    public Transform game_location;

    public GameObject end_game_screen;
    public TextMeshPro checkout_text;

    private GameObject current_website_game;

    private bool player_can_start = true;
    private bool player_can_quit = false;

    // controls for the player when entering the game
    public GameObject player;
    public GameObject player_cam;
    public Transform camera_pos_for_game; // new pos we want cam
    public Transform player_cam_pos; // old cam pos before game
    public GameObject player_canvas;





    public int buffer_timer_between_interactios;

    // Start is called before the first frame update
    void Start()
    {
        start_game_button.SetActive(true);
        start_text_message.SetActive(true);

        end_game_screen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        // if the user is not in game yet
        // and the user has triggered the game to start
        if (player_can_start && Input.GetKeyDown("e")) // TODO: check if user is in range of computer
        {
            player_can_quit = false;
            player_can_start = false;

            // disable the movment script and UI
            player.GetComponent<playerController>().enabled = false;
            player_canvas.SetActive(false);

            // move the camera into position
            // Calculate the position to move the camera to
            Vector3 targetPosition = camera_pos_for_game.position;
            Quaternion targetRotation = camera_pos_for_game.rotation;
            Transform camera = player_cam.GetComponent<Transform>();

            // Interpolate the camera's position toward the target position
            camera.position = targetPosition;
            camera.rotation = targetRotation;

            // unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player_can_quit = true;
        }

        else if (player_can_quit && Input.GetKeyDown("e"))
        { // player wants to exit the game

            // relock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            // move the camera back into player
            // Calculate the position to move the camera to
            Vector3 targetPosition = player_cam_pos.position;
            Quaternion targetRotation = player_cam_pos.rotation;
            Transform camera = player_cam.GetComponent<Transform>();

            // Interpolate the camera's position toward the target position
            camera.position = targetPosition;
            camera.rotation = targetRotation;

            // re enable the movment script and UI
            player.GetComponent<playerController>().enabled = true;
            player_canvas.SetActive(true);

            player_can_start = true;
            player_can_quit = false;
        }

        

    }



    public void start_game()
    {
        player_can_quit = false;
        // make button go awawy
        start_game_button.SetActive(false);
        start_text_message.SetActive(false);

        // start the game
        current_website_game = Instantiate(website_game_prefab, game_location.position, game_location.rotation, this.transform);
        // make sure the website keeps track of the computer
        current_website_game.GetComponent<website_controler>().set_computer(gameObject);
    }

    public void end_game(int total_inital_cost, int total_extra_paid)
    {
        // destory the game
        Destroy(current_website_game);

        // show end of game screnn
        end_game_screen.SetActive(true);
        checkout_text.text = $"Shopping Cart Checkout\n\nOriginal Cost: <color=green>${total_inital_cost} </color>\nGaze Interest Fee: <color=red>+${total_extra_paid} </color>\nTotal Cost: <color=red>${total_inital_cost + total_extra_paid} </color>";

        //Debug.Log($"Game Over\nTotal Item Price: ${total_inital_cost}, Gaze Interest Fee: ${total_extra_paid}, Total Cost: ${total_inital_cost + total_extra_paid}");
    }

    public void open_start_screen()
    {

        Debug.Log("test");
        // remove end gaem screen
        end_game_screen.SetActive(false);

        // make button come back
        start_game_button.SetActive(true);
        start_text_message.SetActive(true);

        player_can_quit = true;
    }
}
