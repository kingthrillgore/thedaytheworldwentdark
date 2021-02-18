using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_controller;
    private Vector3 move_Direction;
    Animator animator;
    float moveAnim =1f;
    float notMoving = 0f;

    //public GameObject arms;
    public float speed = 5f;

    private float gravity = 20f;

    public float jump_force = 10f;

    private float vertical_Velocity;

    public bool isMoving;
    // Start is called before the first frame update

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
        animator = GameObject.Find("Arms Animation").GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        MovethePlayer();


    }

    void MovethePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); //because im a fuckin beast

        move_Direction = transform.TransformDirection(move_Direction); // WORLD STAR
        move_Direction *= speed * Time.deltaTime; // because we live n a world that uses regular time unity... figure it out. 

        if (move_Direction != Vector3.zero)
        {
            animator.SetFloat("MoveAnim", moveAnim);
        }
        else
        {
            animator.SetFloat("MoveAnim", notMoving);
        }

        ApplyGravity();

        character_controller.Move(move_Direction); // because character controllers are crazy

     
     


    } // move player

    void ApplyGravity()
    {
        if(character_controller.isGrounded)
        {
            vertical_Velocity -= gravity * Time.deltaTime;

            //jump 
            PlayerJump();
        }
        else
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    }//apply gravity 

    void PlayerJump()
    {
        if(character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_force;
        }
    }

}//class
