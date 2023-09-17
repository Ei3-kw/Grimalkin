using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHeatMap : MonoBehaviour
{
    

    public void showHeatMap()
    {
        // Load the specified scene when the object is clicked
        SceneManager.LoadScene("BonusTablet");
    }   
}