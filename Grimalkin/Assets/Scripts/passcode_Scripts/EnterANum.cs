using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnterANum : MonoBehaviour
{
    public int thisNum;
    public CheckPin pinChecker;


    // Start is called before the first frame update

   
    // Update is called once per frame
    private void OnMouseDown()
    {
        // Debug.Log("pressed");


        
        if (Is2DView.in2DView && !CodeIsSet.codeIsSet){
            // Debug.Log("Centered");
            pinChecker.numEntered.Add(thisNum);
            
        }
    }
}
