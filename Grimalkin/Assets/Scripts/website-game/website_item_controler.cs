using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class website_item_controler : MonoBehaviour   
{
    public int price_inc_to_trigger_banner = 75;
    public float fade_out_time = 0.25f;
    
    public TextMeshPro item_price;
    public GameObject sold_banner;
    public GameObject website;
    public GameObject eye_icon;
    public GameObject discount_banner;

    int inital_price = 100;
    int location_key = -1;

    int current_price;
    int price_increase = 0;
    bool item_sold = false;
    bool discount_banner_displayed = false;


    public Sprite[] banners;

    // Start is called before the first frame update
    void Start()
    {
        current_price = inital_price;
        // unsold state
        sold_banner.SetActive(item_sold);

        // set alpha to 0 for the sale banner
        Color newColor = new Color(1, 1, 1, 0);
        discount_banner.GetComponent<SpriteRenderer>().material.color = newColor;

        discount_banner.SetActive(discount_banner_displayed);



        eye_icon.SetActive(false);
        item_price.text = $"${current_price}.00";
    }

    public void set_website(GameObject given_website) 
    {
        website = given_website;
    }

    public void OnMouseOver()
    {
        if (!item_sold) 
        { // if the item is not sold yet, price gouge
            eye_icon.SetActive(true);
            price_increase++; // increment the conter while the item is being looked at
        }
    }

    public void OnMouseExit()
    {
        // no longer looking
        eye_icon.SetActive(false);
        if (!item_sold)
        { // if the item is not sold yet, price gouge
          // wait a bit for the user to look away
            Invoke("secrete_update_item", 0.5f);
        }

    }

    public void secrete_update_item()
    {
        current_price += price_increase;

        /// fade out
        //StartCoroutine(fade_out_price());
        item_price.text = $"${current_price}.00";
        /// fade in
        //StartCoroutine(fade_in_price());
  

        if (!discount_banner_displayed && price_increase > price_inc_to_trigger_banner)
        { // if there is no banner displayed yet, and user spent long time looking we can display it
            // select one of the banners at random

            int i = Random.Range(0, banners.Length);
            Sprite banner_chosen = banners[i];

            // get the sprite renderer
            SpriteRenderer spriteRenderer = discount_banner.GetComponent<SpriteRenderer>();
            // Assign the new sprite
            spriteRenderer.sprite = banner_chosen;

            discount_banner_displayed = true;

            // fade in the banner
            StartCoroutine(fade_banner_to(1.0f, fade_out_time*4));
            discount_banner.SetActive(discount_banner_displayed);

        }
        price_increase = 0; // as soon as you look away price jumps up
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




    private IEnumerator fade_out_price()
    {
        /////// fade out ////
        float duration = fade_out_time; //Fade out duration.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0.5f, currentTime / duration);
            item_price.color = new Color(item_price.color.r, item_price.color.g, item_price.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private IEnumerator fade_in_price()
    {
        /////// fade in ////
        float duration = fade_out_time; //Fade out duration.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0.5f, 1 - (currentTime / duration));
            item_price.color = new Color(item_price.color.r, item_price.color.g, item_price.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    IEnumerator fade_banner_to(float new_alpha, float time_to_fade)
    { 
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a; 
        
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time_to_fade) 
        { 
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0f, new_alpha, t));
            discount_banner.GetComponent<SpriteRenderer>().material.color = newColor; 
            yield return null; 
        }
    }
}
