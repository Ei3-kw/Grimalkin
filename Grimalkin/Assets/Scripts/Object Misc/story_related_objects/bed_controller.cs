/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control how the bed acts within the scene
 * - To respond to player when they player interacts with the bed
 * - To progress the story when the bed is relevant to the story
 * 
 * Attached to objects in game scene:
 * - Bed object in scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bed_controller : MonoBehaviour
{
    // the player object within the game scene
    public GameObject player;

    /*
     * Start is called before the first frame update
     * 
     * Since the bed is always in the scene this will be run at the start of the game.
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
        // if they user wants to interact with the object
        if (Input.GetKeyDown("e"))
        {
            // once player interacts for the first time, object glow ends
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it

            // communitcate back to story that the bed has been interacted with
            player.GetComponent<story_controller>().in_bed_now();
        }
    }
}
