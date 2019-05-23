using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreControl : NetworkBehaviour
{


    [SyncVar]
    public int scoreSolid = 0;

    [SyncVar]
    public int scoreStripe = 0;

    [SyncVar]
    public int currentTeam = 0;

    private int countSolidPlayer = 0;

    public GameObject playerPointer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            //testScore();
            secretButton();
        }
    }

    void testScore()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            scoreSolid++;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            scoreStripe--;
        }
    }

    public void switchTeam()
    {
        currentTeam = 1 - currentTeam;
        //Debug.Log(currentTeam.ToString());
    }

    public int getScoreTeam(int team)
    {
        if (team == 0)
        {
            return scoreSolid;
        }
        else
        {
            return scoreStripe;
        }
    }

    public int getCurrentTeam()
    {
        return currentTeam;
    }

    public void addScoreTeam(int team)
    {
        if (team == 0)
        {
            scoreSolid++;
        }
        else
        {
            scoreStripe++;
        }
    }

    public int getAssignedTeam()
    {
      
        if (countSolidPlayer < 2)
        {
            countSolidPlayer++;
            return 0;
        }
        else
        {
            return 1;
        }
       
    }

    public void assignTeamIndex(GameObject playerPointer)
    {
        /*
        if (countSolidPlayer < 2)
        {
            countSolidPlayer++;
            playerPointer.GetComponent<PlayerNetworkHook>().teamIndex = 0;
            return;
        }
        else
        {
            playerPointer.GetComponent<PlayerNetworkHook>().teamIndex = 1;
            return;
        }
        */
        playerPointer.GetComponent<PlayerNetworkHook>().teamIndex = ((countSolidPlayer++) % 2);
        return;
    }

    void secretButton()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
                switchTeam();
        }
    }

}
