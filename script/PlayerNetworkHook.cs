using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class PlayerNetworkHook : NetworkBehaviour
{
    [SyncVar]
    public string pname = "player";

    [SyncVar]
    public int teamIndex = -1;

    [SyncVar]
    public bool canMove;

    int activateChat = 0;
    public GameObject chatPointer;
    public GameObject scorePointer;
    private int Locker = 0;
    private int Locker_score = 0;
    public Transform cue;
    bool freeMode;
    private Vector3 oldPos;
    public GameObject cueBall;


    private void Start()
    {
        if (isServer)
        {
            canMove = true;
        }
        freeMode = true;

    }

    private void Update()
    {
    
        if (Locker == 0)
            chatpointing();


        if (isServer)
        {



            if (Locker_score == 0)
            {
                scorePointing();

            }
            else
            {
                if (teamIndex == -1)
                {
                    scorePointer.GetComponent<ScoreControl>().assignTeamIndex(this.gameObject);


                }
                else
                {

                    canMove = !(teamIndex == (scorePointer.GetComponent<ScoreControl>().getCurrentTeam()));

                }

            }

            if (!canMove)
            {

                //oldPos = transform.position;
                //this.GetComponent<Move>().enabled = canMove;
                //cueBall = GameObject.Find("Cue Ball(Clone)");

                if (freeMode)
                {
                    oldPos = transform.position;
                    this.GetComponent<Move>().enabled = canMove;

                    cueBall = GameObject.Find("Cue Ball(Clone)");
                    transform.position = cueBall.transform.position;
                    freeMode = false;
                }
            }
            else
            {
                //transform.position = oldPos;
                if (!freeMode)
                {
                    transform.position = oldPos;
                    this.GetComponent<Move>().enabled = canMove;
                    freeMode = true;
                }
            }


        }




        if (isClient)
        {
            if (hasAuthority)
            {


                if (teamIndex == 0)
                {
                    //Debug.Log("I play solid ball");
                }
                else
                {
                    //Debug.Log("I play stripe ball");
                }
            }

        }

    }

    void chatpointing()
    {
        GameObject go = GameObject.Find("chatMemory1");

        if (!go)
        {
            //Debug.Log("I can't find");
            return;
        }
        else
        {
            //Debug.Log(go.name);
            chatPointer = GameObject.Find("chatMemory1");
            if (hasAuthority)
                chatPointer.GetComponent<Chat>().playerPointer = this.gameObject;
            Locker = 1;
        }

        if (!hasAuthority) //if (!isLocalPlayer) 
        {
            return;
        }
        else
        {
            this.GetComponent<Camera>().enabled = true;
            this.GetComponent<MouseLook>().enabled = true;
            this.GetComponent<Move>().enabled = true;
            this.GetComponent<CharacterController>().enabled = true;
        }
    }



    void scorePointing()
    {
        GameObject go = GameObject.Find("scoreMemory(Clone)");

        if (!go)
        {
            //Debug.Log("I can't find");
            return;
        }
        else
        {
            //Debug.Log(go.name);
            scorePointer = go;

            Locker_score = 1;
        }

    }


    void OnConnectedToServer()
    {
    }

    [Command]
    public void CmdChatMess_clientToServer(string mess)
    {
        //Debug.Log("server received");
        RpcChatMess_serverToAllClient(mess);
    }


    [ClientRpc]
    public void RpcChatMess_serverToAllClient(string mess)
    {
        //Debug.Log("client received");
        chatPointer.GetComponent<Chat>().chatList.Add(mess);
        //chatPointer.GetComponent<Chat>().chatList.Add(pname + ": " + mess);
        
    }


}

