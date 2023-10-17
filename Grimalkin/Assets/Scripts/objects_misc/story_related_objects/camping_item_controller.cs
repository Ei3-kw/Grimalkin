/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - "camping items" are the items that are to be collected by the player
 *   in their quest to prepare for camping e.g. waterbottle, clothes, ...
 * - To control how the "camping items" acts within the scene
 * - To respond to player when they player interacts with the boxes
 * - To progress the story when the boxes are relevant to the story
 * 
 * Attached to objects in game scene:
 * - Camping items in scene that are to be collected by the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camping_item_controller : MonoBehaviour
{
    // the notification UI that will show the user what items are left to collect
    public GameObject notifs;
    // the name of the item the script is attached to
    public string item_name;

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

            // make the item disapear as it has been "picked up" by the player
            gameObject.SetActive(false);

            // communitcate back to notification UI that the item has been picked up
            // so the user knows what is left to collect
            notifs.GetComponent<UI_notification_controller>().got_item(item_name);
        }
    }
}
