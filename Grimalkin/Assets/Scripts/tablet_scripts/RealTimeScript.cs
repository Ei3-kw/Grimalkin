using UnityEngine;
using TMPro;
using System;

public class RealTimeText : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component

    private void Start()
    {
        // Make sure you have a reference to the TextMeshPro component in the Inspector
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned in the Inspector.");
            return;
        }

        // Update the time initially and then every minute
        UpdateTime();
        InvokeRepeating("UpdateTime", 0f, 60f); // Update every 60 seconds (1 minute)
    }

    private void UpdateTime()
    {
        // Get the current time and format it as a string with "HH:mm" format
        string currentTime = DateTime.Now.ToString("HH:mm");

        // Update the TextMeshPro component with the current time
        textMeshPro.text = currentTime;
    }
}
