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
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.name == "Woods_AMB" || col.gameObject.name == "Ocean_AMB")
        {
            SoundManager.mainAudio.ambienceEvent.setParameterByName("AmbienceType", 0f);
        }
    }
}
