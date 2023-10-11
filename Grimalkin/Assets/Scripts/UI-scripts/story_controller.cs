using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class story_controller : MonoBehaviour
{
    // FPS cap for game to ensure consistant behaviour and preformance
    public int targetFrameRate = 60;

    public GameObject player;
    public GameObject optional_UI;

    public GameObject subtitles;
    public GameObject notifcations;
    private TextMeshProUGUI subtitle_text;

    private string story_stage;

    //// interactions ////
    // game 1
    public GameObject coffee_cup;

    // game 3
    public GameObject computer;

    // game 2
    public GameObject camping_wall_photo;

    // game 4
    public GameObject bed;
    public GameObject fade_obj;

    // game 5
    public GameObject tablet;

    // game ??
    public GameObject phone;
    public GameObject phone_packing_list;
    public GameObject[] camping_items;


    public GameObject passcode_phone;
    public GameObject boxes;
    public GameObject door_bell_sound;

    // intro controls
    public GameObject controls_intro;




    // ending slide show
    private bool in_ss = false;

    public GameObject ss_ending_1;
    public GameObject ss_ending_2;
    public GameObject ss_ending_3;
    public GameObject ss_ending_3_text;
    public GameObject ss_ending_4;



    //end
    public GameObject ss_exit_text;
    public GameObject ss_all_interaction_slides;

    // start
    public GameObject ss_screen;
    public GameObject ss_press_e_text;

    


    public GameObject old_objs;
    public GameObject new_objs;

    public Material night;
    public Material morning;

    public Transform starting_pos;
    public GameObject start_game_text;
    public GameObject fade_in_start;

    public GameObject pause_menu;

    public GameObject menu_exit_text;


    private bool skipped_cutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        fade_in_start.SetActive(false);
        // Cap FPSto ensure consistant behaviour and preformance
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        phone.SetActive(false);
        boxes.SetActive(false);

        // define paramas
        subtitle_text = subtitles.GetComponent<TextMeshProUGUI>();
        ////////////////////////////
        ///
        ss_screen.SetActive(false);
        ss_press_e_text.SetActive(false);


        ////// WHERE TO BEGIN ? ////////////
        // beging dialog 1

        //StartCoroutine(start_stage_4());

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);
        set_story_stage("start_screen");
        start_game_text.SetActive(true);

    }











    // Update is called once per frame
    void Update()
    {
        // if user wants to bring up the excape menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pause_menu.SetActive(true);
        }

        // if we are about to begin the game
        // and press 'e' to begin
        if (story_stage == "start_screen")
        {
            if (Input.GetKeyDown("1"))
            {
                player.GetComponent<playerController>().enabled = true;
                optional_UI.SetActive(true);

                // move player to starting pos
                Vector3 targetPosition = starting_pos.position;
                Quaternion targetRotation = starting_pos.rotation;
                Transform player_pos = player.GetComponent<Transform>();
                player_pos.position = targetPosition;
                player_pos.rotation = targetRotation;

                // turn off start game text
                start_game_text.SetActive(false);
                StartCoroutine(start_stage_1()); // begin the game from the start
            }
            else if (Input.GetKeyDown("2"))
            {
                // move player to starting pos
                Vector3 targetPosition = starting_pos.position;
                Quaternion targetRotation = starting_pos.rotation;
                Transform player_pos = player.GetComponent<Transform>();
                player_pos.position = targetPosition;
                player_pos.rotation = targetRotation;

                // turn off start game text
                start_game_text.SetActive(false);
                StartCoroutine(ending_ss_4()); // skip to end game demo scenes

            }
                
        }






        // if they want to open their phone for the first time
        if (story_stage == "waiting_for_phone_open" && Input.GetKeyDown("e"))
        {

            // DO NOT CHANEG THISSSSS
            StartCoroutine(start_stage_5());
        }

        if (story_stage == "waiting_for_socail_phone" && Input.GetKeyDown("e"))
        {
            Debug.Log("socail media time");
            StartCoroutine(start_stage_5_2());

        }

            









        /////// FOR THE SLIDE SHOW
        if (in_ss && Input.GetKeyDown("e"))
        {
            if (story_stage == "ending_ss_1_waiting") { StartCoroutine(ending_ss_2()); }
            else if (story_stage == "ending_ss_2_waiting") { StartCoroutine(ending_ss_3()); }
            else if (story_stage == "ending_ss_3_waiting") 
            {
                skipped_cutscene = true;
                StartCoroutine(ending_ss_4()); 
            }
            else if (story_stage == "ending_ss_4_waiting") { StartCoroutine(ending_ss_end()); }

        }
   







    }

    public void set_story_stage(string stage)
    {
        story_stage = stage;
        player.GetComponent<playerController>().set_story_stage(stage);

    }

    /// <summary>
    /// CALLS BY PLAEYRS
    /// </summary>

    public void looked_at_cwp()
    {
        StartCoroutine(start_stage_4());
    }

    public void got_coffee()
    {
        StartCoroutine(start_stage_3());
    }

    public void finished_website_game()
    {
        StartCoroutine(start_stage_7());
    }

    public void in_bed_now()
    {
        StartCoroutine(start_stage_8());
    }

    public void alarm_off()
    {
        StartCoroutine(start_stage_9());
    }

    public void got_all_items()
    {
        StartCoroutine(start_stage_6());
    }

    public void opened_boxes()
    {
        StartCoroutine(start_stage_10());
    }

    public void code_entered()
    {
        StartCoroutine(start_stage_11());
    }







    /// <summary>
    /// STORY LINE
    /// </summary>


    // DIALOG 1
    private IEnumerator start_stage_1()
    {
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);
        passcode_phone.SetActive(false); // turn of pin phone to avoid confusion

        fade_in_start.SetActive(true);






        set_story_stage("start_dialog");

        // screen fade in
        yield return new WaitForSeconds(3); // wait


        // subtiles 1
        subtitle_text.text = "Oh Uh... Our anniversary is this weekend...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I really need to plan something for that...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "But first, I would kill for a coffee";
        yield return new WaitForSeconds(2); // wait

        // describe to user the controls
        ss_screen.SetActive(true);
        ss_press_e_text.SetActive(true);
        controls_intro.SetActive(true);



        // only progress when click e
        while (!Input.GetKeyDown("e"))
        {
            yield return null;
        }
        controls_intro.SetActive(false);
        ss_screen.SetActive(false);
        ss_press_e_text.SetActive(false);


        // re enable the movment script and UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);


        // beging game 1
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "";
        StartCoroutine(start_stage_2());

        yield return null;
    }

    // GAME 1
    // COFFEE CUP
    private IEnumerator start_stage_2()
    {
        set_story_stage("coffee");

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Go grab some coffee from the bench");

        // turn on glow for coffee
        coffee_cup.GetComponent<Outline>().enabled = true; // turn off the glow when looked at it

        // game will end when coffee is clicked on 
        yield return null;
    }

    // Dialog 2 
    // hmm what to do..
    private IEnumerator start_stage_3()
    {
        // coffee has been clicked on
        set_story_stage("before_look_at_painting");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Ahh, thats better";
        yield return new WaitForSeconds(2); // wait

        /*
        subtitle_text.text = "Now... What to do for the anniversary";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Maybe I should look around and hope inspiration hits";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I have so many pictures on these walls surely there is something..";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Look around for ideas for your anniversary plans");

        // turn on glow for camping photo
        camping_wall_photo.GetComponent<Outline>().enabled = true;
        set_story_stage("look_at_painting");
        */
        subtitle_text.text = "Oh!! I have an idea for the anniversary!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Camping!!";
        yield return new WaitForSeconds(2); // wait

        StartCoroutine(start_stage_4());
        yield return null;
    }

    // Dialog 3 
    // hmm what to do..
    private IEnumerator start_stage_4()
    {
        set_story_stage("after_painting");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        //subtitle_text.text = "Oh! Camping!";
        //yield return new WaitForSeconds(2); // wait
        //subtitle_text.text = "I wonder if I still have all my gear from my last camping trip";
        //yield return new WaitForSeconds(4); // wait

        subtitle_text.text = "I have a list of items I need to pack saved on my phone";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Press [e] to open your phone";


        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Press [e] to open you phone and check packing list");

        set_story_stage("waiting_for_phone_open");
        yield return null;
    }

    // game 2
    // hmm what to do..
    private IEnumerator start_stage_5()
    {
        set_story_stage("phone_opened");

        // open packing list on phone infront of face
        phone_packing_list.SetActive(true);





        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Ahh yes this is the list of stuff I need to pack!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I just need to find these items around the house and pick them up";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I can look at photos of the camp site while I collect the items!";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Press [e] to open social media";




        set_story_stage("waiting_for_socail_phone");
        yield return null;
    }

    // game 2
    // hmm what to do..
    private IEnumerator start_stage_5_2()
    {
        set_story_stage("open_socail_phone");

        // close packing list on phone infront of face
        phone_packing_list.SetActive(false);
        // open social media phone
        phone.SetActive(true);

        subtitle_text.text = "Oooh these places on social media look nice!";
        yield return new WaitForSeconds(4); // wait

        subtitle_text.text = "Lets go find those items!";
        notifcations.GetComponent<notification_controller>().create_items_notif();

        // set all the items to glow :)
        foreach (GameObject item_to_get in camping_items)
        {
            item_to_get.GetComponent<Outline>().enabled = true; // turn on glow
        }
        set_story_stage("getting_items");


        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "";




        yield return null;
    }

    // game 3
    // hmm what to do..
    private IEnumerator start_stage_6()
    {
        set_story_stage("before_website_game");

        subtitle_text.text = "Ok! that seems like all the items on the old list...";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "Damn, there are definitely things that I forgot on that list though...";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "I think I have second list on my computer with some other items that I needed to buy";
        // close social media phone
        phone.SetActive(false);
        yield return new WaitForSeconds(5); // wait





        subtitle_text.text = "";
        set_story_stage("website_game");

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Check out the living room computer");

        // turn on glow for computer
        computer.GetComponent<Outline>().enabled = true;
        yield return null;
    }

    private IEnumerator start_stage_7()
    {
        set_story_stage("before_bed_time");

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

        //StartCoroutine(start_stage_8());

        yield return null;

    }

    private IEnumerator start_stage_8()
    {
        // bed has been clicked on
        set_story_stage("before_wake_up");
        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();
        bed.GetComponent<Outline>().enabled = false;

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // turn on the pin phone to use after wake up
        passcode_phone.SetActive(true);


        GDTFadeEffect fade_cont = fade_obj.GetComponent<GDTFadeEffect>();
        // fade out screen
        fade_cont.firstToLast = false; // fade to black
        fade_obj.SetActive(false);
        fade_obj.SetActive(true);
        yield return new WaitForSeconds(3); // wait
        RenderSettings.skybox = morning;

        // turn on alarm
        tablet.GetComponent<tablet_controller>().turn_on_alarm();
        // turn on tabelet glow
        tablet.GetComponent<Outline>().enabled = true;



        // fade in screen
        fade_cont.firstToLast = true; // fade to clear
        fade_obj.SetActive(false);
        fade_obj.SetActive(true);
        yield return new WaitForSeconds(3); // wait

        // re enable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);


        //
        subtitle_text.text = "uhhh..h..";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "That music must be my alarm.....";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "I am always misplacing my ipad";
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "";

        set_story_stage("wake_up");

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Find your ipad and turn off the alarm");

        yield return null;

    }

    private IEnumerator start_stage_9()
    {
        // alarm is off

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();
        set_story_stage("alarm_off");

        subtitle_text.text = "Thank goodness that alarm is off now";
        yield return new WaitForSeconds(2); // wait


        // ding dong the door bell
        door_bell_sound.SetActive(true);

        subtitle_text.text = "Oh I think that is the door!";
        yield return new WaitForSeconds(2); // wait

        boxes.SetActive(true);
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

    private IEnumerator start_stage_10()
    {
        set_story_stage("boxes_found");

        subtitle_text.text = "I have everything thing I need for camping now!!";
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "Oh wait! I need to confirm that the order arrived on my phone before I forget";
        yield return new WaitForSeconds(4); // wait

        subtitle_text.text = "I think my code is 3512";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Confirm the order on your phone, check kitchen counter (CODE: 3512)");
        // turn on glow for bed
        passcode_phone.GetComponent<Outline>().enabled = true;
        set_story_stage("ender_passcode");



        yield return null;

    }

    private IEnumerator start_stage_11()
    {
        set_story_stage("code_entered");
        subtitle_text.text = "Ok cool!!";
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "I can't wait for camping tomorow!!";
        yield return new WaitForSeconds(4); // wait

        StartCoroutine(ending_ss_1());
        yield return null;
    }


        /*
        private IEnumerator ending_ss_end()
        {
            set_story_stage("ending_ss_end");
            ss_all_interaction_slides.SetActive(false);
            ss_press_e_text.SetActive(false);

            // BEcause of this you spennt ___ extra dollars
            // and couldn't afford food, your partners dog died :<
            ss_sg_4.SetActive(true);
            // Press [e] to exit game to desktop
            ss_exit_text.SetActive(true);



            // wait for button to be pressed to go to the next slide
            set_story_stage("ending_ss_end_waiting");
            yield return null;
        }
        */

    private IEnumerator ending_ss_end()
    {
        set_story_stage("ending_ss_end");
        in_ss = false;

        // disable old objs
        old_objs.SetActive(false);

        // enable new objs
        new_objs.SetActive(true);

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Interact with red objects to see how they exploit your gaze tracking data");
        menu_exit_text.SetActive(true);

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(false);

        // you left for your camping trip
        // however the consqince from the days before came back to bite
        ss_ending_4.SetActive(false);
        // Press [e] to continue
        ss_press_e_text.SetActive(false);

        set_story_stage("end_game_interactions");


        yield return null;
    }


    // first ending slide
    private IEnumerator ending_ss_1()
    {
        in_ss = true;

        set_story_stage("ending_ss_1");
        subtitle_text.text = "";

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // pop up slide show screen (mosly opaque screen with text)
        // can still kinda see background
        ss_screen.SetActive(true);
        
        // you left for your camping trip
        // however the consqince from the days before came back to bite
        ss_ending_1.SetActive(true);
        // Press [e] to continue
        ss_press_e_text.SetActive(true);

        set_story_stage("ending_ss_1_waiting");

        yield return null;
    }

    // 2nd ending slide
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

    // 3nd ending slide - video
    private IEnumerator ending_ss_3()
    {
        set_story_stage("ending_ss_3");
        // turn off prev slide
        ss_ending_2.SetActive(false);
        // turn on new slide
        ss_ending_3.SetActive(true);

        

        // lead in time
        yield return new WaitForSeconds(6); // wait
        set_story_stage("ending_ss_3_waiting");

        ss_ending_3_text.SetActive(false);

        // video is 40 seconds 40 - 5 = 35
        yield return new WaitForSeconds(34); // wait
        if (!skipped_cutscene) { StartCoroutine(ending_ss_4()); }
        
        yield return null;
    }

    // 4nd ending slide
    private IEnumerator ending_ss_4()
    {
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
        // Press [e] to continue
        ss_press_e_text.SetActive(true);


        set_story_stage("ending_ss_4");
        // turn off prev slide
        ss_ending_3.SetActive(false);
        // turn on new slide
        ss_ending_4.SetActive(true);

        set_story_stage("ending_ss_4_waiting");


        yield return null;
    }






}


