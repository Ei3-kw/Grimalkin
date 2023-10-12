using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class website_start_game_controler : MonoBehaviour
{
    public GameObject computer; // computer that the icon is displayed on

    /*
    when the user click on the rainforest icon, start the website up
    */
    private void OnMouseDown()
    {
        computer.GetComponent<computer_controler>().start_game();
    }

    /*
    when the users cursor is on the object change its colour to
    notify them so they know what to click
    */
    void OnMouseEnter()
    {
        // turn half transparent (alpha = 0.5)
        Color newColor = new Color(1, 1, 1, 0.25f); 
        gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
    }

    /*
    when the users cursor is off the object change its colour to
    notify them so they know what to click
    */
    void OnMouseExit()
    {
        // turn half transparent (alpha = 0.5)
        Color newColor = new Color(1, 1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
    }
}
