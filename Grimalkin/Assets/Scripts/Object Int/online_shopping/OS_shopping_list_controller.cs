/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To show the user what items they need to be (still need to buy) from
 *   the online shopping website to finish the game
 * - while also describing how to play the game in basic detail
 * 
 * Attached to objects in game scene:
 * - shopping list object that is displayed during online shopping game
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OS_shopping_list_controller : MonoBehaviour
{
    // the possible locations that items can be on the shopping list
    public Transform[] item_slots;

    // a prefab of what each shopping list item should be
    public GameObject sl_item_prefab;

    // the website that the shopping list exits in
    public GameObject website;

    // dict will store a refernce from the key location of the item on the website
    // to its represntaive key location on the shopping list
    // {item_location_key : shopping_list_location_key}
    private Dictionary<int, int> item_to_shopping_list = new Dictionary<int, int>();

    // dict will store a refernce from the key location of the item on the website
    // to its represntaive icon OBJECT on the shoppin list
    // {item_location_key : shopping_list_icon}
    private Dictionary<int, Sprite> item_to_icon = new Dictionary<int, Sprite>();

    // dict will store a refernce from the key location of the item on the website
    // to its represntaive icon SPRITE on the shoppin list
    // {item_location_key : shopping list icon object}
    private Dictionary<int, GameObject> item_to_sl_obj = new Dictionary<int, GameObject>();

    /*
     * Remove an item from the shopping list.
     * 
     * This should be called after the respective item has been purchased
     * by the using during online shoppping
     * 
     * item_key: is the location of the item on the ONLINE SHOPPING WEBSITE
     *           (NOT the location on the shopping list)
     */
    public void remove_item(int item_key)
    {
        // if the item sold was on the shopping list
        if (item_to_shopping_list.ContainsKey(item_key)) 
        {
            // remove that item from the relavent shopping list maps
            // as we no longer want it existing anywhere
            item_to_shopping_list.Remove(item_key);
            item_to_icon.Remove(item_key);

            // destroy the item that has now been bought (should no longer be on shopping list)
            Destroy(item_to_sl_obj[item_key]);
            item_to_sl_obj.Remove(item_key);

            // redraw the new shopping list with the old item now removed
            refill_shopping_list(item_to_shopping_list.Keys.ToArray(), item_to_icon.Values.ToArray());
        }
    }

    /*
     * Fill up the shopping list with the items specfied. This includes displaying the sprites
     * on the shopping list screen and recording the items within the shopping list object
     * 
     * item_keys: the item's locations on the WEBSITE should be a list of website item locations
     *            that coresspond to the items we want on the shopping list
     * 
     * item_icons: the sprite icons for the items we want on the shopping list
     * 
     * NOTE - the two lists should align (i.e. index 1 of item_keys should be pointing to the 
     * item on the website with the icon in index 1 of item_icons)
     */
    public void refill_shopping_list(int[] item_keys, Sprite[] item_icons) 
    {
        // destory all the old items in the shopping list to make way for new items
        foreach (GameObject sl_item in item_to_sl_obj.Values.ToArray())
        {
            Destroy(sl_item);
        }    

        // for each of the items to be added to the shopping list
        for (int i = 0; i < item_keys.Length; i++)
        {
            // create the mapping
            // from website item location to shopping list item location
            item_to_shopping_list[item_keys[i]] = i;
            // from website item location to icon 
            item_to_icon[item_keys[i]] = item_icons[i];

            // select the item slot and the sprite that the new item should go into
            Transform item_slot = item_slots[i];
            Sprite item_chosen = item_icons[i];

            // create the new shopping list item that should go into the slot
            GameObject item;
            item = Instantiate(sl_item_prefab, item_slot.position, item_slot.rotation, this.transform); // instatitate 
            item_to_sl_obj[item_keys[i]] = item; // keep track of the shopping list item object

            // get the sprite renderer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            // display the item on the shopping list
            spriteRenderer.sprite = item_chosen;
        }

        // if there are no shopping list items left
        // we have finished the game as we have bought all the items
        if (!item_to_shopping_list.Any())
        {
            // tell the website we got everything
            website.GetComponent<OS_website_controller>().game_won();
        }
    }
}
