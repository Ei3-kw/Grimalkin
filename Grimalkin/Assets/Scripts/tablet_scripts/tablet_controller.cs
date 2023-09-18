using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablet_controller : MonoBehaviour
{
    public Transform camera_pos_for_game;
    public GameObject player_cam;
    public Transform player_cam_pos;

    public GameObject alarm_game;

    public GameObject optional_UI;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        alarm_game.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        look_at();
    }

    // Will be called when the opbject is being looked at by the player
    public void look_at()
    {
        // if the user is not in game yet
        // and the user has triggered the game to start
        Debug.Log("test");
        if (Input.GetKeyDown("e")) // TODO: check if user is in range of computer
        {
            Debug.Log("in");



            StartCoroutine(turn_on());
        }
    }

    private IEnumerator turn_on()
    {
        // once player starts for the first time glow ends
        gameObject.GetComponent<Outline>().enabled = false; // turn off the glow when looked at it

        // disable the movment script and UI
        player.GetComponent<playerController>().enabled = false;
        optional_UI.SetActive(false);

        // move the camera into position
        // Calculate the position to move the camera to
        Vector3 targetPosition = camera_pos_for_game.position;
        Quaternion targetRotation = camera_pos_for_game.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // Interpolate the camera's position toward the target position
        camera.position = targetPosition;
        camera.rotation = targetRotation;

        // unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(0.5f); // wait
        alarm_game.SetActive(true);
        yield return null;

    }

    private IEnumerator turn_off()
    {
        alarm_game.SetActive(false);
        yield return new WaitForSeconds(0.5f); // wait

        alarm_game.SetActive(false);
        // exit the tablet
        // relock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // move the camera back into player
        // Calculate the position to move the camera to
        Vector3 targetPosition = player_cam_pos.position;
        Quaternion targetRotation = player_cam_pos.rotation;
        Transform camera = player_cam.GetComponent<Transform>();

        // Interpolate the camera's position toward the target position
        camera.position = targetPosition;
        camera.rotation = targetRotation;

        // re enable the movment script and UI
        player.GetComponent<playerController>().enabled = true;
        optional_UI.SetActive(true);
        yield return null;

    }


    public void update_done()
    {
        Debug.Log("Done!!!!!");
        StartCoroutine(turn_off());
    }
}
