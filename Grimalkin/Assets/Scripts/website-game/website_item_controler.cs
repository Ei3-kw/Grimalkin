using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class website_item_controler : MonoBehaviour   
{
    public TextMeshPro item_price;
    public GameObject sold_banner;
    public GameObject website;

    int inital_price = 100;
    int location_key = -1;

    int current_price;
    int price_increase = 0;
    bool item_sold = false;

    // Start is called before the first frame update
    void Start()
    {


        current_price = inital_price;
        // unsold state
        sold_banner.SetActive(item_sold);
        item_price.text = current_price.ToString();
    }

    public void set_website(GameObject given_website) 
    {
        website = given_website;
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
        }

       
    }

    public void OnMouseDown() 
    {
        if (!item_sold)
        {
            item_sold = true;
            price_increase = 0;
            sold_banner.SetActive(item_sold);

            // set the shopping list item as bought
            // communicate back to the website
            website.GetComponent<website_controler>().register_item_sold(location_key, inital_price, current_price);
        }       
    }

    // location of that item on the website
    public void set_location_key(int key)
    {
        location_key = key;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
