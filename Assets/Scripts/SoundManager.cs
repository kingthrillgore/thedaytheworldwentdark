using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager mainAudio;

    public string ambience;
    public FMOD.Studio.EventInstance ambienceEvent;

    public string ambientMusic;
    public FMOD.Studio.EventInstance ambientMusicEvent;

    public string MX_1;
    //public FMOD.Studio.EventInstance MX_1Event;
    public string MX_2;
    //public FMOD.Studio.EventInstance MX_2Event;
    public string MX_3;
    //public FMOD.Studio.EventInstance MX_3Event;
    public string MX_4;
    //public FMOD.Studio.EventInstance MX_4Event;
    public string MX_5;
    //public FMOD.Studio.EventInstance MX_5Event;
    public string MX_6;
    //public FMOD.Studio.EventInstance MX_6Event;
    public string MX_7;
    //public FMOD.Studio.EventInstance MX_7Event;

    public string footsteps;

    public string Radio1;
    //public FMOD.Studio.EventInstance Radio1Event;
    public string Radio2;
   // public FMOD.Studio.EventInstance Radio2Event;
    public string Radio3;
    //public FMOD.Studio.EventInstance Radio3Event;




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

       // FMODUnity.RuntimeManager.PlayOneShot("event:/MUSIC/MX_Ambience");

        ambientMusicEvent = FMODUnity.RuntimeManager.CreateInstance(ambientMusic);
        ambientMusicEvent.start();

        /*MX_1Event = FMODUnity.RuntimeManager.CreateInstance(MX_1);
        MX_1Event.start();
        MX_2Event = FMODUnity.RuntimeManager.CreateInstance(MX_2);
        MX_2Event.start();
        MX_3Event = FMODUnity.RuntimeManager.CreateInstance(MX_3);
        MX_3Event.start();
        MX_4Event = FMODUnity.RuntimeManager.CreateInstance(MX_4);
        MX_4Event.start();
        MX_5Event = FMODUnity.RuntimeManager.CreateInstance(MX_5);
        MX_5Event.start();
        MX_6Event = FMODUnity.RuntimeManager.CreateInstance(MX_6);
        MX_6Event.start();
        MX_7Event = FMODUnity.RuntimeManager.CreateInstance(MX_7);
        MX_7Event.start(); */




    }

   
 
}
