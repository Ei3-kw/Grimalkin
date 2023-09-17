using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopButtonController : MonoBehaviour
{
    public GameObject heatmap;
    public TextMeshPro ipad_text;

    private bool clicked = false;

    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("PRESSED YOOO");
        Result.solved = true;

        if (clicked == false)
        {
            StartCoroutine(updating_process());
        }


        

    }

    private IEnumerator updating_process()
    {
        ipad_text.text = "Good Morning!";
        yield return new WaitForSeconds(2); // wait
        ipad_text.text = "Updating.";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating..";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating...";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating.";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating..";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating...";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating.";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating..";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Updating...";
        yield return new WaitForSeconds(1); // wait
        ipad_text.text = "Update Complete!";

        yield return new WaitForSeconds(3); // wait
        // Load the specified heatmap when the object is clicked
        heatmap.GetComponent<HeatMapGenerator>().show_heat_map();

        yield return null;
    }
}
