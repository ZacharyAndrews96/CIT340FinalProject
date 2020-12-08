using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //This will be used to manage sound effects so only one sound source will be needed
    //List of sound effects and a Command to set and run that sound effect
    public List<AudioClip> Sounds;
    public GameObject Parent;
    private AudioSource Target;
    private void Start()
    {
        Target = Parent.GetComponent<AudioSource>();
    }

    public void RunAudio(int X)
    {
        Target.clip = Sounds[X];
        Target.Play();
    }

}
