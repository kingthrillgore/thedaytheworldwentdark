using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraldMovementScript : MonoBehaviour
{

    private CharacterController character_Controller;

    private Vector3 move_Direction;
    public float speed = 5f;
    public float gravity = 20f;

    private float vertical_Velocity;

     void Awake()
    {
        character_Controller = GetComponent<CharacterController>();    
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        character_Controller.Move(move_Direction);
    } // move da playah! 

    void ApplyGravity()
    {
        if(character_Controller.isGrounded)
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }
        else
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;
    }//Apply gravity
}
