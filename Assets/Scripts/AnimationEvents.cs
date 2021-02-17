using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public FMOD.Studio.EventInstance FootstepEvent;

    // Start is called before the first frame update
    void Start()
    {
        FootstepEvent = FMODUnity.RuntimeManager.CreateInstance(SoundManager.mainAudio.footsteps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepSound()
    {
        //FootstepEvent.start();
        //FootstepEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Footsteps");
    }
}
