/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To make sure the instructions for the alarm interaction are shown
 *   when needed
 * 
 * Attached to objects in game scene:
 * - Instructions for alarm interaction
 */

using UnityEngine;
using TMPro;

public class TextMeshProVisibilityController : MonoBehaviour
{
    // text that displays the instructions for the alarm game
    public TextMeshProUGUI instruction;

    /*
     * Update is called every frame
     * 
     * If the alarm has been turned off we can turn off the instructions
     * since there is no more user input required
     */
    public void Update()
    {
        if(!AT_stop_button_parameters.solved) // if the alarm is on
        {
            instruction.enabled = true;
        }
        else // if the alarm is off (turned off successfully by user)
        {
            instruction.enabled = false;
        }
    }
}
