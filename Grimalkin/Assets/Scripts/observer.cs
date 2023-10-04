using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;


public class observer : MonoBehaviour
{
    public class Observations{
        public Dictionary<string,int> total = new Dictionary<string,int>();
        public Dictionary<string,int> recent = new Dictionary<string,int>();
        int index = 0;
        int size;
        List<string>[] last;

        public Observations(int lastSize){
            size = lastSize;
            last = new List<string>[size];
            last[0] = new List<string>();
        }

        public void add(List<string> newObservation){
            if (last[index] != null){
                foreach (string info in last[index]){
                    recent[info] = recent[info] - 1;
                    if (recent[info] <= 0){
                        recent.Remove(info);
                    }
                }
            }
            
            last[index] = newObservation;
            foreach (string info in newObservation){
                recent.TryGetValue(info, out var count); 
                recent[info] = count + 1;

                total.TryGetValue(info, out count); 
                total[info] = count + 1;

            }
            index = (index + 1)% size;
        }

    }
    public Observations observations;

    public int recentSize;

    public RectTransform visionPointer;

    public Camera cam;

    public bool testing = true;

    public List<KeyValuePair<string,int>> test;

    public List<string> totalKeys;
    public List<int> totalValues;

    public List<string> recentKeys;
    public List<int> recentValues;

    // Start is called before the first frame update
    void Start()
    {
        observations = new Observations(recentSize);
    }

    public List<KeyValuePair<string,int>> getRecent(){
        List<KeyValuePair<string,int>> outputList = observations.recent.ToList();
        outputList.Sort((pair1,pair2) => pair2.Value.CompareTo(pair1.Value));
        return outputList;
    }
 

    void FixedUpdate()
    {   
        if (testing){
            if (Input.GetKeyDown(KeyCode.P))
            {

                Ray ray = cam.ScreenPointToRay(visionPointer.position);
                if (Physics.Raycast(ray, out var hit)) {
                    observable test = hit.collider.gameObject.GetComponent<observable>();
                    if(test != null){
                        observations.add(test.info);
                        Debug.Log(test.info[0]);
                    }
                }
            }
        } else {
                Ray ray = cam.ScreenPointToRay(visionPointer.position);
                if (Physics.Raycast(ray, out var hit)) {
                    observable test = hit.collider.gameObject.GetComponent<observable>();
                    if(test != null){
                        observations.add(test.info);
                    }
                }
        }


        totalKeys = new List<string>(observations.total.Keys);
        totalValues = new List<int>(observations.total.Values);

        recentKeys = new List<string>(observations.recent.Keys);
        recentValues = new List<int>(observations.recent.Values);


    }

    
}
