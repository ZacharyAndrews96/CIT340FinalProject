using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings
{
    //Define Tick for each building so they consume the needed resource
    public string Name { get; protected set; }
    abstract public void Tick(Planet X);
}
public class Farm: Buildings
{
    public Farm()
    {
        Name = "Farm";
    }
    public override void Tick(Planet X)
    {
        //This building will produce goods from nothing
        X.AddResource("Farm Products", 50);
    }
}
public class Mine: Buildings
{
    public Mine()
    {
        Name = "Mine";
    }
    public override void Tick(Planet X)
    {
        //Produce Minerals
        X.AddResource("Minerals", 100);
    }
}
public class Smelter: Buildings
{
    public Smelter()
    {
        Name = "Smelter";
    }
    public override void Tick(Planet X)
    {
        //Remove Goods if return false don't add goods
        if(X.RemoveResources("Minerals", 50))
        {
            //Produce Minerals
            X.AddResource("Components", 50);
        }
        
    }
}
public class Factory: Buildings
{
    public Factory()
    {
        Name = "Factory";
    }
    public override void Tick(Planet X)
    {
        //Remove Goods if return false don't add goods
        if (X.RemoveResources("Components", 40))
        {
            //Produce Minerals
            X.AddResource("Products", 30);
        }

    }
}
public class City : Buildings
{
    public int Level;
    public City(int L)
    {
        Level = L;
        Name = "City";
    }
    public override void Tick(Planet X)
    {
        //calculate how much to remove based on level
        int Foods = Level * 20 + 10;
        int Goods = Level * 5 + 15;
        //Remove multiple Goods if return false trigger event
        if(X.RemoveResources("Farm Products", Foods) != true)
        {
            //Tell Data Manager to add event on planet
        }
        if(X.RemoveResources("Products", Goods) != true)
        {
            //tell data manger to add event on planet
        }
        
            

    }
}
