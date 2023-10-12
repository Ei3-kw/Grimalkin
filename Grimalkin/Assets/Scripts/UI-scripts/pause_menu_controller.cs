using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class pause_menu_controller : MonoBehaviour
{
    public playerController playerController;
    public Slider camSlider;

    public Button resetButton, quitButton, resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(restart);
        quitButton.onClick.AddListener(() => {Application.Quit();});
        resumeButton.onClick.AddListener(() => 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
        });
        camSlider.onValueChanged.AddListener(setCameraSensitivity);
    }

    void setCameraSensitivity(float value){
        playerController.camSens = value;
    }


    void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        // // while in pause menu
        // if (Input.GetKeyDown("1")) // restart game
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
        // else if (Input.GetKeyDown("2")) // quit to desktop
        // {
        //     Application.Quit();
        // }
        // else if (Input.GetKeyDown("3")) // return to game
        // {
        //     gameObject.SetActive(false);
        // }
    }
}
