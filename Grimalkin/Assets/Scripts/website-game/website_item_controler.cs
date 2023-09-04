using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class website_item_controler : MonoBehaviour   
{
    public TextMeshPro item_price;
    public GameObject sold_banner;
    public GameObject shopping_list_icon;

    int inital_price = 100;

    int current_price;
    int price_increase = 0;
    bool item_sold = false;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: remove ?
        shopping_list_icon.SetActive(true);


        current_price = inital_price;
        // unsold state
        sold_banner.SetActive(item_sold);
        item_price.text = current_price.ToString();
    }

    public void OnMouseOver()
    {
        if (!item_sold) 
        { // if the item is not sold yet, price gouge
            price_increase++; // increment the conter while the item is being looked at
        }
    }

    public void OnMouseExit()
    {
        if (!item_sold)
        { // if the item is not sold yet, price gouge
            current_price += price_increase;
            item_price.text = (current_price).ToString();
            price_increase = 0; // as soon as you look away price jumps up
            Debug.Log("New Price: " + current_price);
        }

       
    }

    public void OnMouseDown() {
        item_sold = true;
        price_increase = 0;
        sold_banner.SetActive(item_sold);

        // set the shopping list item as bought
        // if it is on the shopping list
        if (shopping_list_icon) 
        {
            shopping_list_icon.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
