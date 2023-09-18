using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffee_controller : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Will be called when the opbject is being looked at by the player
    public void look_at()
    {
        // if the user is not in game yet
        // and the user has triggered the game to start
        if (Input.GetKeyDown("e")) // TODO: check if user is in range 
        {
            // once player starts for the first time glow ends
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
            gameObject.SetActive(false);

            // communitcate back to story
            player.GetComponent<story_controller>().got_coffee();
        }
    }
}
