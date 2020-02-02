using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMessageSetter : MonoBehaviour
{
    public TextMeshProUGUI Message;
    // Start is called before the first frame update
    void Start() {
        Message.SetText("You successfully rebuilt civilization in " + PlayerPrefs.GetInt("days") + " days!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
