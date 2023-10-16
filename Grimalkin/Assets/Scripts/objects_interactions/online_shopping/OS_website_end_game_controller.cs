/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control when online shopping should end
 * - It will be dispayed on the computer when the user is on the check out screen
 * - Will tell the user to click it if they want to exit the online shopping experience
 * - Will inform the computer when it is clicked and online shopping should end
 * 
 * Attached to objects in game scene:
 * - "end button" on computer (quit to desktop button on checkout)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS_website_end_game_controller : MonoBehaviour
{
    // the computer that this button is displayed on 
    public GameObject computer;

    /*
     * When the user clicks on this object.
     * 
     * Tell the computer that the user wants to end online shopping.
     */
    private void OnMouseDown()
    {
        computer.GetComponent<OS_computer_controller>().open_start_screen();
    }
}
