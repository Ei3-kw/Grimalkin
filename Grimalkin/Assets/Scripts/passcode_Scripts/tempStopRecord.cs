using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempStopRecord : MonoBehaviour
{   
    public  GameObject ShowRecord;
    public void OnMouseDown(){
        CodeIsSet.codeIsSet = true;
        ShowRecord.SetActive(true);
        Debug.Log("recording stops");
    }
}
