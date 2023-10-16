/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control each of the items that are for sale on the online shopping website
 * - To register when they have been clicked "sold" and communicate to the 
 *   nessary objects when this happens.
 * - To display the correct sprite so the user knows which item they are buying
 * 
 * Attached to objects in game scene:
 * - Each of the items for sale on the online shopping website
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class website_item_controler : MonoBehaviour   
{
    // text that displays the current price of the item
    public TextMeshPro item_price;

    // banner that will appear above object to tell the user this item has be bought
    public GameObject sold_banner;

    // website object that the items are being displayed on
    public GameObject website;

    // icon of an eye that will display next to the item when the user is looking at it
    // to inform the user what they are gazing at
    public GameObject eye_icon;

    // banner sprite that can appear above the object to insentivise the user to buy
    // e.g. "Last One!" "Buy Now!"
    public GameObject discount_banner;

    // Number of frames that the user has to look at an item for in a row to trigger
    // the discount banner to show up
    public int price_inc_to_trigger_banner = 50;

    // list of possible "discount banners" that can show up above the item
    public Sprite[] banners;

    // the time aloted for the banner to fade in after it has veen triggered
    public float fade_out_time = 0.25f;
   
    // Item parameters
    int inital_price = 100; // the initall price of the item before "gaze tax"
    int current_price; // the current price of the item
    int price_increase = 0; // the amount the price has been increased above its inital price
    bool item_sold = false; // if the item has been sold
    bool discount_banner_displayed = false; // if a discont banner has been displayed yet for this item

    // the location of the item on the webpage
    int location_key = -1; // set to -1 as deafult but must be changed to be used

    /*
     * Start is called before the first frame update
     * 
     * We will initalise the item to its default state when created initally
     */
    void Start()
    {
        // the current price will start at the inital price
        current_price = inital_price;

        // set the banner to its unsold state since not sold yet
        sold_banner.SetActive(item_sold);

        // set alpha to 0 for the sale banner and make it disapear.
        Color newColor = new Color(1, 1, 1, 0);
        discount_banner.GetComponent<SpriteRenderer>().material.color = newColor;
        discount_banner.SetActive(discount_banner_displayed);
        
        // set the eye icon next to the object to false since we are not looking at it yet
        eye_icon.SetActive(false);

        // display the current price
        item_price.text = $"${current_price}.00";
    }

    /*
     * Set the website that the item is displayed on
     * 
     * given_website: the website we wish to the items owner to
     */
    public void set_website(GameObject given_website) 
    {
        website = given_website;
    }

    /*
     * When the user is simulated "looking at the item".
     * We simuate this with mouse over.
     * 
     * When this happens we will keep track of how long they are looking at the item.
     * And when they look a way we will increase the price proportional to how long
     * they have looked at the item for
     */
    public void OnMouseOver()
    {
        if (!item_sold) 
        { // if the item is not sold yet, price gouge
            eye_icon.SetActive(true); // we are looking at the item
            price_increase++; // increment the conter while the item is being looked at
        }
    }

    /*
     * When the user simulated "looks away from item"
     * We simulate this with mouse leaving the item
     * 
     * When this happens we will secretly increase the price proportional to how long
     * they have looked at the item for
     */
    public void OnMouseExit()
    {
        // no longer looking
        eye_icon.SetActive(false); // user is looking away
        if (!item_sold) // only increase the price if item is not sold yet
        { // wait a bit for the user to look away then increase the price
            Invoke("secrete_update_item", 0.5f);
        }
    }

    /*
     * Secretly increase the price of the item after the user looks away.
     * 
     * If the user looked at the item for long enough (showing very high interest)
     * We will also display a "discount type" banner to encourage them to buy!
     */
    public void secrete_update_item()
    {
        // update the new price after increase
        current_price += price_increase;
        item_price.text = $"${current_price}.00";

        // if there is no banner displayed yet, and user spent long time looking we can display it
        if (!discount_banner_displayed && price_increase > price_inc_to_trigger_banner)
        { 
            // select one of the banners at random
            int i = Random.Range(0, banners.Length);
            Sprite banner_chosen = banners[i];

            // get the sprite renderer
            SpriteRenderer spriteRenderer = discount_banner.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            spriteRenderer.sprite = banner_chosen;
            // turn on the banner
            discount_banner_displayed = true;

            // fade in the banner slowly
            StartCoroutine(fade_banner_to(1.0f, fade_out_time*4));
            discount_banner.SetActive(discount_banner_displayed);

        }
        price_increase = 0; // reset the price increase as the user is no longer looking at that item
    }

    /*
     * When the user clicks on the item.
     * 
     * This registers that the user wants to buy the item.
     * We will show the sold banner and record the item as sold.
     * We will then update the shopping list to register that the
     * item has been bought.
     */
    public void OnMouseDown() 
    {
        if (!item_sold) // if the item is not sold yet
        {
            // register item as sold
            item_sold = true;
            price_increase = 0;
            sold_banner.SetActive(item_sold);

            // communicate back to the website that that item has been sold
            website.GetComponent<website_controler>().register_item_sold(location_key, inital_price, current_price);
        }       
    }

    /*
     * Set the location of the item on the website. 
     * i.e. where it sits on the page
     * 
     * key: location of item on website (note the key is arbiatry and only refered interanlly)
     */
    public void set_location_key(int key)
    {
        location_key = key;
    }

    /*
     * Fade the "discount banner" to a specified alpha over a specified amount of time.
     * 
     * new_alpha: new alpha level we want to fade the banner to
     * 
     * time_to_fade: the period of time in which we want this fade to happen in
     */
    IEnumerator fade_banner_to(float new_alpha, float time_to_fade)
    { 
        // over the set period of time slowly increment the alpha level       
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time_to_fade) 
        { 
            // adjust the alpha level
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0f, new_alpha, t));
            discount_banner.GetComponent<SpriteRenderer>().material.color = newColor; 
            yield return null; 
        }
    }
}
