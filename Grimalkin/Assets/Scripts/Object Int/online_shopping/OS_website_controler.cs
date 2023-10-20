/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the online shopping website
 * - To respond to user inputs that will affect the online shopping website
 * - To register when the online shopping starts and ends and make sure
 *   the nessary objects are contacted and communicated with when this happens
 * 
 * Attached to objects in game scene:
 * - Website object that is displayed on the computer
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class OS_website_controller : MonoBehaviour
{
    // number of items that we want the user to have to buy
    public int num_items_to_buy; 

    // an prefab of the website item
    public GameObject item_prefab;

    // A list of possible items that can be chosen to be displayed for purchase on the website
    public Sprite[] possible_items;

    // the locations that the items will go on the website screen
    public Transform[] item_slots;

    // the shopping list object that will display the items we need to purchase
    public GameObject shopping_list;

    // the computer that the website belongs to
    private GameObject computer;

    // variables to keep track of how much the user has spent $
    private int total_inital_cost; // how much the user has paid so far if there was no gaze tracking tax
    private int total_extra_paid; // the extra amount of money paid due to gaze tracking tax


    /*
     * Start is called before the first frame update
     * 
     * We wish to set up the website to be ready for the user to play
     */
    void Start()
    {
        // fill in the items on the website for the user to purchase
        // these items will be a random subset of the possible items
        refill_items();
    }

    /*
     * Register when an item has be sold / bought but the user and adjust the 
     * relavent variables and shopping list
     * 
     * key: is the location of the item on the website screen
     * 
     * inital_price: is the price of the item before gaze tracking tax
     * 
     * sold_price: is the price of the item including gaze tracking tax
     */
    public void register_item_sold(int key, int inital_price, int sold_price)
    {
        // keep track of totals
        total_inital_cost += inital_price; 
        total_extra_paid += sold_price - inital_price;

        // update the shopping list to ensure it is up to date with what we have purchased
        shopping_list.GetComponent<OS_shopping_list_controller>().remove_item(key);
    }

    /*
     * Fill up the shopping website with the items that the user can purchase.
     * These items will be a random subset of the possible items, and
     * they will be in a random order on the screen.
     * 
     * Meaning every time you play it will be different.
     */
    public void refill_items() 
    {
        /*
         * Choose random items from possible items to display for sale on website
         */

        // choose N random item locations from possible_items
        // MAKE SURE there is no double ups
        // choose the item locations
        var rnd = new System.Random();
        var items = Enumerable.Range(0, possible_items.Length).OrderBy(x => rnd.Next()).Take(item_slots.Length).ToArray();

        // save the chosen itmes
        var chosen_items = new Sprite[item_slots.Length];

        // for each of the items chosen 
        int location = 0;
        foreach (int item in items)
        {
            // record the expected position of that new item
            chosen_items[location] = possible_items[item];
            location++;
        }

        // for all all N items slots on the website
        // instantate each of the items in the correct pos setting the art to the 
        // respective item from possible_items
        for (int i = 0; i < item_slots.Length; i++)
        {
            // select the item slot and the sprite
            Transform item_slot = item_slots[i];
            Sprite item_chosen = chosen_items[i];

            // create the new item on the website
            GameObject item;
            item = Instantiate(item_prefab, item_slot.position, item_slot.rotation, this.transform); // instatitate 

            // get the sprite renderer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            spriteRenderer.sprite = item_chosen;

            // set the location 'key' of the item so that we can keep track of which item is where
            item.GetComponent<OS_website_item_controller>().set_location_key(i);
            item.GetComponent<OS_website_item_controller>().set_website(gameObject);
        }

        /*
         * Choose random items from the website to put onto shopping list
         */

        // set up shopping list
        // choose num_items_to_buy random items from chosen_items
        // choose the item locations
        var keys = Enumerable.Range(0, chosen_items.Length).OrderBy(x => rnd.Next()).Take(num_items_to_buy).ToArray();
        Sprite[] icons = new Sprite[num_items_to_buy];

        // from the item locations find the assocated sprites
        int icon_num = 0;
        foreach (int key in keys)
        {
            // store the sprite in same index as location
            icons[icon_num] = chosen_items[key];
            icon_num++;
        }

        // tell the shopping list what items we have chosen (tell shopping list the item locations
        // and thier respective icons)
        shopping_list.GetComponent<OS_shopping_list_controller>().refill_shopping_list(keys, icons);
    }

    /*
     * Will mark the online shopping as over and will quit the main website,
     * showing the user the checout screen.
     * 
     * Sould be called when all the items on the shopping list have been bought
     */
    public void game_won()
    {
        // tell computer that the online shoppping is over
        computer.GetComponent<OS_computer_controller>().end_game(total_inital_cost, total_extra_paid);
    }

    /*
     * Will record the computer object that the webiste is currently being displayed on
     */
    public void set_computer(GameObject given_computer)
    {
        // keep track of object
        computer = given_computer;
    }

}

 
