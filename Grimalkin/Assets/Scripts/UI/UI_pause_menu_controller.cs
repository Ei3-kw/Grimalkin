/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To control the pause menu screen UI
 * - This screen will show up when the [esc] key in game
 * - Will give the user options to quit to desktop, restart the game, or just resume
 * - Will allow the user to adjust the mouse sensitivity
 * 
 * Attached to objects in game scene:
 * - Pause Menu UI element
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_pause_menu_controller : MonoBehaviour
{
    // The script that will control the player object in scene
    public player_controller player_controller;

    // The camera sensitivity slider that will show up in the menu
    public Slider camSlider; 
    // The set of buttons on the pause menu screen
    public Button resetButton, quitButton, resumeButton;

    /* Start is called before the first frame update
     * 
     * Will be run when the pause menu screen is shown
     * We will display the buttons and define what happens when the button is pressed
     */
    void Start()
    {
        // button to restart the game from begining
        resetButton.onClick.AddListener(restart);

        // button to quit the game to desktop
        quitButton.onClick.AddListener(() => {Application.Quit();});

        // button to resume the game (i.e. exit pause menu)
        resumeButton.onClick.AddListener(() => 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
        });

        // define how the mouse sensitivity slider works
        // (slider up => higher sens, down => lower)
        camSlider.onValueChanged.AddListener(setCameraSensitivity);
    }

    /*
     * Set the mouse sensitivity
     * Should be used by the slider in the pause menu
     * 
     * value: the value we wish to set the mouse sensitivity to 
     */
    void setCameraSensitivity(float value){
        player_controller.camSens = value;
    }

    /*
     * Restart the game from the beginin by reloading the whole scene
     */
    void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
