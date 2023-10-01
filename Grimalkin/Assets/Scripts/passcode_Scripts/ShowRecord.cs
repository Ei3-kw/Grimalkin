using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShowRecord : MonoBehaviour
{
    public GameObject circlePrefab; // Drag your circle prefab here in the Inspector
    public float moveSpeed = 2f;
    // public EyePositionTracker eyePositionTracker;
    public Camera mainCamera;
    public GameObject sphere;

    private List<Vector3> positions = new List<Vector3>();
    private int currentPositionIndex = 0;
 

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject); // Make this object persist between scenes
    // }
    void Start()
    {
        
        // Fill the positions list with Vector3 positions (you can populate it as needed)
        EyePositionRecorder eyePositionTracker = GameObject.Find("EyePositionRecorder").GetComponent<EyePositionRecorder>();
        Debug.Log("get");

        positions = eyePositionTracker.eyePositions;
        // Debug.Log(positions[0]);
        
        // Spawn the initial circle
        // SpawnCircleAtCurrentPosition();
        Vector3 testpos = new Vector3(0, 0, 0);
        // currentCircle = Instantiate(circlePrefab, testpos, Quaternion.identity);
        Debug.Log(positions.Count);
        sphere.SetActive(true);
        
          
    }

    void Update()
    {   
        

        
        // Check if the circle has reached the current position
        if (currentPositionIndex < positions.Count)
        {
             
            Vector3 posToMove = new Vector3(positions[currentPositionIndex].x, positions[currentPositionIndex].y,mainCamera.transform.position.z);
            // Debug.Log("???");
            posToMove = mainCamera.ScreenToWorldPoint(posToMove);
            sphere.transform.position = posToMove;
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // if(Physics.Raycast(ray, out RaycastHit raycastHit)) {
            //     sphere.transform.postion = raycastHit.point;
            // }
            // Vector3 newpos = positions[currentPositionIndex];
            
            currentPositionIndex++;
            
            
            // currentCircle.transform.position = new Vector3(1f, 0, 0);
            // if (currentPositionIndex < positions.Count){
            
            // currentCircle.transform.position = new Vector3(1f, 0, 0);
            // Debug.Log("done");
            // // currentCircle.transform.position = positions[currentPositionIndex];
            // currentPositionIndex++;
            // }
            

            // currentCircle.transform.position = new Vector3(0, 0, 0);
            // float step = moveSpeed * Time.deltaTime;
            // currentCircle.transform.position = Vector3.MoveTowards(
            //     currentCircle.transform.position, 
            //     positions[currentPositionIndex], 
            //     step);

            // Check if the circle has reached the current position
            // if (Vector3.Distance(currentCircle.transform.position, positions[currentPositionIndex]) < 0.001f)
            // {
            //     Destroy(currentCircle);

            //     // Move to the next position in the list
            //     currentPositionIndex++;

            //     // Check if we've reached the end of the list
            //     if (currentPositionIndex < positions.Count)
            //     {
            //         SpawnCircleAtCurrentPosition();
            //     }
            //     else
            //     {
            //         Debug.Log("All positions reached!");
            //     }
            // }
        }
    }

    // void SpawnCircleAtCurrentPosition()
    // {   
       
    //     currentCircle = Instantiate(circlePrefab, positions[currentPositionIndex], Quaternion.identity);

    // }
}
