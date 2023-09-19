using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class notification_controller : MonoBehaviour
{
    public TextMeshProUGUI notif_text;

    public GameObject player;
    
    // items
    private int num_items;
    public GameObject shirt_icon;
    public GameObject laptop_icon;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_notif(string message)
    {
        Debug.Log("notif set");
        gameObject.SetActive(true);
        notif_text.text = message;
    }

    public void remove_notif()
    {
        Debug.Log("ntoif removed");
        gameObject.SetActive(false);

    }


    /// <summary>
    ///  get camping item game
    /// </summary>
    public void create_items_notif()
    {
        Debug.Log("camping notif set");
        gameObject.SetActive(true);
        notif_text.text = "Collect:";

        ////// set this
        num_items = 2;
        // turn all the items on
        shirt_icon.SetActive(true);
        laptop_icon.SetActive(true);



    }

    public void got_item(string item_name)
    {

        if (item_name == "shirt")
        {
            shirt_icon.SetActive(false);
            num_items--;
        }
        if (item_name == "laptop")
        {
            laptop_icon.SetActive(false);
            num_items--;
        }


        if (num_items == 0)
        {
            remove_notif();

            // send message to progess story
            player.GetComponent<story_controller>().got_all_items();
        }
    }
}
