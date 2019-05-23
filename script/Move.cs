using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Move : NetworkBehaviour {


    public Camera fpCam;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;


    public float nextJump = 0.5F;
    public float jumpDelta = 0.5F;
    public float myTime = 0.0F;

    //public Text textB;

    public GameObject prefBall;
    void ChangeView()// change player view between viewmodes
    {
        
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Jump") && myTime > nextJump)
        {
            nextJump = myTime + jumpDelta;
            this.GetComponent<Camera>().enabled = !this.GetComponent<Camera>().enabled;
            fpCam.GetComponent<Camera>().enabled = !this.GetComponent<Camera>().enabled; 
            nextJump = nextJump - myTime;
            myTime = 0.0F;
        }
    }

    void MoveByInput()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            //if (Input.GetButton("Jump"))
            // moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {

        MoveByInput();
        ChangeView();
        
    }
}
