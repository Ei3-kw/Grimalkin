/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee & Timothy Ryall
 * 
 * Purpose:
 * - controls the phone and what poster is showing on the phone 
 *   
 * Attached to objects in game scene:
 * - phone that user can hold in hand
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SM_phone_controller : MonoBehaviour
{   
    // base delay in between post refresh in seconds 
    public float refreshDelay;
    // limits for randomly selecting topics 
    // thresholds[0] the chance of not selecting the main topic
    // thresholds[0] the chance of skipping a observation topic 
    public float[] thresholds;
    // reference to the player_observer class see player_observer.cs for more info
    public player_observer myplayer_observer;
    // reference to poster text object  
    public TextMeshPro phoneText;
    // reference to poster Material, used to update the image 
    public Material imageMat;
    // main topic used to get post not related to the recent observation 
    public string mainTopic = "trip";


    // a poster made of a image and text 
    [Serializable]
    public class Poster{
        public Texture image;
        public String Text;
    }

    // a collection of poster with a shared topic 
    [Serializable]
    public class PosterCollection
    {
        // the topic name
        public String name;
        public List<Poster> collection;
    }

    // list of PosterCollection to edited in the inspector to add posters 
    public List<PosterCollection> posters;

    // to be enabled at endgame 
    public bool demo_mode = false;
    public GameObject demo_text;

    // list of the names of the posters that will be shown (for dev purposes)
    private List<String> postersNames;

    // time between image chages on phone
    private float nextRefreshTime;


    /*
     * Start is called before the first frame update
     * 
     * We want to set up the phone with the first poster.
     * And keep track of all the possible posters we can show the user
     */
    void Start()
    {   
        // set the poster to the first one 
        imageMat.mainTexture = posters[0].collection[0].image;
        phoneText.text = posters[0].collection[0].Text;

        // crate a list of poster names from posters 
        foreach (PosterCollection coll in posters)
        {
            postersNames.Add(coll.name);
        }
    }
    //*******************************
    // user data in use section start 
    //*******************************

    /*
     * Update is called once per frame.
     * 
     * We will check if the user whishes to exit the phone.
     * If not, we will check if it is time for the poster to change
     * i.e. refresh to a new image. if it is, we will set a new image based
     * on what the user has been looking at.
     * 
     * (the more frequent the user has looked at the item, the more likly it is to 
     * show up on phone)
     */
    void Update()
    {
        // if in demo mode and wish to put the phone away
        if (demo_mode && Input.GetKeyDown("f"))
        {
            demo_text.SetActive(false); // turn of the instruction text
            gameObject.SetActive(false); // turn the phone off
        }

        if ( Time.time > nextRefreshTime )
        {   
            // set the refresh time with a small random variable   
            nextRefreshTime = Time.time + refreshDelay + UnityEngine.Random.Range(0.0f,1.0f) ;

            // using a random value to select a poster from the main topic  
            if (UnityEngine.Random.Range(0.0f, 1.0f) > thresholds[0])
            {
                int index = postersNames.IndexOf(mainTopic);
                setToRandomFromCollection(posters[index].collection);
                
            } else
            {
                // select a poster based on the recent observation
                List<KeyValuePair<String,int>> recent =  myplayer_observer.getRecent();
                foreach (KeyValuePair<string,int> pair in recent)
                {   
                    // observation topic might not have posters related to it 
                    if (postersNames.Contains(pair.Key) )
                    {
                        // randomly choose to skip topic, 
                        // to get more variant of posters 
                        if (UnityEngine.Random.Range(0.0f, 1.0f) > thresholds[1])
                        {
                            int index = postersNames.IndexOf(pair.Key);
                            setToRandomFromCollection(posters[index].collection);
                            break;
                        }
                    }
                }
            } 
        }
    }

    //-------------------------------
    // user data in use section end
    //-------------------------------

    /*
     * set the poster on the phone to one of the poster from the givin collection.
     * i.e. change the image on the phone
     * 
     * collection: list of posters that we will choose one randomly from
     *             to display on the phone.
     */
    public void setToRandomFromCollection(List<Poster> collection){
        int rint = UnityEngine.Random.Range(0, collection.Count);
        imageMat.mainTexture = collection[rint].image;
        phoneText.text = collection[rint].Text;
    }

    /*
     * Will start the phone in demo mode (i.e. end of game explainations)
     */
    public void start_demo()
    {
        demo_mode = true; // signify that we are in the demo
    }

    /*
     * Will end the user demo and return the phone away
     */
    public void end_demo()
    {
        demo_text.SetActive(false); // turn of the instruction text
        gameObject.SetActive(false); // turn the phone off
    }
}
