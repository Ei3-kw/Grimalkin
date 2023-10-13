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

public class camping_item : MonoBehaviour
{
    // the notification UI that will show the user what items are left to collect
    public GameObject notifs;
    // the name of the item the script is attached to
    public string item_name; 

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
            notifs.GetComponent<notification_controller>().got_item(item_name);
        }
    }
}
