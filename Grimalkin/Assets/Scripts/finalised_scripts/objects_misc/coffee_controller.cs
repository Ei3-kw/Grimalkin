/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control how the coffee cup acts within the scene
 * - To respond to player when they player interacts with the coffee cup
 * - To progress the story when the coffee cup is relevant to the story
 * 
 * Attached to objects in game scene:
 * - Coffee cup object in scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffee_controller : MonoBehaviour
{
    public GameObject player; // player object in scene

    /*
     * Start is called before the first frame update
     */
    void Start()
    {
        // turn off the glow since we only want bed to glow when story commands it 
        // at the start of the game not nessecary 
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
        // if the user wants to interact with the object
        if (Input.GetKeyDown("e"))
        {
            // once player interacts for the first time, object glow ends
            gameObject.GetComponent<Outline>().enabled = false; 
            // remove the coffee from the scene as the player "drinks it"
            gameObject.SetActive(false);

            // communitcate back to story that player has interacted with the coffee
            player.GetComponent<story_controller>().got_coffee();
        }
    }
}
