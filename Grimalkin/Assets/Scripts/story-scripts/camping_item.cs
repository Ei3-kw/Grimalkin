using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camping_item : MonoBehaviour
{
    public GameObject notifs;
    public string item_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Will be called when the opbject is being looked at by the player
    public void look_at()
    {
        // if the user is not in game yet
        // and the user has triggered the game to start
        if (Input.GetKeyDown("e")) // TODO: check if user is in range 
        {
            // once player starts for the first time glow ends
            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it
            gameObject.SetActive(false);

            // communitcate back to story
            notifs.GetComponent<notification_controller>().got_item(item_name);
        }
    }
}
