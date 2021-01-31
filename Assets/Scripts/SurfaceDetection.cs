using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Ocean_AMB")
        {
            Debug.Log("Ocean Ambience!");
            SoundManager.mainAudio.ambienceEvent.setParameterByName("AmbienceType", 1f);
        }
        if (col.gameObject.name == "Woods_AMB")
        {
            Debug.Log("Woods Ambience!");
            SoundManager.mainAudio.ambienceEvent.setParameterByName("AmbienceType", 2f);
        }

        if (col.gameObject.name == "Brook_AMB")
        {
            Debug.Log("Frogs Ambience!");
            SoundManager.mainAudio.ambienceEvent.setParameterByName("AmbienceType", 3f);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.name == "Woods_AMB" || col.gameObject.name == "Ocean_AMB" || col.gameObject.name == "Brook_AMB")
        {
            SoundManager.mainAudio.ambienceEvent.setParameterByName("AmbienceType", 0f);
        }
    }
}
