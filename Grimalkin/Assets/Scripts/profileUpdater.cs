using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class profileUpdater : MonoBehaviour
{   
    [Serializable]
    public class DataPoint{
        public List<string> values;
        public TextMeshPro text;
    }

    public observer myobs;

    
    float  nextUpTime = 0;
    public float upDelay;

    public List<DataPoint> points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpTime){
            nextUpTime = Time.time + upDelay;
            foreach (DataPoint p in points ){
                string maxText = p.values[0];
                int currentMax = 0;
                foreach (string text in p.values){
                    myobs.observations.total.TryGetValue(text, out var count); 
                    if (count > currentMax){
                        currentMax = count;
                        maxText = text;
                    } 
                }
                p.text.text = maxText;
            }
        }
    }
}
