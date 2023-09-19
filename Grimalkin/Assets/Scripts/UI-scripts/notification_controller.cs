using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class notification_controller : MonoBehaviour
{
    public TextMeshProUGUI notif_text;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_notif(string message)
    {
        Debug.Log("notif set");
        gameObject.SetActive(true);
        notif_text.text = message;
    }

    public void remove_notif()
    {
        Debug.Log("ntoif removed");
        gameObject.SetActive(false);

    }
}
