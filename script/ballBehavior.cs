using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ballBehavior : NetworkBehaviour {

    public int ballType; //0,1,2,3 for BLANK, SOLID, STRIPE, 8-ball
    public float goalY; //terrain designer will decide the Z of goal
    public int currentTeam;
    public GameObject scoreBoard;
    public GameObject scorePointer;
    private bool isOnTable;
    private bool Locker = false;
    public GUIText WinnerText;
    public GUIText LoserText;
    public GameObject ballPrefab;
    public Vector3 middle;
    public Quaternion rotation;
    //private bool Win;
    //private book Lose;

    private Vector3 originPos;

	// Use this for initialization
	void Start () {
        //middle = new Vector3(0, goalY, 0);
        isOnTable = true;
      //  Win = false;
      //  Lose = false;
        WinnerText.text = "";
        LoserText.text = "";
        scoreBoard = GameObject.Find("scoreControl");
        goalY = transform.position.y;

        originPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;
        UpdateCurrentTeam();
        CheckBallIn();
	}


    void UpdateCurrentTeam()
    {
        if (!Locker)
        {
            GameObject go = GameObject.Find("scoreMemory(Clone)");

            if (!go)
            {
                //Debug.Log("I can't find scoreMemory");
                return;
            }
            else
            {
                //Debug.Log(go.name);
                scorePointer = go;

                Locker = true;
            }
        }

        
    }


    void CheckBallIn()
    {
        if ((goalY - transform.position.y > 2.0f) && (isOnTable))
        {
            Debug.Log("Ball is in hole");
            isOnTable = false;

            if (ballType == 0)
            {
                //Set location of BLANK ball to the center
                this.transform.position = originPos;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                isOnTable = true;
                SwitchTeam();

                //ballPrefab.transform.SetPositionAndRotation(middle, rotation);
                //SwitchTeam();
            } else if (ballType == 3)
            {
                
                if (scorePointer.GetComponent<ScoreControl>().getScoreTeam(currentTeam) == 7)
                {
                    //currentTeam win
                    Debug.Log("Current Team win");
                    WinnerText.text = "You Won!";
                    //Win = true;
                    //break;
                } else
                {
                    //currentTeam lose
                    Debug.Log("Current Team lose");
                    LoserText.text = "You Lost.";
                    //Lose = true;
                    //break;
                }


            } else
            {
                if (ballType % 2 == currentTeam)
                {
                    //currentTeam gain 1 point and keep going
                    scorePointer.GetComponent<ScoreControl>().addScoreTeam(currentTeam);

                } else
                {
                    scorePointer.GetComponent<ScoreControl>().addScoreTeam(1-currentTeam);
                    SwitchTeam();
                }

            }





        }
    }

    
    public void SwitchTeam()
    {
        if (!Locker)
            return;

        scorePointer.GetComponent<ScoreControl>().switchTeam();
       
    }

}
