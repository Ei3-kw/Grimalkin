using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class computer_controler : MonoBehaviour
{
    public GameObject website_game_prefab;
    public GameObject start_game_button;
    public Transform game_location;

    public GameObject end_game_screen;
    public TextMeshPro checkout_text;

    private GameObject current_website_game;

    // Start is called before the first frame update
    void Start()
    {
        start_game_button.SetActive(true);
        end_game_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_game()
    {
        // make button go awawy
        start_game_button.SetActive(false);

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
        checkout_text.text = $"Shopping Cart Checkout\n\nOriginal Cost: <color=green>${total_inital_cost} </color>\nGaze Interest Fee: <color=red>${total_extra_paid} </color>\nTotal Cost: <color=red>${total_inital_cost + total_extra_paid} </color>";

        //Debug.Log($"Game Over\nTotal Item Price: ${total_inital_cost}, Gaze Interest Fee: ${total_extra_paid}, Total Cost: ${total_inital_cost + total_extra_paid}");
    }

    public void open_start_screen()
    {

        Debug.Log("test");
        // remove end gaem screen
        end_game_screen.SetActive(false);

        // make button come back
        start_game_button.SetActive(true);
    }
}
