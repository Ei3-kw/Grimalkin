// using UnityEngine;

// public class AlarmPlayer : MonoBehaviour
// {
//     public AudioSource musicSource; // Assign your audio source in the Inspector

//     private void Awake()
//     {
//         // Ensure that this AudioManager persists across scenes
//         DontDestroyOnLoad(gameObject);

//         // Start playing the music if MusicManager.x is true
        
//     }

//     public void Update(){
//         Debug.Log(!Result.solved);
//         Debug.Log(!AlarmIsPlaying.alarmIsPlaying);

//         if ((!Result.solved)&&(!AlarmIsPlaying.alarmIsPlaying))
//         {
//             musicSource.Play();
//             AlarmIsPlaying.alarmIsPlaying = true;
//         }
//         else if ((Result.solved)&&(AlarmIsPlaying.alarmIsPlaying)){
//             musicSource.Stop();
//         }
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; // Assign your audio source in the Inspector

    private void Awake()
    {
        // Ensure that this AudioManager persists across scenes
        DontDestroyOnLoad(gameObject);
        // DontDestroyOnLoad(musicSource);

        

    }

    private void Start()
    {
        // Add debug logs to track scene transitions
       
        // musicSource.Play();
        //Debug.Log(AlarmIsPlaying.alarmIsPlaying);


    }

    private void Update()
    {
        // Debug.Log(!Result.solved);
        // Debug.Log(!AlarmIsPlaying.alarmIsPlaying);
        // Debug.Log("Loaded scene: " + SceneManager.GetActiveScene().name);

        // if(musicSource.isPlaying){
        //     Debug.Log("Music playing");

        // }
        if ((!Result.solved) && (!AlarmIsPlaying.alarmIsPlaying))
        {
            musicSource.Play();
            AlarmIsPlaying.alarmIsPlaying = true;
            Debug.Log("Music started");
        }
        else if ((Result.solved) && (AlarmIsPlaying.alarmIsPlaying))
        {
            musicSource.Stop();
            // Debug.Log("Music stopped");
        }
    }
}
