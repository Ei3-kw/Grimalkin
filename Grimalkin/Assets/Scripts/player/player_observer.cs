/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee & Timothy Ryall
 * 
 * Purpose:
 * - records the observation that the player has done including a total
 *   and recent observation by fetching the observable objects and adding 
 *   them to the Observations. 
 *   
 * 
 * Attached to objects in game scene:
 * - Player (as this is our player_observer in game)
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//*********************************
// user data in use section start 
//*********************************
public class player_observer : MonoBehaviour
{
    /*
     * Observations class stores the total, recent observation
     * and handles adding new observation to them
     *
     * the observation are stored as (topic,number) pair where the number
     * is the number of times that topic has been observed
     */
    public class Observations{
        // contains all total observations that the user makes
        // {observed item : how many frames we have looked at it for}
        public Dictionary<string,int> total = new Dictionary<string,int>();

        // used to store recent observation so they can be
        // removed once there not recent 
        public Dictionary<string,int> recent = new Dictionary<string,int>();

        // the index of the oldest element in the last array 
        List<string>[] last;
        int index = 0;

        // the size of the last array 
        int size;

        /*
         * Class init
         * 
         * we set the number of objects that can be observed initally
         * to be lastSize.
         * 
         * lastSize: number of objects that we can initally keep track of in our
         *           observations
         */
        public Observations(int lastSize){
            size = lastSize;
            last = new List<string>[size];
            last[0] = new List<string>();
        }

        /*
         * add a new observation.
         * 
         * This should be called when the user looks at something
         * i.e. observes something.
         * 
         * newObservation: the new items that the user has obsevered
         */
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

    // keep track of all the observations
    public Observations observations;

    // the size of the recent observation / the number of frames that count as recent 
    public int recentSize;

    // the postion of eye tracker 
    public RectTransform visionPointer;

    // main camera 
    public Camera cam;

    

    /*
     * Start is called before the first frame update
     * 
     * We set the inital obsevations up to count only the "recentSize"
     * most recent observations.
     */
    void Start()
    {
        observations = new Observations(recentSize);
    }

    //*********************************
    // user data in use section start 
    //*********************************

    /*
     * Get the recent obeservations made by the user
     * 
     * Returns: the recent observations sorted by the most to least
     */
    public List<KeyValuePair<string,int>> getRecent()
    {
        // convert observations to a list
        List<KeyValuePair<string,int>> outputList = observations.recent.ToList();

        // sort the list in most to least
        outputList.Sort((pair1,pair2) => pair2.Value.CompareTo(pair1.Value));
        return outputList;
    }



    /*
     * This update is called after a fixed amount of time
     * 
     * every fixed update we will check what the user is looking at
     * and record it 
     */
    void FixedUpdate()
    {   
        // casting a ray from the eye postion to the 3d world 
        Ray ray = cam.ScreenPointToRay(visionPointer.position);

        // if we hit something 
        if (Physics.Raycast(ray, out var hit)) 
        {
            // if the hit object has a observable component the observation is add
            observable_object test = hit.collider.gameObject.GetComponent<observable_object>();
            if (test != null) // if we are observing an obserable object
            {
                observations.add(test.info);
            }
        }
    }
}

//-------------------------------
// user data in use section end
//-------------------------------