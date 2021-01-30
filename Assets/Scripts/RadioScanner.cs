using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray playerDirection = new Ray(transform.position, Vector3.forward);

        Debug.DrawRay(transform.position, Vector3.forward);

        if (Physics.Raycast(playerDirection, out hit)) {
            Debug.Log("Raycast is hitting something");

            if (hit.collider.tag == "NPC") {
                Debug.Log("We are facing an NPC.");
            }
        }    
    }
}
