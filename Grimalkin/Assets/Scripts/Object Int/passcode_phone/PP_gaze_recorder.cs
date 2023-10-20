/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To keep track of the "gaze" (mouse) positions as the user
 *   enters their pin into the phone
 * 
 * Attached to objects in game scene:
 * - Pin app on the users phone
 */

using System.Collections.Generic;
using UnityEngine;

public class PP_gaze_recorder : MonoBehaviour
{
    // List to store mouse positions of where the user is "looking"
    public List<Vector3>  eyePositions = new List<Vector3>();
    
    /*
     * Update is called once per frame
     * 
     * Each frame we record where the user was looking and add it to the 
     * list of positions looked
     */
    public void Update()
    {
        // if we are in the phone and the code is not set yet
        if (!PP_pin_parameters.codeIsSet && PP_phone_parameters.in2DView){
            // Get the current mouse position in world space
            Vector3 eyePosition = Input.mousePosition;

            // Add the mouse position to the list
            eyePositions.Add(eyePosition);
        }
    }
}
