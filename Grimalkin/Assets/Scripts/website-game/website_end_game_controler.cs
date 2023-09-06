using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class website_end_game_controler : MonoBehaviour
{

    public GameObject computer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        computer.GetComponent<computer_controler>().open_start_screen();
    }
}
