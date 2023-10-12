using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class door_hover_controler : MonoBehaviour
{

    public GameObject interaction_UI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        Debug.Log("tes");
        TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
        interaction_text.text = "Press [e] to interact with door";
        interaction_UI.SetActive(true); // turn the UI element on
    }

    void OnMouseExit()
    {
        Debug.Log("mosue off");
        interaction_UI.SetActive(false);
    }
}