using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Chat : MonoBehaviour
{
    private string curMess;
    public List<string> chatList = new List<string>();
    public GameObject playerPointer;

    private void Start()
    {
        //Debug.Log(isLocalPlayer);
        //Debug.Log(isServer);
    }

    // Update is called once per frame
    void Update()
    {
        if (chatList.Count > 5)
        {
            chatList.RemoveAt(0);
        }
    }


    private void OnGUI()
    {
        //layout the button
        using (var areaScope = new GUILayout.AreaScope(new Rect(Screen.width -150, Screen.height*2 /3, 100, 1000)))  //(Screen.width - 130, 10, 100, 1000)
        {            
            curMess = GUILayout.TextField(curMess);
            GUI.color = Color.yellow;

            if (GUILayout.Button("Send"))
            {
                if (!string.IsNullOrEmpty(curMess.Trim()))
                {
                    playerPointer.GetComponent<PlayerNetworkHook>().CmdChatMess_clientToServer(curMess); //LNWHook put in the playInfo
                    curMess = string.Empty;
                }
            }            
        }
        //display list of string in the scope
        using (var areaScope = new GUILayout.AreaScope(new Rect(10, Screen.height*2 /3, Screen.width/2, 1000)))  //(Screen.width - 130, 10, 100, 1000)
        {
            foreach (string st in chatList)
            {
                GUILayout.Label(st);
            }
        }
    }
}
