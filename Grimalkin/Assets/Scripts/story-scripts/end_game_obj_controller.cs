/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control how the interactable demo objects act within the
 *   "end game educational explaination" section of the game
 *   
 * - Each of these objects will have two purposes:
 *   1. To explain to the user the interaction that occured during the main part of the game
 *      and the reserach behind it
 *   2. To allow the use to enter a "behind the scenes" demo where the user can replay 
 *      the interaction but also see how their gaze tracking data was exploited along
 *      the way
 * 
 * Attached to objects in game scene:
 * - Demo objects in scene  apart of the "end game educational explaination" section of game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_game_obj_controller : MonoBehaviour
{
    // state variables relating to the current stage of the "infomation slide show"
    // the section where we explain the interaction and reserach behind that 
    // objects interaction within the game
    private int next_slide = 0; // the slide that we should show the user next
    private bool in_slides = false; // if the user is currerntly looking at the slides

    // objects that are explicitly used within the above mentioned slide show
    public GameObject ss_screen; // this is the overlay on screen that all the text will be displayed on
    public GameObject ss_press_e_text; // UI prompt to tell the user to press [e] to continue 
    public GameObject[] slides; // the slides (in order) that we wish to show the user for this interaction

    // settings related to the demo that we will show the user
    // replay of the game, but with a behind the scenes of how the gaze tracking works
    public bool has_demo = false; // if this object has a demo avaibale 
    public string obj_type = "None"; // the type of object i.e. what type of demo should we show
    public GameObject demo_obj; // the object that will preform the demo
    // the object that houses the object that will preform the demo
    // e.g. phone app is the demo obj, phone is the parent obj
    public GameObject demo_obj_parent; 
    public GameObject demo_text; // the text that will be displayed to the user as they play the demo
    
    public GameObject phone; // the social media phone the player can use
    public GameObject player; // the player object in the scene
    public GameObject optional_UI; // the optional UI elements in the scene

    // Update is called once per frame
    void Update()
    {
        // if we are on the last slide and press F
        // this means we want to begin the demo (if there is one)
        if (has_demo && next_slide == slides.Length && Input.GetKeyDown("f"))
        {
            exit_ss();

            // start the respective demo
            if (obj_type == "computer")
            {
                demo_obj.GetComponent<computer_controler>().start_demo();
            }
            else if (obj_type == "phone")
            {
                demo_obj.SetActive(true); // turn on the phone
                demo_obj.GetComponent<phoneCon>().start_demo();
                demo_text.SetActive(true); // turn on text to allow user to exit demo
            }
            else if (obj_type == "tablet")
            {
                demo_obj.GetComponent<tablet_controller>().start_demo();
            }
            else if (obj_type == "pin_phone")
            {
                demo_obj_parent.SetActive(false);
                demo_obj_parent.SetActive(true);
                demo_obj.GetComponent<Go2DView>().start_demo();
                demo_text.SetActive(true); // turn on text telling the user the pin
            }
        }

        // if we want to go to the next slide (press)
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
        next_slide = 1; // back to the start of the slide show
        slides[slides.Length - 1].SetActive(false); // turn off last slide
        in_slides = false;

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);

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
            phone.GetComponent<phoneCon>().end_demo();
            in_slides = true;

            // disable all player controls and excess UI
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);

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
