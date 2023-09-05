using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer_controler : MonoBehaviour
{
    public GameObject website_game_prefab;
    public GameObject start_game_button;
    public Transform game_location;

    private GameObject current_website_game;

    // Start is called before the first frame update
    void Start()
    {
        start_game_button.SetActive(true);
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

    public void end_game()
    {
        Destroy(current_website_game);

        // make button come back
        start_game_button.SetActive(true);       
    }
}
