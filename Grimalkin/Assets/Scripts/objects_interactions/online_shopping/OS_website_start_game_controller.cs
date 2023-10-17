/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control when online shopping should start
 * - It will be dispayed on the computer when the user first opens it 
 * - Will inform the computer when it is clicked and online shopping should start
 * 
 * Attached to objects in game scene:
 * - "start button" on computer (rainforest online shopping website icon)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS_website_start_game_controller : MonoBehaviour
{
    public GameObject computer; // computer that the icon is displayed on

    /*
     * when the user click on the rainforest icon, start the website up
     */
    private void OnMouseDown()
    {
        // inform the computer we wish to start
        computer.GetComponent<OS_computer_controller>().start_game();
    }

    /*
     * when the users cursor is on the object change its colour to
     * notify them so they know what to click.
     */
    void OnMouseEnter()
    {
        // turn icon mostly transparent (alpha = 0.25)
        Color newColor = new Color(1, 1, 1, 0.25f); 
        gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
    }

    /*
     * when the users cursor is off the object change its colour to
     * notify them so they know what to click
     */
    void OnMouseExit()
    {
        // turn fully opaque (alpha = 1)
        Color newColor = new Color(1, 1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
    }
}
