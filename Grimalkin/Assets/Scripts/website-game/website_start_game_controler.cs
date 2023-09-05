using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class website_start_game_controler : MonoBehaviour
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
        computer.GetComponent<computer_controler>().start_game();
        Debug.Log("button rpesd");
    }
}
