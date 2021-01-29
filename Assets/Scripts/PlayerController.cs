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
}
