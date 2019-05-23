using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxSwitch : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {		
	}	

    void ChatOnOff()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {            
            this.GetComponent<Chat>().enabled = !this.GetComponent<Chat>().enabled;           
        }
    }

    // Update is called once per frame
    void Update ()
    {
        ChatOnOff();
    }
}
