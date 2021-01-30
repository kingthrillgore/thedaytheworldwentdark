using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thank you Brackeys for carrying my lazy ass
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera;
    public float speed = 6f;
    public float turnInterpolateTime = 0.1f;
    private float turnSmoothInterpolate;
    private bool _IsPlayerWithinRange = false;
    private GameObject ActiveNPC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal"); // W, S, Arrow U D
        float vert = Input.GetAxisRaw("Vertical"); // A, D, Arrow L R
        Vector3 dir = new Vector3(horiz, 0f, vert);

        if (dir.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //Adds a transition between turn directions by running every measurable transition on a grid
            //tl;dr this makes it look less like garbage
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothInterpolate, turnInterpolateTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movedir.normalized * speed * Time.deltaTime);
        }
    }

    
    // Fired off when something enters the trigger space of the attached GameObject
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC") {
            // TODO add sanity check so if this.ActiveNPC is set, nothing happens.
            _IsPlayerWithinRange = true;
            Debug.Log("Collision Detected");
            /* Component[] components = other.gameObject.GetComponents(typeof(Component));
            FollowPlayer FP1 = other.gameObject.GetComponent<FollowPlayer>(); */

            Debug.Log("FollowPlayer Retrieved");
            // Set the follow behavior
            other.gameObject.GetComponent<FollowPlayer>().target = this.transform;

            // TODO We will need to keep track of our traveler so we only keep one at a time
            this.ActiveNPC = other.gameObject;
        }

        // TODO Another check down the road for when the Player eventually collides with the campfire
        if (other.tag == "Fireplace") {
            // Clear out our companion's target
            this.ActiveNPC.GetComponent<FollowPlayer>().target = null;
            Debug.Log("NPC is safe.");

            // Lerp the target to the Fireplace
            this.ActiveNPC.transform.position = Vector3.MoveTowards(this.ActiveNPC.transform.position, other.transform.position, 2f * Time.deltaTime);

            // Turn off their collide so nothing else can happen to them.
            this.ActiveNPC.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Fired off when something exits the trigger space of the attached GameObject
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC") {
            _IsPlayerWithinRange = false;
            
        }
    }
}
