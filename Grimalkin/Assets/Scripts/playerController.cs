using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Vector2 camSens;
    public float moveSpeed;
    public float visMoveSpeed;
    public Vector2 visRatio; 
    public Transform head;
    public RectTransform visionPointer;
    public float rotationX;
    public float rotationY;
    public CharacterController character;
    public Camera cam;
    public bool simVision = true;
    Vector2 inputAxes;
    Vector2 visInputAxes;
    Vector2 screenSize;
    public Vector2 pointerPos;
    RaycastHit hit;

    Vector3 posFilter;

    public GameObject eye_pointer; //where the eye is looking
    public GameObject interaction_UI;
    // list of objects the player can interact with
    public GameObject computer;


    
    // Start is called before the first frame update
    private void Start()
    {
        // set the eye pointer to be half transparent
        Color newColor = new Color(1, 1, 1, 0.5f);
        eye_pointer.GetComponent<Image>().material.color = newColor;

        posFilter = new Vector3(1,0,1);
        screenSize = new Vector2(Screen.width,Screen.height);
        pointerPos = screenSize/2;
        Cursor.lockState = CursorLockMode.Locked;
        interaction_UI.SetActive(false); // turn the UI element off
    }
    private void Update()
    {
        if (simVision)
        {
            pointerPos.x = Mathf.Clamp(pointerPos.x + visInputAxes.x * visMoveSpeed * 100 * Time.deltaTime, 0, screenSize.x);
            pointerPos.y = Mathf.Clamp(pointerPos.y + visInputAxes.y * visMoveSpeed * 100 * Time.deltaTime, 0, screenSize.y);
            visionPointer.position = new Vector3(pointerPos.x ,pointerPos.y, 0 );
        }
        float mouseX = Input.GetAxis("Mouse X") * camSens.x * 1000 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * camSens.y * 1000 * Time.deltaTime;
        rotationX -= mouseY ;
        rotationX = Mathf.Clamp(rotationX, -60f, 60f) ;
        rotationY = Mathf.Clamp(((pointerPos.x/screenSize.x) -0.5f)* visRatio.x, -30f, 30f);
        head.localRotation = Quaternion.Euler(rotationX - Mathf.Clamp(((pointerPos.y/screenSize.y) -0.5f)* visRatio.y, -15f, 15f), rotationY, 0f );
        transform.Rotate(Vector3.up * mouseX);

        UpdateAxes();
        Vector3 move = inputAxes.x * transform.forward + inputAxes.y * transform.right;
        character.Move(move * moveSpeed * Time.deltaTime); 
        transform.position = new Vector3 (transform.position.x, 0, transform.position.z);

        //ray casting 
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     Debug.Log("sending out ray");
        //     Ray ray = cam.ScreenPointToRay(pointerPos);
        //     if (Physics.Raycast(ray, out hit)) {
        //         Debug.Log(hit.collider.gameObject.name + "was hit by my eyes");
        //     }
        // }


        // check what the player is looking at
        Ray looking_at = cam.ScreenPointToRay(pointerPos);
        if (Physics.Raycast(looking_at, out hit))
        {
            if (hit.collider.gameObject == computer) // if looking at the computer
            {
                Debug.Log(hit.collider.gameObject.name + "was registered");
                // show a a message on screen that the user can now interact
                TextMeshProUGUI interaction_text = interaction_UI.GetComponent<TextMeshProUGUI>();
                interaction_text.text = "Press [e] to interact with computer";
                interaction_UI.SetActive(true); // turn the UI element on

                // tell the computer it is beign looked at
                hit.collider.gameObject.GetComponent<computer_controler>().look_at();

                // set the eye pointer to be fully coloured
                Color newColor = new Color(1, 1, 1, 1);
                eye_pointer.GetComponent<Image>().material.color = newColor;
            }
            else // if they are not looking at any object of interest
            {
                interaction_UI.SetActive(false); // turn the UI element off
                
                // set the eye pointer to be half transparent
                Color newColor = new Color(1, 1, 1, 0.5f);
                eye_pointer.GetComponent<Image>().material.color = newColor;
            }
        }
        else // if they are not looking at any object at all
        {
            interaction_UI.SetActive(false); // turn the UI element off

            // set the eye pointer to be half transparent
            Color newColor = new Color(1, 1, 1, 0.5f);
            eye_pointer.GetComponent<Image>().material.color = newColor;
        }
    }

    private void UpdateAxes()
    {
        inputAxes = Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            inputAxes.x += 1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputAxes.x -= 1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputAxes.y -= 1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputAxes.y += 1;
        }
        if (simVision){
                visInputAxes = Vector2.zero;
            if(Input.GetKey(KeyCode.UpArrow)){
                visInputAxes.y += 1;
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                visInputAxes.y -= 1;
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                visInputAxes.x -= 1;
            }
            if(Input.GetKey(KeyCode.RightArrow)){
                visInputAxes.x += 1;
            }
        }
        
    }
}
