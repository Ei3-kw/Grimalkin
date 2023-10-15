using System.Collections.Generic;
using UnityEngine;

public class EyePositionRecorder : MonoBehaviour
{
    // List to store mouse positions
    public List<Vector3>  eyePositions = new List<Vector3>();
    
    

    // Update is called once per frame
    public void Update(){
        if (!CodeIsSet.codeIsSet && Is2DView.in2DView){
            // Get the current mouse position in world space
            Vector3 eyePosition = Input.mousePosition;


            // Add the mouse position to the list
            eyePositions.Add(eyePosition);
        }
    }
}
