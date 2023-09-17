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

    //// interactions ////
    // game 1
    public GameObject coffee_cup;

    // game ???
    public GameObject computer;

    // game 2
    public GameObject camping_wall_photo;


    // Start is called before the first frame update
    void Start()
    {
        // define paramas
        subtitle_text = subtitles.GetComponent<TextMeshProUGUI>();
        ////////////////////////////

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);
        currency_UI.SetActive(false);



        // beging dialog 1
        StartCoroutine(start_stage_1());

    }

    // Update is called once per frame
    void Update()
    {
        
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






    /// <summary>
    /// STORY LINE
    /// </summary>
    

    // DIALOG 1
    private IEnumerator start_stage_1()
    {
        player.GetComponent<playerController>().set_story_stage("start_dialog");

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
        player.GetComponent<playerController>().set_story_stage("coffee");

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
        player.GetComponent<playerController>().set_story_stage("look_at_painting");

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
        camping_wall_photo.GetComponent<Outline>().enabled = true; // turn off the glow when looked at it

        yield return null;
    }

    // Dialog 3 
    // hmm what to do..
    private IEnumerator start_stage_4()
    {
        // coffee has been clicked on
        player.GetComponent<playerController>().set_story_stage("after_painting");

        // remove task notifaction
        notifcations.GetComponent<notification_controller>().remove_notif();

        subtitle_text.text = "Oh!!! Camping !!!!!";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "";


        yield return null;
    }
}
