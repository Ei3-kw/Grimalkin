using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopButtonController : MonoBehaviour
{
    public GameObject heatmap;
    public GameObject tablet;
    public TextMeshPro ipad_text;
    public GameObject alarm_sound;

    public GameObject player;

    public TextMeshProUGUI subtitles;

    private bool clicked = false;

    public bool demo_mode = false;

    private void Update()
    {

    }

    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("PRESSED YOOO");

        // turn of music
        alarm_sound.SetActive(false);

        if (clicked == false)
        {
            clicked = true;
            StartCoroutine(updating_process());
        }


        

    }

    private IEnumerator updating_process()
    {
        ipad_text.text = "Good Morning!";
        yield return new WaitForSeconds(2); // wait
        ipad_text.text = "Updating.";
        subtitles.text = "Updating...? Really..? now..?";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating..";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating...";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating.";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Update Complete!";
        subtitles.text = "Finally!";

        yield return new WaitForSeconds(1); // wait

        if (demo_mode)
        {
            // Load the specified heatmap when the object is clicked
            heatmap.GetComponent<HeatMapGenerator>().show_heat_map();
            yield return new WaitForSeconds(5); // wait

        }

        // progress story
        // communitcate back to story
        tablet.GetComponent<tablet_controller>().update_done();
        player.GetComponent<story_controller>().alarm_off();

        yield return null;

    }
}
