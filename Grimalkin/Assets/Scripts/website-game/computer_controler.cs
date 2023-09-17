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
    public GameObject optional_UI;

    // UI to adjust currency
    public GameObject currency_UI;





    public int buffer_timer_between_interactios;

    // Start is called before the first frame update
    void Start()
    {
        start_game_button.SetActive(true);
        start_text_message.SetActive(false);
        end_game_screen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        // player wants to exit the game
        if (player_can_quit && Input.GetKeyDown("e"))
        {
            start_text_message.SetActive(false);
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
            optional_UI.SetActive(true);

            player_can_start = true;
            player_can_quit = false;
        }
    }


    // Will be called when the opbject is being looked at by the player
    public void look_at()
    {
        // if the user is not in game yet
        // and the user has triggered the game to start
        if (player_can_start && Input.GetKeyDown("e")) // TODO: check if user is in range of computer
        {
            // once player starts for the first time glow ends
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
            start_text_message.SetActive(true);

            player_can_quit = false;
            player_can_start = false;

            // disable the movment script and UI
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);

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
        int final_cost = total_inital_cost + total_extra_paid;
        checkout_text.text = $"Shopping Cart Checkout\n\nOriginal Cost: <color=green>${total_inital_cost} </color>\nGaze Interest Fee: <color=red>+${total_extra_paid} </color>\nTotal Cost: <color=red>${final_cost} </color>";

        // adust money and credits
        currency_UI.GetComponent<currency_controler>().change_money(-final_cost);
        currency_UI.GetComponent<currency_controler>().change_credits(3);

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











