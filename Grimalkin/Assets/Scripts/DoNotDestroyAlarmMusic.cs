using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyAlarmMusic : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void Awake(){
        GameObject[] musicObj  = GameObject.FindGameObjectsWithTag("alarmMusic");
        if (musicObj.Length>1){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

}
