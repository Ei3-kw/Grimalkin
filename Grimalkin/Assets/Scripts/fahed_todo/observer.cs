/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee
 * 
 * Purpose:
 * - records the observation that the player has done including a total
 *   and recent observation by fetching the observable objects and adding 
 *  them to the Observations. 
 *   
 * 
 * Attached to objects in game scene:
 * - observer 
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class observer : MonoBehaviour
{
    /*
     * Observations class stores the total, recent observation
     * and handles adding new observation to them
     *
     * the observation are stored as (topic,number) pair where the number
     * is the number of times that topic has been observed
     */
    public class Observations{
        public Dictionary<string,int> total = new Dictionary<string,int>();
        public Dictionary<string,int> recent = new Dictionary<string,int>();
        // used to store recent observation so they can be
        // removed once there not recent 
        List<string>[] last;
        // the index of the oldest element in the last array 
        int index = 0;
        // the size of the last array 
        int size;

        public Observations(int lastSize){
            size = lastSize;
            last = new List<string>[size];
            last[0] = new List<string>();
        }

        public void add(List<string> newObservation){
            // remove the oldest observation form recent 
            if (last[index] != null){
                foreach (string info in last[index]){
                    recent[info] = recent[info] - 1;
                    if (recent[info] <= 0){
                        recent.Remove(info);
                    }
                }
            }
            
            // add the new observation to the recent list 
            last[index] = newObservation;


            // add the new observation to the total and recent 
            foreach (string info in newObservation){
                recent.TryGetValue(info, out var count); 
                recent[info] = count + 1;

                total.TryGetValue(info, out count); 
                total[info] = count + 1;

            }

            // increment the index to the new oldest 
            index = (index + 1)% size;
        }

    }
    public Observations observations;
    // the size of the recent observation / the number of frames that count as recent 
    public int recentSize;
    // the postion of eye tracker 
    public RectTransform visionPointer;

    // main camera 
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        observations = new Observations(recentSize);
    }

    // return the recent observations sorted by the most to least 
    public List<KeyValuePair<string,int>> getRecent(){
        List<KeyValuePair<string,int>> outputList = observations.recent.ToList();
        outputList.Sort((pair1,pair2) => pair2.Value.CompareTo(pair1.Value));
        return outputList;
    }

    void FixedUpdate()
    {   
        // casting a ray from the eye postion to the 3d world 
        Ray ray = cam.ScreenPointToRay(visionPointer.position);
        if (Physics.Raycast(ray, out var hit)) {
            // if the hit object has a observable component the observation is add
            observable test = hit.collider.gameObject.GetComponent<observable>();
            if(test != null){
                observations.add(test.info);
            }
        }
    }

    
}
