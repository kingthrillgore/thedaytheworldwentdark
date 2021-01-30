using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager mainAudio;

    public string ambience;
    public FMOD.Studio.EventInstance ambienceEvent;

    public string footsteps; 


    void Awake()
    {
        if (mainAudio != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            mainAudio = this;
            DontDestroyOnLoad(gameObject);
        }

    }   
    
    // Start is called before the first frame update
    void Start()
    {

        ambienceEvent = FMODUnity.RuntimeManager.CreateInstance(ambience);
        ambienceEvent.start();
        //FMODUnity.RuntimeManager.PlayOneShot("event:/NightAMB_1");
    }
}
