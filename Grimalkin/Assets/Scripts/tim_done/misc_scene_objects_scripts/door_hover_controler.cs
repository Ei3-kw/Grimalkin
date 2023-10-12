/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the text prompt that will inform the user that they can 
 *   open the door objects
 * 
 * Attached to objects in game scene:
 * - Any door type onject
 * 
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class door_hover_controler : MonoBehaviour
{
    public GameObject interaction_UI; // UI that displays the door interaction message

    /* 
     * Turn on the UI element when looking at door to inform the user that
     * they can open the door by pressing [e]
     */
    void OnMouseOver()
    {
        // get the text element of the UI so we can modify the text
        TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
        interaction_text.text = "Press [e] to interact with door";
        interaction_UI.SetActive(true); // turn the UI element on
    }

    /* 
     * Turn on the UI element off when no longer looking at door to inform the user that
     * they can no longer open the door by pressing [e]
     */
    void OnMouseExit()
    {
        interaction_UI.SetActive(false); // turn UI element off
    }
}