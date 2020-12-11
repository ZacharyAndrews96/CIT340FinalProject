using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    //Varibles
    //List of Resources and list of buildings
    public string ID;
    public List<Resource> Stockpile;
    public List<Buildings> Civ;
    private int Demand { get
        {
            int D = 1;
            foreach(Buildings B in Civ)
            {
                if(B.Name == "City")
                {
                    D += 200 * ((City)B).Level;
                }
            }
            Debug.Log("Demand Return a value of " + D + " on the planet " + ID);
            return D;
        } }
    //functions
    //Tick() AddResource() RemoveResource()
    public Planet(string Index)
    {
        ID = Index;
        Stockpile = Resource.BuildList(1000);
        Civ = new List<Buildings>();
        //EveryPlanet will have 1 city and 1 farm to start with
        Civ.Add(new Farm());
        Civ.Add(new City(1));
        Debug.Log("A new Planet was made with a stockpile lise with a count of " + Stockpile.Count);
    }
    public void Tick()
    {

        foreach(Buildings X in Civ)
        {
            X.Tick(this);
        }
        //After the tick calculate the new value of the price of goods
        
        foreach(Resource R in Stockpile)
        {
            Debug.Log("The current Rate for " + R.Name + " is " + R.CurrentPrice + " on the planet " + ID);
            if (R.Amount > Demand)
            {
                Debug.Log("Math Check " + R.BasePrice * Demand / R.Amount);
                R.CurrentPrice = (R.BasePrice * Demand / R.Amount);
            }
            else
            {
                if(R.Amount != 0)
                {
                    R.CurrentPrice = (int)(R.BasePrice * 1.05 * (Demand / R.Amount));
                }
                
            }
            Debug.Log("Now the Rate for " + R.Name + " is " + R.CurrentPrice + " on the planet " + ID);
            //Do a check to see if its to low don't let the price be less than 2/5 of the base price
            if(R.CurrentPrice < R.BasePrice / 5 * 2)
            {
                R.CurrentPrice = R.BasePrice /5 * 2;
            }
            //also do a check to make sure the price can't be to high limit it to 10x
            if(R.CurrentPrice > R.BasePrice * 10)
            {
                R.CurrentPrice = R.BasePrice * 10;
            }
        }
        //if there is more than 100 food and goods tell the gamemaster to turn off the particle system
        if(Stockpile[0].Amount > 100 && Stockpile[3].Amount > 100)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>().TurnOffParticles(this);
        }
    }
    public void AddResource(string N, int A)
    {
        foreach(Resource X in Stockpile)
        {
            if(X.Name == N)
            {
                X.Amount += A;
            }
        }
    }
    public int GetRate(string N)
    {
        foreach(Resource X in Stockpile)
        {
            if(X.Name == N)
            {
                return X.CurrentPrice;
            }
            
        }
        return 0;
    }
    public bool RemoveResources(string N, int A)
    {
        Debug.Log("Attempting to remove " + N + " on planet " + ID + "The Stockpile has "+Stockpile.Count);
        foreach(Resource X in Stockpile)
        {
            if(X.Name == N)
            {
                Debug.Log("Found the Resource " + N + "On planet " + ID);
                //Check if there is enough to take don't want a negative balance
                if(X.Amount >= A)
                {
                    X.Amount -= A;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            
        }
        Debug.Log("Failed to find the Resource on"+ID);
        return false;
    }
    public void AddBuilding(Buildings X)
    {
        Civ.Add(X);
    }



}
