using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager mainAudio;

   


    private void Awake()
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/NightAMB_1");
    }
}
