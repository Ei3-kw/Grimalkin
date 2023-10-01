using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_game_obj_controller : MonoBehaviour
{

   
    private int next_slide = 0;
    private bool in_slides = false;

    public GameObject player;
    public GameObject optional_UI;
    public GameObject currency_UI;

    public GameObject ss_screen;
    public GameObject ss_press_e_text;


    public GameObject[] slides;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if we go to the next slide
        if (in_slides && Input.GetKeyDown("e"))
        {
            // if there are no more slides left
            if (next_slide == slides.Length)
            {
                exit_ss();
            }
            else
            {
                // progress to next slide
                // turn off prev
                slides[next_slide-1].SetActive(false);
                // turn on the next slide
                slides[next_slide].SetActive(true);
                next_slide++;
            }
        }
    }

    private void exit_ss()
    {
        slides[slides.Length - 1].SetActive(false); // turn off last slide
        in_slides = false;

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        currency_UI.SetActive(true);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(false);
        // Press [e] to continue
        ss_press_e_text.SetActive(false);
    }

    // Will be called when the opbject is being looked at by the player
    public void look_at()
    {
        // if the user is not in game yet
        // and the user has triggered the game to start
        if (Input.GetKeyDown("e")) // TODO: check if user is in range
        {
            in_slides = true;

            // disable all player controls and excess UI
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);
            currency_UI.SetActive(false);

            // pop up slide show screen (mosly opaque screen with text)
            // can still kinda see background
            ss_screen.SetActive(true);
            // Press [e] to continue
            ss_press_e_text.SetActive(true);

            // turn on the first slide
            slides[0].SetActive(true);
            next_slide = 1;
        }
    }


}
