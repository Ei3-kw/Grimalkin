/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control how the boxes acts within the scene
 * - To respond to player when they player interacts with the boxes
 * - To progress the story when the boxes are relevant to the story
 * 
 * Attached to objects in game scene:
 * - Box object in scene (camping items that have arrived)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxes_controller : MonoBehaviour
{
    public GameObject player; // player object in scene
    // model for the closed boxes (not yet opened by player)
    public GameObject closed_boxes;
    // model for the open boxes (opened by player)
    public GameObject open_boxes;

    /*
     * Start is called before the first frame update
     * 
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
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it

            // open the boxes (i.e. swap out the model for the boces)
            closed_boxes.SetActive(false);
            open_boxes.SetActive(true);

            // communitcate back to story that the boxes have been interacted with
            player.GetComponent<story_controller>().opened_boxes();
        }
    }
}
