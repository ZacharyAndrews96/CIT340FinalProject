using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //List of resourses and Stats
    //Stats: Speed, Max Storage, Boost
    public List<Resource> Inventory;
    public int Money;
    public float Speed;
    public int Storage;
    public int Capacity { get
        {
            int Cap = 0;
            foreach(Resource X in Inventory)
            {
                Cap += X.Amount;
            }
            return Cap;
        } }
    public float Boost;

    public Player()
    {
        Money = 1000;
        Storage = 200;
        Speed = 3.0f;
        Boost = .5f;
        Inventory = Resource.BuildList(0);
        
    }

    public void AddResource(Planet X, string R, int A)
    {
        //Need the rate
        int Cost = X.GetRate(R);
        //Check to see if we can afford this much
        if(A*Cost > Money)
        {
            //Tell DataManager to tell Visualmanager that the player can't afford this
            return;
        }
        else
        {
            if(A+Capacity > Storage)
            {
                //Tell Datamanager to tell visual manager that the player can't store this
                return;
            }
            else
            {
                if (X.RemoveResources(R, A))
                {
                    foreach (Resource x in Inventory)
                    {
                        if (x.Name == R)
                        {
                            x.Amount += A;
                            Money -= (A * Cost);
                        }
                    }
                }
                
            }
        }
    }
    public void RemoveResources(Planet X, string R, int A)
    {
        
        foreach (Resource x in Inventory)
        {
            if (x.Name == R)
            {
                Money += (A * X.GetRate(R));
                x.Amount -= A;
                X.AddResource(R, A);
            }
        }
    }

}
