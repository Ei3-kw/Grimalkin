/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the "notification" UI
 * - The notification UI is a UI element that will appear in the top right corner
 *   of the screen throughout gameplay to remind the user of the next step
 *   they have to take to progress the story
 * - This script allows external scripts to communicate with the nofication UI
 *   to add and remove certain notifications when events happen in game
 * 
 * Attached to objects in game scene:
 * - Notification UI element
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_notification_controller : MonoBehaviour
{
    public TextMeshProUGUI notif_text; // the notification text that the user can read
    public GameObject player; // the player object that the user controls
    
    // The items that the player has to collect when preparing for camping
    private int num_items; // the number of items that we currently have to collect
    // icons for objects that we have to collect that will be shown in the notification
    // so the user remebers what we are looking for when "searching for camping items"
    public GameObject shirt_icon;
    public GameObject laptop_icon;
    public GameObject waterbottle_icon;

    /* 
     * Start is called before the first frame update
     * 
     * We first want to hide all the notification objects
     * As at the start of the game, there is initally no task for the user to preform
     */
    void Start()
    {
        gameObject.SetActive(false);
        shirt_icon.SetActive(false);
        laptop_icon.SetActive(false);
        waterbottle_icon.SetActive(false);
    }

    /* 
     * Set the notification text to be {message}
     * And show the "notification" to the user on the UI
     * 
     * message: the message of the task that we want to be as the notification
     */
    public void set_notif(string message)
    {
        gameObject.SetActive(true);
        notif_text.text = message;
    }

    /*
     * Remove the notification from the screen
     */
    public void remove_notif()
    {
        gameObject.SetActive(false);

    }

    /*
     * Creates a special type of notification for the "get camping items"
     * interaction which will inform the user to collect the 3 camping items
     * 
     * This will display the icons in the notification box, telling the user to collect them
     * As the user collects them, the icons will be removed from the box to 
     * signify that the item has been collected.
     */
    public void create_items_notif()
    {
        // turn the notification UI on and tell the user to collect the items
        gameObject.SetActive(true);
        notif_text.text = "Collect:";

        num_items = 3; // the number of items the user has to collect
        // turn all the item icons on to start with
        shirt_icon.SetActive(true);
        laptop_icon.SetActive(true);
        waterbottle_icon.SetActive(true);
        // the icons will be removed as the represented item is collected
    }

    /*
     * Signify to the notification UI that one of the camping items has been picked up
     * It will remove the item icon matching the {item_name} that is currently
     * on the notification UI.
     * 
     * item_name: the name of the item that has been collected, and therefore
     *            should be removed from the notification
     */
    public void got_item(string item_name)
    {
        // if the item collected is one of the recognised items
        // remove the icon, and the decrease the number of items left to collect
        if (item_name == "shirt")
        {
            shirt_icon.SetActive(false);
            num_items--;
        }
        else if (item_name == "laptop")
        {
            laptop_icon.SetActive(false);
            num_items--;
        }
        else if (item_name == "waterbottle")
        {
            waterbottle_icon.SetActive(false);
            num_items--;
        }

        // if all the items have now been collected
        if (num_items == 0) 
        {
            remove_notif(); // remove the notification task

            // send message to progess story as all items have been collected
            player.GetComponent<story_controller>().got_all_items();
        }
    }
}
