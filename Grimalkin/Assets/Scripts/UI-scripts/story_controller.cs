/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the main story progession of the game
 * - It will communicate to other objects when needed within the story
 * - And other object will communicate with in when the story is to be progressed
 * - It will manage the progression to different stages of the story and 
 *   send messages to different UI feautres within the game when nessary
 * - The story will begin when the game starts
 * 
 * Attached to objects in game scene:
 * - Player object in scene (i.e. the user that will stay present throughout the whole game)
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class story_controller : MonoBehaviour
{
    // keep track of the current stage that the story is in
    private string story_stage;

    // FPS cap for game to ensure consistant behaviour and preformance
    public int targetFrameRate = 60;

    // skyboxes used throughout the game that will be displayed outside
    public Material night; // before player sleeps
    public Material morning; // after player sleeps and wakes up

    // the main player object within the scene (what the user controls)
    public GameObject player;

    // UI elements that need to be sent messages by the story
    public GameObject optional_UI; // UI elements that can be disabled if needed (not nessary to play)
    public GameObject subtitles; // the subtitles UI object that is displayed on the bottom of screen
    private TextMeshProUGUI subtitle_text; // the subtitles text that is displayed on screen
    public GameObject notifcations; // the notification UI element that is displayed
    public GameObject controls_intro; // screen displaying the introductory controls to the user

    // pause menu UI elements
    public GameObject pause_menu; // pause menu UI element
    public GameObject menu_exit_text; // text describing to the user how to use pause menu

    // start of the game UI and objects
    public GameObject start_game_text; // the text that will inform user how to start the game
    public GameObject fade_in_start; // object that will control the fade in as the world loads
    public Transform starting_pos; // the position the player will start the game in

    /*
     * Objects associated with each key interaction within the game
     * During different stages of the story, different objects will be created,
     * removed or modified to suit what is happening
     */
    // drink the coffee
    public GameObject coffee_cup; // the coffee cup object

    // buy items from online shopping website interaction
    public GameObject computer; // the computer mointor object that hosts the website

    // sleep in bed and sleep through the night
    public GameObject bed; // bed object
    // the screen fade object that will ensure that the screen fades to and from black when going to sleep
    public GameObject fade_obj;

    // tablet alarm interaction
    public GameObject tablet; // the tablet object that the alarm will be presented on

    // pick up camping items, and look at social media interaction
    public GameObject phone;
    public GameObject phone_packing_list;
    public GameObject[] camping_items;

    // enter passcode on phone to confirm delivery interaction
    public GameObject passcode_phone;
    public GameObject boxes;
    public GameObject door_bell_sound;

    /*
     * ending slide show settings and UI elemenets
     * the ending slide show is the set of slides that will play after the user has finished
     * the main game and is entering the "educational demo" part of the game
     */
    // parameters and general UI elements
    private bool in_ss = false; // keeps track of if we are in the ending slide show or not
    public GameObject ss_screen; // the black UI screen the the slides will be overlayed ontop of
    public GameObject ss_press_e_text; // text informing the user they can click e to continue

    // slides used within slide show
    public GameObject ss_ending_1; // slide 1
    public GameObject ss_ending_2; // slide 2
    public GameObject ss_ending_3; // slide 3 (this is a video showing the cameras in the house)
    public GameObject ss_ending_3_text; // the text overlayed on the video (from slide 3)
    public GameObject ss_ending_4; // slide 4
    private bool skipped_cutscene = false; // if we have skiped the "camera video" cut scene

    // sets of objects that will be changed in the transition from the first to second half of game
    public GameObject old_objs; // objects that are ONLY apart of the "main story - first half of game"
    public GameObject new_objs; // objects that are ONLY apart of the "end demos - second half of game"

    /*
     * Start is called before the first frame update
     * we will set up the scene to begin the game
     */
    void Start()
    {
        // don't fade in untill the user clicks [1] to start the game
        fade_in_start.SetActive(false);

        // Cap FPS to ensure consistant behaviour and preformance
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        // turn off any objects that are meant to be hidden at the start of the game
        phone.SetActive(false);
        boxes.SetActive(false);

        // define params that are to be used within the game
        subtitle_text = subtitles.GetComponent<TextMeshProUGUI>();

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;

        // turn off any UI elements that are not needed yet
        ss_screen.SetActive(false);
        ss_press_e_text.SetActive(false);
        optional_UI.SetActive(false);

        // turn on the start screen of the game
        set_story_stage("start_screen");
        start_game_text.SetActive(true);
    }

    /*
     * Update is called once per frame
     * 
     * We will check for any user inputs, if the user is within the correct 
     * story stage, and press a key that should progress the story, we will
     * act on it. If either, a key that doesn't do anything is pressed or the 
     * user is not in the correct story stage for that key to mean anything,
     * we do nothing.
     * 
     * We will also check for key presses that will bring up the menu screens
     * Or the user is in the start screen and wants to start the game
     */
    void Update()
    {
        // if we are in the start screen, i,e. are about to begin the game
        if (story_stage == "start_screen")
        {
            // if user wishes to start the game from the begining
            if (Input.GetKeyDown("1"))
            {
                // turn ON player controls and any UI elements needed for the game
                player.GetComponent<playerController>().enabled = true;
                optional_UI.SetActive(true);

                // move player to starting location
                Vector3 targetPosition = starting_pos.position;
                Quaternion targetRotation = starting_pos.rotation;
                Transform player_pos = player.GetComponent<Transform>();
                player_pos.position = targetPosition; // correct position
                player_pos.rotation = targetRotation; // correct rotation

                // turn off start game text
                start_game_text.SetActive(false);
                StartCoroutine(start_stage_1()); // begin the game from the start
            }
            // if the user wishes to skip to the end game educational demo 
            else if (Input.GetKeyDown("2"))
            {
                // move player to starting location
                Vector3 targetPosition = starting_pos.position;
                Quaternion targetRotation = starting_pos.rotation;
                Transform player_pos = player.GetComponent<Transform>();
                player_pos.position = targetPosition; // correct position
                player_pos.rotation = targetRotation; // correct rotation

                // turn off start game text
                start_game_text.SetActive(false);
                StartCoroutine(ending_ss_4()); // skip to end game demo scenes
            }
        }

        // if user wants to bring up the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // bring up the menu (this includes all the buttons etc.)
            pause_menu.SetActive(true);
        }

        // if they want to open their phone for the first time
        if (story_stage == "waiting_for_phone_open" && Input.GetKeyDown("e"))
        {
            StartCoroutine(start_stage_5()); // progress story
        }
        // if they want to open their phone for the second time
        if (story_stage == "waiting_for_socail_phone" && Input.GetKeyDown("e"))
        {
            StartCoroutine(start_stage_5_2()); // progress story
        }

        // if the user is within the slide show, and they wish to progress to the next slide
        if (in_ss && Input.GetKeyDown("e")) // press e to go to next slide
        {
            // depending on what slide they are currently on, move to the next one
            if (story_stage == "ending_ss_1_waiting") { StartCoroutine(ending_ss_2()); }
            else if (story_stage == "ending_ss_2_waiting") { StartCoroutine(ending_ss_3()); }
            else if (story_stage == "ending_ss_3_waiting") 
            { // if the user presses e to skip the cut scene 
                skipped_cutscene = true; // record they skiped it
                StartCoroutine(ending_ss_4()); 
            }
            else if (story_stage == "ending_ss_4_waiting") { StartCoroutine(ending_ss_end()); }
        }
    }

    /*
     * Set the current story stage.
     * 
     * We need to ensure that both the story controler and the player are on the 
     * same page about what story stage we are on. If we want to change the story
     * stage we must also inform the player
     * 
     * stage: name of the story stage we want to go into to
     */
    public void set_story_stage(string stage)
    {
        story_stage = stage;
        player.GetComponent<playerController>().set_story_stage(stage);

    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When coffee is consumed
     */
    public void got_coffee()
    {
        StartCoroutine(start_stage_3()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When all camping items have been gathered
     */
    public void got_all_items()
    {
        StartCoroutine(start_stage_6()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When the website game has been completed
     */
    public void finished_website_game()
    {
        StartCoroutine(start_stage_7()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When the user has gone to bed
     */
    public void in_bed_now()
    {
        StartCoroutine(start_stage_8()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When alarm has been turned off by the user
     */
    public void alarm_off()
    {
        StartCoroutine(start_stage_9()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When usre has opened the boxes at the door
     */
    public void opened_boxes()
    {
        StartCoroutine(start_stage_10()); // start next story stage after this event
    }

    /*
     * Is called by external objects within the scene when a particular event
     * happens that should progress the story
     * 
     * When the correct code has been entered on the phone
     */
    public void code_entered()
    {
        StartCoroutine(start_stage_11()); // start next story stage after this event
    }

    /*
     * Story stage within the game
     * 
     * This stage is the entry point for the main game
     * It will introduce the player to the story line
     * Show the user the controls and explain how the game works in general
     */
    private IEnumerator start_stage_1()
    {
        set_story_stage("start_dialog");

        // start with the player unable to move
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false); // turn off unnessary UI elements in intro
        passcode_phone.SetActive(false); // turn of pin phone to avoid confusion

        // fade the game in from black to start while assests load in
        fade_in_start.SetActive(true);
        yield return new WaitForSeconds(3); // wait while the screen fades


        // display first set of dialog to the user 
        subtitle_text.text = "Oh Uh... Our anniversary is this weekend...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I really need to plan something for that...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "But first, I would kill for a coffee";
        yield return new WaitForSeconds(2); // wait

        // describe to user the controls by showing them a controls info screen
        // they can now click [e] to exit the controls
        ss_screen.SetActive(true);
        ss_press_e_text.SetActive(true);
        controls_intro.SetActive(true);

        // only progress when click e to cotntinue
        while (!Input.GetKeyDown("e"))
        {
            yield return null; // keep waiting untill e pressed
        }

        // turn off the controls screen
        controls_intro.SetActive(false);
        ss_screen.SetActive(false);
        ss_press_e_text.SetActive(false);

        // re enable the movment script and UI so the player can move and look around
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        yield return new WaitForSeconds(2); // wait

        // progress to next stage of game
        subtitle_text.text = "";
        StartCoroutine(start_stage_2()); // progress to collect coffee cup
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Get coffee from bench
     *
     * Will be triggered automatically after previous story stage
     * 
     * This stage will be amost a small "tutorial" on how to play
     * by describing how we will show objectives within the game
     */
    private IEnumerator start_stage_2()
    {
        set_story_stage("coffee");

        // pop up task notifaction telling the user what to do
        notifcations.GetComponent<notification_controller>().set_notif("Go grab some coffee from the bench");

        // turn on glow for coffee
        coffee_cup.GetComponent<Outline>().enabled = true; // turn off the glow when looked at it
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Think about camping after drinking coffe
     *
     * Will be triggered after:
     * - player drinks the coffee
     * 
     * Will describe the main goal within the rest of the story,
     * to prepare for camping
     */
    private IEnumerator start_stage_3()
    {
        // coffee has been clicked on
        set_story_stage("after_coffee");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // begin dialog in form of subtitles
        subtitle_text.text = "Ahh, that's better";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Oh!! I have an idea for the anniversary!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Camping!!";
        yield return new WaitForSeconds(2); // wait

        StartCoroutine(start_stage_4());
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Remeber to look at phone to see items for camping
     *
     * Will be triggered automatically after previous story stage
     * 
     * Will inform the user that they have to open their phone to continue
     */
    private IEnumerator start_stage_4()
    {
        set_story_stage("remembered_camping");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // begin dialog in form of subtitles
        subtitle_text.text = "I have a list of items I need to pack saved on my phone";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Press [e] to open your phone";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Press [e] to open you phone and check packing list");

        // wait for user input to open phone
        set_story_stage("waiting_for_phone_open");
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Looking at phone to determin what items we need to pick up for camping
     *
     * Will be triggered after:
     * - player opens phone to look at camping packing list
     * 
     * Will display the items the user needs for camping and how to collect them
     * This will make the user move around the unit to search for the items
     */
    private IEnumerator start_stage_5()
    {
        set_story_stage("phone_opened");

        // open packing list on phone infront of face
        phone_packing_list.SetActive(true);

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // begin dialog in form of subtitles
        subtitle_text.text = "Ahh yes this is the list of stuff I need to pack!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I just need to find these items around the house and pick them up";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I can look at photos of the camp site while I collect the items!";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Press [e] to open social media";

        // wait for user input to open social media
        set_story_stage("waiting_for_socail_phone");
        yield return null;
    }

    /*
     * Story stage within the game:
     * - after user opens their phone to look at social media and how has to 
     *   search around for camping items
     *
     * Will be triggered after:
     * - after user opens their phone to look at social media
     * 
     * The user now has to explore their house to collect the items for camping
     */
    private IEnumerator start_stage_5_2()
    {
        set_story_stage("open_socail_phone");

        // close packing list on phone infront of face
        phone_packing_list.SetActive(false);

        // open social media phone that will be displayed on the side of the screen
        // the phone will display camping photos and targeted ads
        phone.SetActive(true);

        // begin dialog in form of subtitles
        subtitle_text.text = "Oooh these places on social media look nice!";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "Let's go find those items!";

        // put up a notification that will describe to the user what items they 
        // need to pick up
        notifcations.GetComponent<notification_controller>().create_items_notif();

        // set all the camping items that need to be picked up to glow
        foreach (GameObject item_to_get in camping_items)
        {
            item_to_get.GetComponent<Outline>().enabled = true; // turn on glow
        }

        // wait for a bit for the user to register what is going on
        set_story_stage("getting_items");
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "";
        yield return null;
    }

    /*
     * Story stage within the game:
     * - User has collected all items and is told to do some online shopping
     *
     * Will be triggered after:
     * - User has collected all items for camping within unit
     * 
     * The user now has to go to the computer to begin the online shopping
     */
    private IEnumerator start_stage_6()
    {
        set_story_stage("before_website_game");

        // begin dialog in form of subtitles
        subtitle_text.text = "Ok! that seems like all the items on the old list...";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Damn, there are definitely things that I forgot on that list though...";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "I think I have a second list on my computer with some other items that I needed to buy";
        
        // close social media phone to finish up that interaction
        phone.SetActive(false);
        yield return new WaitForSeconds(5); // wait
        subtitle_text.text = "";
        set_story_stage("website_game");

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Check out the living room computer");

        // turn on glow for computer that the user has to interact with next for online shopping
        computer.GetComponent<Outline>().enabled = true;
        yield return null;
    }

    /*
     * Story stage within the game:
     * - User has must now go to sleep to progress the story
     *
     * Will be triggered after:
     * - After user has completed online shopping
     * 
     * The user will be told to go to a bed in their bedroom
     */
    private IEnumerator start_stage_7()
    {
        set_story_stage("before_bed_time");

        // begin dialog in form of subtitles
        subtitle_text.text = "Ok! that seems like it then!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I think I have all the items I need for camping...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I just need to hope they all arrive in time";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Wow it is getting late...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I should really go to sleep now";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Time to go to bed for the night, find your bed");
        
        // turn on glow for bed
        bed.GetComponent<Outline>().enabled = true;
        set_story_stage("bed_time");
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Turn off ipad alarm on tablet
     *
     * Will be triggered after:
     * - User has slept in their bed, it is now morning
     * 
     * The user will be told to turn off their ipad alarm that has woken them up
     */
    private IEnumerator start_stage_8()
    {
        set_story_stage("before_wake_up");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();
        bed.GetComponent<Outline>().enabled = false; // bed no longer glows becuse we have slept in in

        // disable all player controls and excess UI
        // since the user is waking up and cannot move
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // turn on the pin phone to use after wake up
        // since we will need to use it soon
        passcode_phone.SetActive(true);

        // register the fade in / out screen object
        GDTFadeEffect fade_cont = fade_obj.GetComponent<GDTFadeEffect>();

        // fade out screen (going to sleep)
        fade_cont.firstToLast = false; // fade to black
        fade_obj.SetActive(false);
        fade_obj.SetActive(true);
        yield return new WaitForSeconds(3); // wait

        // it is now morning :)
        RenderSettings.skybox = morning;

        // turn on alarm
        tablet.GetComponent<tablet_controller>().turn_on_alarm();
        // turn on tabelet glow since we need to interact with it
        tablet.GetComponent<Outline>().enabled = true;

        // fade in screen (waking up)
        fade_cont.firstToLast = true; // fade to clear
        fade_obj.SetActive(false);
        fade_obj.SetActive(true);
        yield return new WaitForSeconds(3); // wait

        // re enable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);

        // begin dialog in form of subtitles
        subtitle_text.text = "uhhh..h..";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "That music must be my alarm.....";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "I am always misplacing my ipad";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Find your ipad and turn off the alarm");
        set_story_stage("wake_up");
        yield return null;

    }

    /*
     * Story stage within the game:
     * - Packages have arrived from your online shopping
     *
     * Will be triggered after:
     * - Player turns off the alarm and exits the tablet
     * 
     * The user will be informed that they have to check the door 
     * as their online shopping has arrived
     */
    private IEnumerator start_stage_9()
    {
        // alarm is off
        set_story_stage("alarm_off");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // begin dialog in form of subtitles
        subtitle_text.text = "Thank goodness that alarm is off now";
        yield return new WaitForSeconds(2); // wait

        // ding dong the door bell sound plays since packages arrive
        door_bell_sound.SetActive(true); // play the sound
        boxes.SetActive(true); // add boxes to the scene

        // more dialog
        subtitle_text.text = "Oh I think that is the door!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "That must be our camping gear arriving!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Go to the door and see what arrived");

        // highlight boxes
        set_story_stage("find_boxes");
        boxes.GetComponent<Outline>().enabled = true;
        yield return null;
    }

    /*
     * Story stage within the game:
     * - Boxes opened, need to confirm order on phone
     *
     * Will be triggered after:
     * - Player opens the boxes at the door
     * 
     * User will be asked to go to phone and confirm that the order has arrived
     */
    private IEnumerator start_stage_10()
    {
        set_story_stage("boxes_found");

        // begin dialog in form of subtitles
        subtitle_text.text = "I have everything I need for camping now!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Oh wait! I need to confirm that the order arrived on my phone before I forget";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "I think my code is 3512";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Confirm the order on your phone, check kitchen counter (CODE: 3512)");
        
        // turn on glow for the phone
        passcode_phone.GetComponent<Outline>().enabled = true;
        set_story_stage("ender_passcode");
        yield return null;
    }

    /*
     * Story stage within the game:
     * - After the code is entered, the main story is now done :)
     *
     * Will be triggered after:
     * - Player correctly enters their pin on the phone to confirm the order
     * 
     * After the code is entered, the main story is now done 
     */
    private IEnumerator start_stage_11()
    {
        set_story_stage("code_entered");

        // begin dialog in form of subtitles
        subtitle_text.text = "Ok cool!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I can't wait for camping tomorrow!!";
        yield return new WaitForSeconds(4); // wait

        // begin the ending game slide show
        StartCoroutine(ending_ss_1());
        yield return null;
    }

    /*
     * First ending slide in the end game slide show
     * 
     * Will explain that the main story is over
     */
    private IEnumerator ending_ss_1()
    {
        set_story_stage("ending_ss_1");
        // begin the slide show
        in_ss = true;
        subtitle_text.text = "";

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(true);

        // show the first slide
        ss_ending_1.SetActive(true);

        // show press [e] to continue text
        ss_press_e_text.SetActive(true);

        set_story_stage("ending_ss_1_waiting");
        yield return null;
    }

    /*
     * Second ending slide in the end game slide show
     * 
     * Will explain that their gaze was being tracked and exploited
     */
    private IEnumerator ending_ss_2()
    {
        set_story_stage("ending_ss_2");
        // turn off prev slide
        ss_ending_1.SetActive(false);
        // turn on new slide
        ss_ending_2.SetActive(true);
        set_story_stage("ending_ss_2_waiting");
        yield return null;
    }

    /*
     * Third ending slide in the end game slide show - Video
     * 
     * Will show the user all the cameras that were tracking their gaze
     * by showing a video of the cam locations
     */
    private IEnumerator ending_ss_3()
    {
        set_story_stage("ending_ss_3");
        // turn off prev slide
        ss_ending_2.SetActive(false);
        // turn on new slide
        ss_ending_3.SetActive(true);

        // lead in time before the video begins
        yield return new WaitForSeconds(6); // wait
        set_story_stage("ending_ss_3_waiting");
        ss_ending_3_text.SetActive(false); // turn off text when real video begins

        // video is 40 seconds 40 - 6 = 34
        yield return new WaitForSeconds(34); // wait while video is player

        // if the user hasn't force skiped the cutsceen yet then move to tthe next
        if (!skipped_cutscene) { StartCoroutine(ending_ss_4()); }
        yield return null;
    }

    /*
     * Fouth ending slide in the end game slide show - Video
     * 
     * Will explain to the user they can now explore all the demos
     * and get explained how their gaze was tracked
     * 
     * NOTE this is the stage that the player will enter if they 
     * choose to skip to the end game demos from the start screen
     */
    private IEnumerator ending_ss_4()
    {
        // make sure to record we are in the slide show 
        // since we may be entering this stage from the start screen
        in_ss = true;
        subtitle_text.text = "";

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(true);
        // show press [e] to continue text
        ss_press_e_text.SetActive(true);

        set_story_stage("ending_ss_4");
        // turn off prev slide
        ss_ending_3.SetActive(false);
        // turn on new slide
        ss_ending_4.SetActive(true);

        set_story_stage("ending_ss_4_waiting");
        yield return null;
    }

    /*
     * This wraps up the end game slide show and allows the user to 
     * explore the world freely and try out all the demos.
     */
    private IEnumerator ending_ss_end()
    {
        set_story_stage("ending_ss_end");
        in_ss = false; // slide show now over

        // disable old objs that are only in main story
        old_objs.SetActive(false);
        // enable new objs that are only in end game demo part of game
        new_objs.SetActive(true);

        // add task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Interact with red objects to see how they exploit your gaze tracking data");
        menu_exit_text.SetActive(true);

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(false);

        // turn of last slide
        ss_ending_4.SetActive(false);
        // remove press [e] to continue text
        ss_press_e_text.SetActive(false);

        set_story_stage("end_game_interactions");
        yield return null;
    }
}
