using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSDection : MonoBehaviour
{
    public AnimationEvents _animationEvents;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.blue);

            if(hit.collider.name == "Sand_Floor")
            {
                Debug.Log("sand!");
                _animationEvents.FootstepEvent.setParameterByName("Surface", 1);
            }
        }
    }
}
 