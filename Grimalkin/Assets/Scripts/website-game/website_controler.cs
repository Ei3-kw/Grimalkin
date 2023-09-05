using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class website_controler : MonoBehaviour
{
    public int num_items_to_buy;

    public GameObject item_prefab;
    public Sprite[] possible_items;
    public Transform[] item_slots;
    public GameObject shopping_list;

    // Start is called before the first frame update
    void Start()
    {
        refill_items();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void register_item_sold(int key)
    {
        shopping_list.GetComponent<shopping_list_controler>().remove_item(key);
    }


    public void refill_items() 
    {
        // choose N random items from possible_items
        var chosen_items = new Sprite[item_slots.Length];

        for (int i = 0; i < item_slots.Length; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, possible_items.Length);
            chosen_items[i] = possible_items[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            possible_items[take] = possible_items[i];
            possible_items[i] = chosen_items[i];
        }


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
            item = Instantiate(item_prefab, item_slot.position, item_slot.rotation) as GameObject; // instatitate 

            // get the sprite renderer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            spriteRenderer.sprite = item_chosen;

            // set the location 'key' of the item
            item.GetComponent<website_item_controler>().set_location_key(i);
            item.GetComponent<website_item_controler>().set_website(gameObject);

        }





        // set up shopping list
        // choose N random items from chosen_items
        var to_buy = new Sprite[num_items_to_buy];
        var keys = new int[num_items_to_buy];

        for (int i = 0; i < num_items_to_buy; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, chosen_items.Length);
            to_buy[i] = chosen_items[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            chosen_items[take] = chosen_items[i];
            chosen_items[i] = to_buy[i];
            keys[i] = take;
        }


        shopping_list.GetComponent<shopping_list_controler>().refill_shopping_list(keys, to_buy);


    }

}
