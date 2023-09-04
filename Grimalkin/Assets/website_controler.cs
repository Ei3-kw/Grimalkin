using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class website_controler : MonoBehaviour
{
    public GameObject item_prefab;
    public Sprite[] possible_items;
    public Transform[] item_slots;

    // Start is called before the first frame update
    void Start()
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
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
