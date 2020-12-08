using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebula : MonoBehaviour
{
    public float ReduceFactor = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<SpaceTruck>().ReduceSpeed += ReduceFactor;
            collision.transform.GetComponent<Rigidbody2D>().velocity /= 2;
            GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>().RunAudio(2);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<SpaceTruck>().ReduceSpeed -= ReduceFactor;
        }
    }
}
