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

    //NPC variables
    private bool _IsPlayerWithinRange = false;
    private GameObject ActiveNPC;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            // TODO add sanity check so if this.ActiveNPC is set, nothing happens.
            _IsPlayerWithinRange = true;
            Debug.Log("Collision Detected");
            /* Component[] components = other.gameObject.GetComponents(typeof(Component));
            FollowPlayer FP1 = other.gameObject.GetComponent<FollowPlayer>(); */

            Debug.Log("FollowPlayer Retrieved");
            // Set the follow behavior
            other.gameObject.GetComponent<FollowPlayer>().target = this.transform;

            this.ActiveNPC = other.gameObject;
        }

        if (other.tag == "Fireplace")
        {
            // Clear out our companion's target
            this.ActiveNPC.GetComponent<FollowPlayer>().target = null;
            Debug.Log("NPC is safe.");

            // Lerp the target to the Fireplace
            this.ActiveNPC.transform.position = Vector3.MoveTowards(this.ActiveNPC.transform.position, other.transform.position, 2f * Time.deltaTime);

            // Turn off their collide so nothing else can happen to them.
            this.ActiveNPC.GetComponent<BoxCollider>().enabled = false;
            this.ActiveNPC = null;
        }
    }

    // Fired off when something exits the trigger space of the attached GameObject
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            _IsPlayerWithinRange = false;

        }
    }
}

