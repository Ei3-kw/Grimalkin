using UnityEngine;
using UnityEngine.SceneManagement;
    

public class switchScene : MonoBehaviour
{ 

    public void OnMouseDown()
    {
        // Debug.Log("down");
        SceneManager.LoadScene("Scenes/passcodeGameScenes/showing");
    }
}


