using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class shopping_list_controler : MonoBehaviour
{

    public Transform[] item_slots;
    public GameObject sl_item_prefab;
    public GameObject website;

    // dict will store a refernce from the key location of the item 
    // to its represntaive key location on the shopping list

    // {item_location_key : shopping_list_location_key}
    private Dictionary<int, int> item_to_shopping_list = new Dictionary<int, int>();
    // {item_location_key : shopping_list_icon}
    private Dictionary<int, Sprite> item_to_icon = new Dictionary<int, Sprite>();
    // {item_location_key : shopping list icon object}
    private Dictionary<int, GameObject> item_to_sl_obj = new Dictionary<int, GameObject>();



    public TextMeshPro test;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void remove_item(int item_key)
    {
        // if the item sold was on the shopping list
        if (item_to_shopping_list.ContainsKey(item_key)) 
        {
            // remove that item from the screen
            item_to_shopping_list.Remove(item_key);
            item_to_icon.Remove(item_key);

            //destroy old item
            Destroy(item_to_sl_obj[item_key]);
            item_to_sl_obj.Remove(item_key);

            Debug.Log("sold item" + item_key);

            // redraw the new shopping list
            refill_shopping_list(item_to_shopping_list.Keys.ToArray(), item_to_icon.Values.ToArray());

            
        }
    }

    public void refill_shopping_list(int[] item_keys, Sprite[] item_icons) 
    {
        // destory all the old item
        foreach (GameObject sl_item in item_to_sl_obj.Values.ToArray())
        {
            Destroy(sl_item);
        }    


        for (int i = 0; i < item_keys.Length; i++)
        {
            // create the mapping
            // from item key to sl key
            item_to_shopping_list[item_keys[i]] = i;
            // from item key to icon
            item_to_icon[item_keys[i]] = item_icons[i];

            // select the item slot and the sprite
            Transform item_slot = item_slots[i];
            Sprite item_chosen = item_icons[i];


            // create the new item
            GameObject item;
            item = Instantiate(sl_item_prefab, item_slot.position, item_slot.rotation, this.transform); // instatitate 
            item_to_sl_obj[item_keys[i]] = item;

            // get the sprite renderer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            // display the item
            spriteRenderer.sprite = item_chosen;
        }

        // if there are no shopping list items left
        // we have finished the game
        if (!item_to_shopping_list.Any())
        {
            website.GetComponent<website_controler>().game_won();
        }
    }
}
