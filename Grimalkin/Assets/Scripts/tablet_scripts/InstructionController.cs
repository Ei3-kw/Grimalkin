using UnityEngine;
using TMPro;

public class TextMeshProVisibilityController : MonoBehaviour
{
    public TextMeshProUGUI instruction; // Reference to your TextMeshPro component

    // Function to make the TextMeshPro text visible
    public void Update(){
        if(!Result.solved){
            instruction.enabled = true;
        }
        else{
            instruction.enabled = false;

        }
    }
    
}
