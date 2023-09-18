using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class story_controller : MonoBehaviour
{
    public GameObject player;
    public GameObject optional_UI;
    public GameObject currency_UI;

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

    // game 5
    public GameObject tablet;


    // Start is called before the first frame update
    void Start()
    {
        // define paramas
        subtitle_text = subtitles.GetComponent<TextMeshProUGUI>();
        ////////////////////////////



        ////// WHERE TO BEGIN ? ////////////
        // beging dialog 1
        StartCoroutine(start_stage_8());

    }

    // Update is called once per frame
    void Update()
    {
        // if they want to open their phone for the first time
        if(story_stage == "waiting_for_phone_open" && Input.GetKeyDown("f"))
        {
            StartCoroutine(start_stage_5());
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







    /// <summary>
    /// STORY LINE
    /// </summary>


    // DIALOG 1
    private IEnumerator start_stage_1()
    {

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);
        currency_UI.SetActive(false);

        set_story_stage("start_dialog");

        // screen fade in
        yield return new WaitForSeconds(3); // wait

        // notifcation pop up
        yield return new WaitForSeconds(2); // wait

        // subtiles 1
        subtitle_text.text = "Oh Uh... Our anniversary is this weekend...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I really need to plan something for that...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "But first, I would kill for a coffee";

        // re enable the movment script and UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        currency_UI.SetActive(true);


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
        set_story_stage("look_at_painting");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Ahh, thats better";
        yield return new WaitForSeconds(1); // wait
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

        yield return null;
    }

    // Dialog 3 
    // hmm what to do..
    private IEnumerator start_stage_4()
    {
        set_story_stage("after_painting");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Oh! Camping!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I wonder if I still have all my gear from my last camping trip";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "I have a list of items I need to pack saved on my phone";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "Press [F] to open your phone";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Press [F] to open you phone and check packing list");

        set_story_stage("waiting_for_phone_open");
        yield return null;
    }

    // game 2
    // hmm what to do..
    private IEnumerator start_stage_5()
    {
        set_story_stage("phone_opened");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Ahh yes this is the list of stuff I need to pack!";
        yield return new WaitForSeconds(4); // wait
        subtitle_text.text = "Implement phone interaction........";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "Pick up all the items we need.......";
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "";
        StartCoroutine(start_stage_6());

        yield return null;
    }

    // game 3
    // hmm what to do..
    private IEnumerator start_stage_6()
    {
        set_story_stage("website_game");

        subtitle_text.text = "Damn, there are definitely things that I forgot on this list...";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "I think I have second list on my computer with some other items that I needed to buy";
        yield return new WaitForSeconds(5); // wait



        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Check out the living room computer");

        // turn on glow for computer
        computer.GetComponent<Outline>().enabled = true;
        yield return null;
    }

    private IEnumerator start_stage_7()
    {
        set_story_stage("bed_time");

        subtitle_text.text = "Ok! that seems like it then!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I think I have all the items I need for camping...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I Just need to hope they all arrive in time";
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

        //StartCoroutine(start_stage_8());

        yield return null;

    }

    private IEnumerator start_stage_8()
    {
        // bed has been clicked on
        set_story_stage("wake_up");
        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        // turn on alarm
        tablet.GetComponent<tablet_controller>().turn_on_alarm();
        // turn on tabelet glow
        tablet.GetComponent<Outline>().enabled = true;

        // fade out screen

        // fade in screen

        //
        subtitle_text.text = "uhhh..h..";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "That music must be my alarm.....";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "I am always misplacing my ipad";
        yield return new WaitForSeconds(2); // wait

        subtitle_text.text = "";

        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Find your ipad and turn off the alarm");

        //StartCoroutine(start_stage_8());

        yield return null;

    }

    private IEnumerator start_stage_9()
    {
        // bed has been clicked on

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();
        set_story_stage("alarm_off");

        subtitle_text.text = "Thank goodness that alarm is off now";
        yield return new WaitForSeconds(4); // wait
        

        // ding dong the door bell
        subtitle_text.text = "Oh I think that is the door!";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "That must be our camping gear arriving!";
        yield return new WaitForSeconds(3); // wait
        subtitle_text.text = "";


        // pop up task notifaction
        notifcations.GetComponent<notification_controller>().set_notif("Go to the door and see what arrived");


        yield return null;

    }
}
