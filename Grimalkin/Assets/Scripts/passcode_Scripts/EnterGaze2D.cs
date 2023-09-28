using UnityEngine;

public class EnterGaze2D : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject phoneObject;
    public GameObject passcodePhone;
    public  GameObject showRecord;
    // public GameObject ShowRecord;

    public static Vector3 orginalCameraPosition;
    private void Start()
    {
     // Assuming the camera you want to move is the main camera.
    orginalCameraPosition = mainCamera.transform.position;
    // Debug.Log(Go2DView.orginalCameraPosition);
    
    }

    private void OnMouseDown(){
    // {   if (Is2DView.in2DView){
    Debug.Log("iii");
    Transform phoneTransform = phoneObject.transform; // Replace 'phoneObject' with your phone's GameObject reference.
    int cameraHeightAbovePhone = 6;
    Vector3 desiredCameraPosition = new Vector3(phoneTransform.position.x, phoneTransform.position.y + cameraHeightAbovePhone, phoneTransform.position.z);
    // Move the camera above the phone and look at it.
    // Vector3 desiredCameraPosition = new Vector3(transform.position.x, transform.position.y + cameraHeightAbovePhone, transform.position.z);
    Debug.Log(desiredCameraPosition);
    mainCamera.transform.position = desiredCameraPosition;
    mainCamera.transform.LookAt(phoneObject.transform.position);
    Is2DView.in2DView = true;
    passcodePhone.SetActive(true);  
    // ShowRecord showRecord = GameObject.Find("ShowRecord").GetComponent<ShowRecord>();
    // GameObject showRecord = GameObject.Find("ShowRecord");
    showRecord.SetActive(true);  

    // }

    }
}
