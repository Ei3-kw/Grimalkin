/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee
 * 
 * Purpose:
 * - controls the phone and what poster is showing on the phone 
 *   
 * Attached to objects in game scene:
 * - phone 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class phoneCon : MonoBehaviour
{   
    // base delay in between post refresh in seconds 
    public float refreshDelay;
    // limits for randomly selecting topics 
    // thresholds[0] the chance of not selecting the main topic
    // thresholds[0] the chance of skipping a observation topic 
    public float[] thresholds;
    // reference to the observer class see observer.cs for more info
    public observer myObserver;
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

    private List<String> postersNames;
        
    private float nextRefreshTime;


    // Start is called before the first frame update
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

    // Update is called once per frame
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
                setToRandomFromCollection(posters[index]);
                
            } else
            {
                // select a poster based on the recent observation
                List<KeyValuePair<String,int>> recent =  myObserver.getRecent();
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
                            setToRandomFromCollection(posters[index]);
                            break;
                        }
                    }
                }
            } 
        }
        
    }

    /*
     * set the poster on phone to one of the poster from the givin collection 
     * 
     * PostCollection: collection of posters we want to access and set the phone
     *                 screen to be one of the posters in the collection
     */
    public void setToRandomFromCollection(PosterCollection PostCollection){
        List<Poster> collection = PostCollection.collection;
        int rint = UnityEngine.Random.Range(0, collection.Count);
        imageMat.mainTexture = collection[rint].image;
        phoneText.text = collection[rint].Text;
    }

    public void start_demo()
    {
        demo_mode = true; // signify that we are in the demo
    }

    public void end_demo()
    {
        demo_text.SetActive(false); // turn of the instruction text
        gameObject.SetActive(false); // turn the phone off
    }
}
