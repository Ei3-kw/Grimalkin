using UnityEngine;
using UnityEngine.SceneManagement;
    

public class switchScene : MonoBehaviour
{ 

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Scenes/passcodeGameScenes/Showing");
    }
}


