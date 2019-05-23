using UnityEngine;
using UnityEngine.Networking;

public class objSpawn : NetworkBehaviour
{

    public GameObject ballPref;

    public override void OnStartServer()
    {

            var ball = (GameObject)Instantiate(ballPref, transform.position, transform.rotation);
            NetworkServer.Spawn(ball);
        
    }
}