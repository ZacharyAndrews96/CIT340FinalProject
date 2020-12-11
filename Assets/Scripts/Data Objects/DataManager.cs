using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    static public DataManager Data;
    //Needs to store Player And a list of planets
    public Player Gamer;
    public int LevelID;
    public int Upgrades;
    List<Planet> System;

    public DataManager()
    {
        Upgrades = 0;
        Gamer = new Player();
        System = new List<Planet>();
        Data = this;
    }
    public List<float> GetStats()
    {
        List<float> Output = new List<float>();
        Output.Add(Gamer.Speed);
        Output.Add(Gamer.Boost);
        return Output;
    }
    public void Tick()
    {
        foreach (Planet X in System)
        {
            X.Tick();
        }
    }
    public List<Buildings> GetPlanetCiv(string PN)
    {
        foreach (Planet X in System)
        {
            if (X.ID == PN)
            {
                return X.Civ;
            }

        }
        return new List<Buildings>();
    }
    public void Shortage(int Deficit, Planet X)
    {
        //First lets take money for not filling the needs of the population
        Gamer.Money -= 10 * Deficit;
        //Next check to see if the particle system is active for the planet if not activate it
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>().TurnOnParticles(X);
    }

    public void UpgradeSpeed()
    {
        Upgrades--;
        Gamer.Speed += .5f;
    }
    public void UpgradeStorage()
    {
        Upgrades--;
        Gamer.Storage += 50;
    }
    public void UpgradeBoost()
    {
        Upgrades--;
        Gamer.Boost += .25f;
    }
    public Planet GetPlanet(string PN)
    {
        foreach (Planet X in System)
        {
            if (X.ID == PN)
            {
                return X;
            }

        }
        return null;
    }
    public List<Resource> GetPlanetStock(string PN)
    {
        Debug.Log("attempting to find:" + PN);
        foreach (Planet X in System)
        {
            Debug.Log("Looking at " + X.ID);
            if (X.ID == PN)
            {
                return X.Stockpile;
            }

        }
        Debug.Log("A planet ID was not found in the list");
        return new List<Resource>();
    }
    public List<Resource> GetPlayerStock()
    {
        return Gamer.Inventory;
    }
    public void LoadPlanet(Planet X)
    {
        System.Add(X);
    }
    
    public void UnloadPlanets()
    {
        System = new List<Planet>();
    }
}
