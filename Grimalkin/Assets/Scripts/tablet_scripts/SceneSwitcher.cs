using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad;

    private void OnMouseDown()
    {
        // Load the specified scene when the object is clicked
        SceneManager.LoadScene(sceneToLoad);
    }   
}