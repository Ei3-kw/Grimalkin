using UnityEngine;

public class Go2DView : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject phoneObject;
    public GameObject passcodePhone;
    public static Vector3 orginalCameraPosition;

    public Transform newCamPos;

    public GameObject player;
    public GameObject optional_UI;
    public GameObject currency_UI;


    private void Start()
    {
        // Assuming the camera you want to move is the main camera.
        gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it

        // Debug.Log(Go2DView.orginalCameraPosition);

    }

    public void look_at()
    {   
        if (!Is2DView.in2DView && Input.GetKeyDown("e")){

            gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it


            // disable all player controls and excess UI
            orginalCameraPosition = mainCamera.transform.position;
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);
            currency_UI.SetActive(false); /////// TODO ? 
            Transform phoneTransform = phoneObject.transform; // Replace 'phoneObject' with your phone's GameObject reference.

            //int cameraHeightAbovePhone = 6;
            Vector3 desiredCameraPosition = new Vector3(newCamPos.position.x, newCamPos.position.y, newCamPos.position.z);
            // Move the camera above the phone and look at it.
            // Vector3 desiredCameraPosition = new Vector3(transform.position.x, transform.position.y + cameraHeightAbovePhone, transform.position.z);
            Debug.Log(desiredCameraPosition);
            mainCamera.transform.position = desiredCameraPosition;
            mainCamera.transform.LookAt(phoneObject.transform.position);
            Is2DView.in2DView = true;
            passcodePhone.SetActive(true);

            // disable all player controls and excess UI
            player.GetComponent<playerController>().enabled = false;
            optional_UI.SetActive(false);
            currency_UI.SetActive(false); /////// TODO ? 

            // unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

    }
}
