using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHeatMap : MonoBehaviour
{
    public GameObject heatmap;

    public void showHeatMap()
    {
        // Load the specified scene when the object is clicked
        heatmap.GetComponent<HeatMapGenerator>().show_heat_map();
    }   
}