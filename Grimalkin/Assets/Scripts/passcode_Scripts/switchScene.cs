using UnityEngine;
using UnityEngine.SceneManagement;
    

public class SwitchScene : MonoBehaviour
{ 

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Scenes/passcodeGameScenes/Showing");
    }
}


