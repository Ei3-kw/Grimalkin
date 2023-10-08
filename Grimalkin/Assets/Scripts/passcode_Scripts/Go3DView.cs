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

    public GameObject ShowRecord;

    public bool demo_mode = false;
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
        app.SetActive(false);
        ShowRecord.SetActive(true);


        // Hide the TextMeshPro text
        // textMeshPro.gameObject.SetActive(false);

        // Now, change the camera position

    }
   
}
