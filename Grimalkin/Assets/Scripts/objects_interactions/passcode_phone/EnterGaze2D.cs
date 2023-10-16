/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - to keep track of the original camera position after the user enters the 
 *   pin entering interaction
 * 
 * Attached to objects in game scene:
 * - Phone object that you enter pin into
 */


using UnityEngine;

public class EnterGaze2D : MonoBehaviour
{
    // camera that the user looks through
    public Camera mainCamera;

    // the position of the camera in the world before the iteraction started
    public static Vector3 orginalCameraPosition;

    /*
     * Start will be called before the first frame update
     * 
     * Save the original camera position
     */
    private void Start()
    {
        orginalCameraPosition = mainCamera.transform.position;
    }
}
