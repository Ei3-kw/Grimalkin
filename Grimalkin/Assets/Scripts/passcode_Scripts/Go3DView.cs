using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Go3DView : MonoBehaviour
{
    
    public Camera myCamera;
    public GameObject phoneObject;
    public float textDisplayTime = 1.5f; 
    public TextMeshPro message;
    public GameObject passcodePhone;
    public GameObject app;

    public GameObject player;
    public GameObject optional_UI;
    public GameObject currency_UI;


    // Start is called before the first frame update
    // public void OnMouseDown()
    // {
    //     myCamera.transform.position = Go2DView.orginalCameraPosition;
    //     myCamera.transform.LookAt(phoneObject.transform.position);
    //     Debug.Log("confimred");
    // }
    private void OnMouseDown()
    {
        // Debug.Log("yess");  
        StartCoroutine(ShowTextAndMoveCamera());
    }

    private IEnumerator ShowTextAndMoveCamera()
    {
        message.SetText("Your delivery is confirmed!"); // Set the TextMeshPro text
        // message.gameObject.SetActive(true); // Show the TextMeshPro text

        yield return new WaitForSeconds(textDisplayTime); // Wait for the specified time

        // Hide the TextMeshPro text
        // textMeshPro.gameObject.SetActive(false);

        // Now, change the camera position
        myCamera.transform.position = Go2DView.orginalCameraPosition;
        myCamera.transform.LookAt(phoneObject.transform.position);
        passcodePhone.SetActive(false);  
        app.SetActive(false);

        // disable all player controls and excess UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        currency_UI.SetActive(true); /////// TODO ? 

        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<story_controller>().code_entered();
    }
   
}
