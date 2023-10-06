using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class phoneCon : MonoBehaviour
{   
    public float refreshDelay;
    public float NotificationDelay;
    public float notificationChance;

    public float[] thresholds;
    public observer myObserver;
    public TextMeshPro phoneText;
    public Material imageMat;
    public GameObject phoneBody;
    public bool phoneON = false;

    public string mainTopic = "trip";
    

    float nextTime;
    float nextNotificationTime;
    string currentPost;


    [Serializable]
    public class Poster{
        public Texture image;
        public String Text;
    }

    [Serializable]
    public class PosterCollection
    {
        public String name;
        public List<Poster> collection;
    }


    public List<String> postersNames;

    public List<PosterCollection> posters;
    
    public Poster test;

    public bool demo_mode = false;
    public GameObject demo_text;




    // Start is called before the first frame update
    void Start()
    {
        //demo_mode = false;
        currentPost = posters[0].name;
        imageMat.mainTexture = posters[0].collection[0].image;
        phoneText.text = posters[0].collection[0].Text;
        phoneON = false;


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
            Debug.Log("demo mode off");
            demo_text.SetActive(false); // turn of the instruction text
            gameObject.SetActive(false); // turn the phone off
        }
        if (!phoneON &&  Time.time > nextNotificationTime ){
            nextNotificationTime = Time.time  + NotificationDelay;
            if (UnityEngine.Random.Range(0.0f, 1.0f) < notificationChance){
                phoneON = !phoneON;
                phoneBody.SetActive(phoneON);
            }
        }
        else if ( Time.time > nextTime || Input.GetKeyUp(KeyCode.N))
        {   
            nextTime = Time.time + refreshDelay + UnityEngine.Random.Range(0.0f,1.0f) ;
            if (UnityEngine.Random.Range(0.0f, 1.0f) > thresholds[0])
            {
                int index = postersNames.IndexOf(mainTopic);
                int rint = UnityEngine.Random.Range(0, posters[index].collection.Count);
                imageMat.mainTexture = posters[index].collection[rint].image;
                phoneText.text = posters[index].collection[rint].Text;
                
            } else
            {
                List<KeyValuePair<String,int>> recent =  myObserver.getRecent();
                foreach (KeyValuePair<string,int> pair in recent)
                {   
                    if (postersNames.Contains(pair.Key) )
                    {
                        if (UnityEngine.Random.Range(0.0f, 1.0f) > thresholds[1])
                        {
                            currentPost= pair.Key;

                            int index = postersNames.IndexOf(pair.Key);
                            int rint = UnityEngine.Random.Range(0, posters[index].collection.Count);
                            imageMat.mainTexture = posters[index].collection[rint].image;
                            phoneText.text = posters[index].collection[rint].Text;
                            Debug.Log(phoneText.text);
                            break;
                        }
                    }
                }

            }
            
            
        }
        
    }

    public void start_demo()
    {
        demo_mode = true; // signify that we are in the demo
        Debug.Log("demo mode ONNNN");
    }


}
