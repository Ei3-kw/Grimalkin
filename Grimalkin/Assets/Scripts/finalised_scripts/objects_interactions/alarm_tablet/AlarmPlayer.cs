/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the alarm music player
 * - The music will be played when instructed and the alarm is not yet turned on
 * - THe music will be stoped once the user turns it off
 * 
 * Attached to objects in game scene:
 * - Tablet that has the alarm playing
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; // The alarm music that will be playing

    /*
     * Will be called every frame
     * 
     * We continue to check if the alarm has been turned off yet
     * If it has be will stop the music, if not we continue to play
     */
    private void Update()
    {
        // if the alarm has not been turned off yet
        if ((!Result.solved) && (!AlarmIsPlaying.alarmIsPlaying))
        {
            musicSource.Play(); // keep playing the alarm
            AlarmIsPlaying.alarmIsPlaying = true;
        }
        // if the alarm has been turned off
        else if ((Result.solved) && (AlarmIsPlaying.alarmIsPlaying))
        {
            musicSource.Stop(); // stop the alarm
        }
    }
}
