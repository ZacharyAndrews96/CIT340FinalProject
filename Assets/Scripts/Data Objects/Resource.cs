using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource
{
    //Static function to build the list in one place
    public static List<Resource> BuildList(int X)
    {
        List<Resource> Template = new List<Resource>();
        Template.Add(new FarmProducts(X));
        Template.Add(new Minerals(X));
        Template.Add(new Components(X));
        Template.Add(new Products(X));
        Debug.Log("A new Template List was Made that has a count of " + Template.Count);
        return Template;
    }
    //will have a Name, base price, current price, and amount
    public string Name { get; protected set; }
    public int BasePrice { get; protected set; }
    
    public int CurrentPrice;
    public int Amount;
}
public class FarmProducts : Resource
{
    public FarmProducts(int Start = 0)
    {
        Name = "Farm Products";
        BasePrice = 5;
        CurrentPrice = BasePrice;
        Amount = Start;
    }
}
public class Minerals : Resource
{
    public Minerals(int Start = 0)
    {
           Name = "Minerals";
        BasePrice = 10;
        CurrentPrice = BasePrice;
        Amount = Start;
    }
}
public class Components : Resource
{
    public Components(int Start = 0)
    {
        Name = "Components";
        BasePrice = 20;
        CurrentPrice = BasePrice;
        Amount = Start;
    }
}
public class Products : Resource
{
    public Products(int Start = 0)
    {
        Name = "Products";
        BasePrice = 50;
        CurrentPrice = BasePrice;
        Amount = Start;
    }
}
