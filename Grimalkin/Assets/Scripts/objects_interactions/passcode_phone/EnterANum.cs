/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the buttons on the pin pad of the phone
 * - To communicate to relatent objects of the users pin input
 * 
 * Attached to objects in game scene:
 * - Each number button on the pin pad of the phone
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnterANum : MonoBehaviour
{
    // the number displayed on this button
    public int thisNum;

    // the object that will keep track of the pin
    public CheckPin pinChecker;

    /*
     * When the button is clicked
     * 
     * Add the respective number to the current user pin.
     */
    private void OnMouseDown()
    {
        // if we are in the phone and the code is not set yet
        if (Is2DView.in2DView && !CodeIsSet.codeIsSet){
            // add this number to the user entered pin
            pinChecker.numEntered.Add(thisNum);
        }
    }
}
