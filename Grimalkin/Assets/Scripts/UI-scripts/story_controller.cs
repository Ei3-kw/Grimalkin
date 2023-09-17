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

    // game 2
    public GameObject computer;


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


    // DIALOG 1
    private IEnumerator start_stage_1()
    {
        // screen fade in
        yield return new WaitForSeconds(3); // wait

        // notifcation pop up
        yield return new WaitForSeconds(2); // wait

        // subtiles 1
        subtitle_text.text = "Oh Uh... Thats this weekend...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "I really need to plan something for that...";
        yield return new WaitForSeconds(2); // wait
        subtitle_text.text = "But first, I would kill for a coffee";

        // re enable the movment script and UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        currency_UI.SetActive(true);

        // pop up task notifaction
        yield return new WaitForSeconds(1); // wait
        notifcations.GetComponent<notification_controller>().set_notif("Go grab some coffee from the bench");







        yield return null;
    }

    // GAME 1
    // COFFEE CUP
    void start_stage_2()
    {
        



    }
}
