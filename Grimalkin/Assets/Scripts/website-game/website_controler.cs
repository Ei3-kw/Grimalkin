using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class website_controler : MonoBehaviour
{
    public int num_items_to_buy;

    public GameObject item_prefab;
    public Sprite[] possible_items;
    public Transform[] item_slots;
    public GameObject shopping_list;

    private GameObject computer;

    private int total_inital_cost;
    private int total_extra_paid;


    // Start is called before the first frame update
    void Start()
    {
        refill_items();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void register_item_sold(int key, int inital_price, int sold_price)
    {
        total_inital_cost += inital_price;
        total_extra_paid += sold_price - inital_price;
        shopping_list.GetComponent<shopping_list_controler>().remove_item(key);
    }


    public void refill_items() 
    {
        // choose N random item locations from possible_items

        // choose the item locations
        var rnd = new System.Random();
        var items = Enumerable.Range(0, possible_items.Length).OrderBy(x => rnd.Next()).Take(item_slots.Length).ToArray();


        Debug.Log(items);

        var chosen_items = new Sprite[item_slots.Length];

        // for each of the items chosen
        int location = 0;
        foreach (int item in items)
        {
            chosen_items[location] = possible_items[item];
            location++;
        }

        ////////// choose objects for webpage ///////////////
        // for all all N items slots on the website
        // instantate each of the items in the correct pos setting the art to the 
        // respective item from possible_items
        for (int i = 0; i < item_slots.Length; i++)
        {
            // select the item slot and the sprite
            Transform item_slot = item_slots[i];
            Sprite item_chosen = chosen_items[i];

            // create the new item
            GameObject item;
            item = Instantiate(item_prefab, item_slot.position, item_slot.rotation, this.transform); // instatitate 

            // get the sprite renderer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            spriteRenderer.sprite = item_chosen;

            // set the location 'key' of the item
            item.GetComponent<website_item_controler>().set_location_key(i);
            item.GetComponent<website_item_controler>().set_website(gameObject);

        }




        ////////// choose objects for shoppinglist ///////////////
        // set up shopping list
        // choose num_items_to_buy random items from chosen_items

        // choose the item locations
        var keys = Enumerable.Range(0, chosen_items.Length).OrderBy(x => rnd.Next()).Take(num_items_to_buy).ToArray();

        Sprite[] icons = new Sprite[num_items_to_buy];

        // from the item locations find the assocated sprites
        int icon_num = 0;
        foreach (int key in keys)
        {
            icons[icon_num] = chosen_items[key];
            icon_num++;
        }


        shopping_list.GetComponent<shopping_list_controler>().refill_shopping_list(keys, icons);
    }

    public void game_won()
    {
        computer.GetComponent<computer_controler>().end_game(total_inital_cost, total_extra_paid);

    }

    public void set_computer(GameObject given_computer)
    {
        computer = given_computer;
    }

}

 
