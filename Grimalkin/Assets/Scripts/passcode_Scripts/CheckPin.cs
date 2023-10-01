using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPin : MonoBehaviour
{
    public  GameObject ShowRecord;
    public List<int> numEntered = new List<int>();
    public List<int> correctPin = new List<int>();
    public GameObject app;
   
    // Start is called before the first frame update
    void Start()
    {
        correctPin.Add(3);
        correctPin.Add(5);
        correctPin.Add(1);
        correctPin.Add(2);
    }

    // Update is called once per frame
    void Update()
    {   
        // Debug.Log(Is2DView.in2DView&&!CodeIsSet.codeIsSet);
        // bool pinIsCorrect = true;
        bool pinIsCorrect = numEntered.SequenceEqual(correctPin);
      
        // Debug.Log(numEntered.Count);
        if(!CodeIsSet.codeIsSet && !pinIsCorrect && numEntered.Count==4){
            numEntered.Clear();
        }
        if (!CodeIsSet.codeIsSet && pinIsCorrect){
            // Debug.Log("COrrect pin");
            CodeIsSet.codeIsSet = true;
            app.SetActive(true);
            // ShowRecord.SetActive(true);
        }
    }
}
