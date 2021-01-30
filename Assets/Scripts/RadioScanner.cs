using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScanner : MonoBehaviour
{
    public float FadeoutCooldown = 5f;
    public bool FadeoutRunning = false;
    delegate void StartAudioPlay(int num);
    delegate void KillAudioPlay(int num);
    StartAudioPlay startAudio;
    KillAudioPlay killAudio;

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
                FadeoutRunning = true;

                // TODO Send a delegate to the Sound Engine we are facing an NPC
                //startAudio(1);

                if (FadeoutRunning) {
                    if (FadeoutCooldown > 0) {
                        Debug.Log("Coming in Strong");
                        FadeoutCooldown -= Time.deltaTime;
                    } else {
                        Debug.Log("Fadeout Radio");
                        //TODO delegate to kill sound
                        //killAudio(1);
                        FadeoutCooldown = 0;
                        FadeoutRunning = false;
                    }
                }
            }
        }    
    }
}
