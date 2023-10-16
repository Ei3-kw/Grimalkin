/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To check if the user has entered the correct pin when 
 *   trying to access their phone
 * 
 * Attached to objects in game scene:
 * - Pin app on the users phone
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PP_pin_controller : MonoBehaviour
{
    public  GameObject ShowRecord;
    public List<int> numEntered = new List<int>();
    public List<int> correctPin = new List<int>();
    public GameObject app;

    /*
     * Start is called before the first frame update.
     * 
     * set the correct pin to be 3512.
     */
    void Start()
    {
        correctPin.Add(3);
        correctPin.Add(5);
        correctPin.Add(1);
        correctPin.Add(2);
    }

    /*
     * Update is called once per frame
     * 
     * Each frame we will check if either:
     * - The pin is correct -> then open the confirm order app
     * - The pin is incorrect -> reset the entered pin and make them enter again
     * 
     * If they have not entered a full pin yet, do nothing
     */
    void Update()
    {   
        bool pinIsCorrect = numEntered.SequenceEqual(correctPin);
      
        // if they have entered a full pin (4 numbers) and it is wrong
        if(!PP_pin_parameters.codeIsSet && !pinIsCorrect && numEntered.Count==4){
            numEntered.Clear();
        }
        // if they have entered a full pin 
        if (!PP_pin_parameters.codeIsSet && pinIsCorrect){
            numEntered.Clear();
            PP_pin_parameters.codeIsSet = true;

            // let them confirm their order
            app.SetActive(true);
        }
    }
}
