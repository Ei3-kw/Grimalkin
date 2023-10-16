/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To keep display to the user what numbers they have already
 *   entered onto the key pad
 * 
 * Attached to objects in game scene:
 * - Pin app on the users phone
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PP_keypad_controller : MonoBehaviour
{
    public TextMeshPro input1; // first input to key pad
    public TextMeshPro input2; // second input to key pad
    public TextMeshPro input3; // third input to key pad
    public TextMeshPro input4; // fourth input to key pad
    public PP_pin_controller pinChecker; // object that keeps track of the pin

    /*
     * Update is called once per frame
     * 
     * We will update the pin entered on the top of the screen progressivly 
     * as the user enters their pin. 
     * 
     * The top of the phone screen will reflect the users inputs
     */
    void Update()
    {
        // if user has entered the N digits, then display the first N digits
        if(pinChecker.numEntered.Count==1){
            input1.text = pinChecker.numEntered[0].ToString();
        }
        else if(pinChecker.numEntered.Count==2){
            input2.text = pinChecker.numEntered[1].ToString();
        }
        else if(pinChecker.numEntered.Count==3){
            input3.text = pinChecker.numEntered[2].ToString();
        }
        else if(pinChecker.numEntered.Count==4){
            input4.text = pinChecker.numEntered[3].ToString();
        }
        // If they have entered 0 digits, then display empty for all of them
        else {
            input1.text = "_";
            input2.text = "_";
            input3.text = "_";
            input4.text = "_";
        }
    }
}
