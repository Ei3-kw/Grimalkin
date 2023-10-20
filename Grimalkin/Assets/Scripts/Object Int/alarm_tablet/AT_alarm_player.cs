/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To ensure that there is only 1 source of alarm music
 * - and that one source is not destroyed
 * 
 * Attached to objects in game scene:
 * - The alarm music source (object playing the alarm music)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AT_alarm_player : MonoBehaviour
{
    /*
     * When the object is "Awoken" we want to ensure that we only have 1
     * music player.
     * 
     * i.e. there is only one sound source for the alarm music.
     */
    private void Awake(){
        // find a list of all the objects in the scene with this tag
        GameObject[] musicObj  = GameObject.FindGameObjectsWithTag("alarmMusic");

        // if there is more than one
        if (musicObj.Length > 1)
        {
            // destory the current game object (destory the current music source)
            // to reduce the number of music players since we only want one
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(this.gameObject); // ensure that when the scene loads this is not destroyed
    }
}
