using System.Collections.Generic;
using UnityEngine;

public class EyePositionRecorder : MonoBehaviour
{
    // List to store mouse positions
    public List<Vector3>  eyePositions = new List<Vector3>();
    
    
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject); // Make this object persist between scenes
    }

    // Update is called once per frame
    public void Update(){
        //Debug.Log(eyePositions.Count);
        if (!CodeIsSet.codeIsSet && Is2DView.in2DView){
        // Get the current mouse position in world space
            // Vector3 eyePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // Vector3 eyePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 eyePosition = Input.mousePosition;


            // Add the mouse position to the list
            eyePositions.Add(eyePosition);
        }
    }
    // void OnMouseDown()
    // {  
    //     if (!CodeIsSet.codeIsSet && Is2DView.in2DView){
    //     // Get the current mouse position in world space
    //         // Vector3 eyePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //         // Vector3 eyePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         Vector3 eyePosition = Input.mousePosition;


    //         // Add the mouse position to the list
    //         eyePositions.Add(eyePosition);
    //     }
        
    // }

    // Function to return the list of mouse positions
    // public List<Vector3> GetEyePositions()
    // {
    //     return eyePositions;
    // }
}
