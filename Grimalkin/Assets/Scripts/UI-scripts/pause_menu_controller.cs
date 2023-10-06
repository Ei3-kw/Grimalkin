using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // while in pause menu
        if (Input.GetKeyDown("1")) // restart game
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown("2")) // quit to desktop
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown("3")) // return to game
        {
            gameObject.SetActive(false);
        }
    }
}
