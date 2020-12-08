using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float Cooldown = 5;
    float TimeSinceLastCue;
    // Start is called before the first frame update
    void Start()
    {
        TimeSinceLastCue = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Run audio Cue that you can't enter an asteroid field
        if(collision.gameObject.tag == "Player")
        {
            if(TimeSinceLastCue + Cooldown < Time.time)
            {
                //Run Audio
                GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>().RunAudio(0);
                TimeSinceLastCue = Time.time;
            }
        }
    }
}
