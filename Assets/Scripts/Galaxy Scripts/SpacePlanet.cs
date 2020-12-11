using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlanet : MonoBehaviour
{
    bool Orbit = false;
    public string PlanetID;
    public int NumberFarms;
    public int NumberMines;
    public int NumberSmelters;
    public int NumberFactories;
    public List<int> NumberCitiesAndLevel;
    // Start is called before the first frame update
    void Start()
    {
        //On start up have each planet tell the gamemaster to register them in the Data Level
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>().RegistarPlanetToData(transform.GetComponent<SpacePlanet>());
        //Because i don't know how to turn of the particle system form the editor lets do it here
        transform.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Orbit)
        {
            Debug.Log("player is in orbit of " + PlanetID);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>().DisplayMarket(PlanetID);
                GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>().RunAudio(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Add Audio Here later
        Orbit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Add audio here later
        Orbit = false;
    }
}
