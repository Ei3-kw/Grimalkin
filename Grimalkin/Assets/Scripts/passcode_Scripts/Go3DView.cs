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
        message.SetText("Your delivery is <color=green>confirmed!</color>"); // Set the TextMeshPro text
        // message.gameObject.SetActive(true); // Show the TextMeshPro text

        yield return new WaitForSeconds(textDisplayTime); // Wait for the specified time
        

        if (demo_mode)
        {
            ShowRecord.GetComponent<ShowRecord>().demo_mode = true;
            ShowRecord.SetActive(true);
            Debug.Log("ACTIVATEED ");
            app.SetActive(false);

        }
        else 
        {
            CodeIsSet.codeIsSet = false;
            EyePositionRecorder eyePositionTracker = GameObject.Find("EyePositionRecorder").GetComponent<EyePositionRecorder>();
            eyePositionTracker.eyePositions.Clear();

            myCamera.transform.position = Go2DView.orginalCameraPosition;
            myCamera.transform.LookAt(phoneObject.transform.position);
            passcodePhone.SetActive(false);
            app.SetActive(false);

            // disable all player controls and excess UI
            player.GetComponent<playerController>().enabled = true;
            optional_UI.SetActive(true);

            Is2DView.in2DView = false;
            // lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<story_controller>().code_entered();

        }


        // Hide the TextMeshPro text
        // textMeshPro.gameObject.SetActive(false);

        // Now, change the camera position

    }

}
