/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee
 * 
 * Purpose:
 * - update the text on the tv, that represent the profile of the player 
 * based on the observations collected.   
 *   
 * 
 * Attached to objects in game scene:
 * - ending_stats
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class profileUpdater : MonoBehaviour
{   
    // represent a data point with a text object and possible values  
    [Serializable]
    public class DataPoint{
        public List<string> values;
        public TextMeshPro text;
    }

    // reference to the observer class see observer.cs for more info
    public observer myObs;
    // delay between each update to the profile
    public float upDelay;
    // the next update time
    float  nextUpTime = 0;
    // list of the dataPoint on the end_state 
    public List<DataPoint> points;

    // Update is called once per frame
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
                        maxText = text;
                    } 
                }
                point.text.text = maxText;
            }
        }
    }
}
