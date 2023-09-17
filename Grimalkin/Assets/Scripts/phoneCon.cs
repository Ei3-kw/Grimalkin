using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class phoneCon : MonoBehaviour
{   
    public float refreshDelay;
    public observer myObserver;
    public TextMeshPro phoneText;
    public Material imageMat;
    public GameObject phoneBody;
    public bool phoneON = false;
    public List<String> postsNames;
    public List<Texture> images;
    public List<String> postsText;

    float nextTime;
    string currentPost;
    



    // Start is called before the first frame update
    void Start()
    {
        currentPost = postsNames[0];
        imageMat.mainTexture = images[0];
        phoneText.text = postsText[0];
        phoneON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            phoneON = !phoneON;
            phoneBody.SetActive(phoneON);
        }
        if (phoneON && Time.time > nextTime)
        {
            nextTime = Time.time + refreshDelay;
            List<KeyValuePair<String,int>> recent =  myObserver.getRecent();
            foreach (KeyValuePair<string,int> pair in recent)
            {   
                if (postsNames.Contains(pair.Key) && currentPost != pair.Key)
                {
                    currentPost= pair.Key;
                    int index = postsNames.IndexOf(pair.Key);
                    imageMat.mainTexture = images[index];
                    phoneText.text = postsText[index];
                    break;
                }
            }
        }
        
    }
}
