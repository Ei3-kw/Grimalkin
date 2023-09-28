using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInput : MonoBehaviour
{
    public TextMeshPro input1;
    public TextMeshPro input2;
    public TextMeshPro input3;
    public TextMeshPro input4;
    public CheckPin pinChecker;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pinChecker.numEntered.Count==1){
            // Debug.Log("input");
            input1.text = pinChecker.numEntered[0].ToString();
        }
        else if(pinChecker.numEntered.Count==2){
            // Debug.Log("input");
            input2.text = pinChecker.numEntered[1].ToString();
        }
        else if(pinChecker.numEntered.Count==3){
            // Debug.Log("input");
            input3.text = pinChecker.numEntered[2].ToString();
        }
        else if(pinChecker.numEntered.Count==4){
            // Debug.Log("input");
            input4.text = pinChecker.numEntered[3].ToString();
        }
        else {
            input1.text = "_";
            input2.text = "_";
            input3.text = "_";
            input4.text = "_";


        }
        
    }
}
