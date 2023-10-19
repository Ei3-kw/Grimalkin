/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee & Timothy Ryall
 * 
 * Purpose:
 * - update the text on the tv, that represent the profile of the player 
 * based on the observations collected.   
 *   
 * 
 * Attached to objects in game scene:
 * - ending_stats (tv screen in end game that displays stats)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class demo_profile_controller : MonoBehaviour
{   

    // reference to the player_observer class see player_observer.cs for more info
    public player_observer myObs;

    // delay between each update to the profile
    public float upDelay;

    // the next update time
    float  nextUpTime = 0;

    //*******************************
    // user data in use section start 
    //*******************************

    // represent a data point with a text object and possible values  
    [Serializable]
    public class DataPoint{
        public List<string> values;
        public TextMeshPro text;
    }


    // list of the dataPoint on the end_state 
    public List<DataPoint> points;

    /*
     * Update is called once per frame.
     * 
     * We will check through everything that the user has observed.
     * And for each "category of items" e.g. fav animal, we will pick the 
     * item in that category that the user has looked at the most 
     * to display on the screen.
     */
    void Update()
    {
        if (Time.time >= nextUpTime){
            // set the next update time 
            nextUpTime = Time.time + upDelay;

            // loop through each data point and set the text to the most 
            // observed value of the point values 
            foreach (DataPoint point in points ){
                string maxText = point.values[0];
                int currentMax = 0;
                foreach (string text in point.values){
                    myObs.observations.total.TryGetValue(text, out var count); 
                    if (count > currentMax){
                        currentMax = count;
                        // set the new text on the screen
                        maxText = text;
                    } 
                }
                point.text.text = maxText;
            }
        }
    }

    //-------------------------------
    // user data in use section end
    //-------------------------------
}
