using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int LevelIndex;
    public GameObject Market;
    public GameObject UIObject;
    public bool Pause;
    public float TimePerDay;
    private float LastTimeCheck;
    public int Days;
    public int TimeLimit;
    public List<int> Goals;
    private string CurrentPlanetIndex;
    private List<Resource> Stock;
    private List<Resource> PStock;
    // Start is called before the first frame update
    void Start()
    {
        //Need to load the list of system into the data manager
        DataManager.Data.LevelID = LevelIndex;
        Pause = false;
        LastTimeCheck = Time.time;
        Days = 0;
        UpdateUIObject();
        UpdateGoalsUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the data level needs to tick
        if(Pause == false)
        {
            if(Time.time > LastTimeCheck + TimePerDay)
            {
                //Tick
                DataManager.Data.Tick();
                LastTimeCheck = Time.time;
                Days++;
                //Update Counter UI
                UpdateUIObject();
            }
        }
        if(Days > TimeLimit)
        {
            Endlevel();
        }
    }
    private void UpdateUIObject()
    {
        //0 is Time 1 is the Money 2 is goals
        UIObject.transform.GetChild(0).GetComponent<Text>().text = "Day " + Days;
        UIObject.transform.GetChild(1).GetComponent<Text>().text = "Money: "+DataManager.Data.Gamer.Money;

    }
    private void UpdateGoalsUI()
    {
        string Output = "Goals: (Money) \n";
        for (int i = 0; i < Goals.Count; i++)
        {
            Output += "" + (i + 1) + ":" + Goals[i] + "\n";
        }
        UIObject.transform.GetChild(2).GetComponent<Text>().text = Output;
    }
    public void Endlevel()
    {
        bool Fail = true;
        for (int i = 0; i < Goals.Count; i++)
        {
            if(DataManager.Data.Gamer.Money > Goals[i])
            {
                DataManager.Data.Upgrades++;
                Fail = false;
            }
            else
            {
                break;
            }
        }
        if (Fail)
        {
            SceneManager.LoadScene("Loss");
        }
        //Load the Upgrades Scene
        SceneManager.LoadScene("UpgradeScene");
    }
    public void RegistarPlanetToData(SpacePlanet X)
    {
        //Make A new Planet
        Planet N = new Planet(X.PlanetID);
        for (int i = 0; i < X.NumberFarms; i++)
        {
            N.AddBuilding(new Farm());
        }
        for (int i = 0; i < X.NumberMines; i++)
        {
            N.AddBuilding(new Mine());
        }
        for (int i = 0; i < X.NumberSmelters; i++)
        {
            N.AddBuilding(new Smelter());
        }
        for (int i = 0; i < X.NumberFactories; i++)
        {
            N.AddBuilding(new Factory());
        }
        for (int i = 0; i < X.NumberCitiesAndLevel.Count; i++)
        {
            for (int J = 0; J < X.NumberCitiesAndLevel[i]; J++)
            {
                N.AddBuilding(new City(i + 1));
            }
        }
        DataManager.Data.LoadPlanet(N);


    }
    public void DisplayMarket(string PN)
    {
        Market.SetActive(true);
        Pause = true;
        //Need Player and Planet Stock
        CurrentPlanetIndex = PN;
        //Debug
        string DebugText = "The planet stock has the following ";
        
        Stock = DataManager.Data.GetPlanetStock(PN);
        PStock = DataManager.Data.GetPlayerStock();
        foreach (Resource Y in Stock)
        {
            DebugText += Y.Name + "Has " + Y.Amount;
        }
        Debug.Log(DebugText);
        //Update Sliders
        Market.transform.GetChild(3).GetComponent<Slider>().minValue = -PStock[0].Amount;
        Market.transform.GetChild(3).GetComponent<Slider>().maxValue = Stock[0].Amount;
        Market.transform.GetChild(3).GetComponent<Slider>().value = PStock[0].Amount;
        Market.transform.GetChild(4).GetComponent<Slider>().minValue = -PStock[1].Amount;
        Market.transform.GetChild(4).GetComponent<Slider>().maxValue = Stock[1].Amount;
        Market.transform.GetChild(4).GetComponent<Slider>().value = PStock[1].Amount;
        Market.transform.GetChild(5).GetComponent<Slider>().minValue = -PStock[2].Amount;
        Market.transform.GetChild(5).GetComponent<Slider>().maxValue = Stock[2].Amount;
        Market.transform.GetChild(5).GetComponent<Slider>().value = PStock[2].Amount;
        Market.transform.GetChild(6).GetComponent<Slider>().minValue = -PStock[3].Amount;
        Market.transform.GetChild(6).GetComponent<Slider>().maxValue = Stock[3].Amount;
        Market.transform.GetChild(6).GetComponent<Slider>().value = PStock[3].Amount;
        //update Display
        //Planet Stock
        Market.transform.GetChild(7).GetComponent<Text>().text = "" + Stock[0].Amount;
        Market.transform.GetChild(8).GetComponent<Text>().text = "" + Stock[1].Amount;
        Market.transform.GetChild(9).GetComponent<Text>().text = "" + Stock[2].Amount;
        Market.transform.GetChild(10).GetComponent<Text>().text = "" + Stock[3].Amount;
        //Player Stock
        Market.transform.GetChild(11).GetComponent<Text>().text = "" + PStock[0].Amount;
        Market.transform.GetChild(12).GetComponent<Text>().text = "" + PStock[1].Amount;
        Market.transform.GetChild(13).GetComponent<Text>().text = "" + PStock[2].Amount;
        Market.transform.GetChild(14).GetComponent<Text>().text = "" + PStock[3].Amount;
        //Display the buildings at this planet
        string Structures = "The following buildings are on this planet: ";
        foreach(Buildings B in DataManager.Data.GetPlanetCiv(PN))
        {
            Structures += B.Name;
        }
        Market.transform.GetChild(15).GetComponent<Text>().text = Structures;
        //Update Price for each
        Market.transform.GetChild(16).GetComponent<Text>().text = "" + Stock[0].CurrentPrice;
        Market.transform.GetChild(17).GetComponent<Text>().text = "" + Stock[1].CurrentPrice;
        Market.transform.GetChild(18).GetComponent<Text>().text = "" + Stock[2].CurrentPrice;
        Market.transform.GetChild(19).GetComponent<Text>().text = "" + Stock[3].CurrentPrice;
        //Update Inventory capacity and Money
        Market.transform.GetChild(0).GetComponent<Text>().text = "Money: " + DataManager.Data.Gamer.Money;
        Market.transform.GetChild(20).GetComponent<Text>().text = "Capacity: " + DataManager.Data.Gamer.Capacity + "/" + DataManager.Data.Gamer.Storage;
    }
    public void ProcessTransaction()
    {
        Planet X = DataManager.Data.GetPlanet(CurrentPlanetIndex);
        Player P = DataManager.Data.Gamer;
        Slider Now;
        //gets all the sliders values and process
        //Farm
        Now = Market.transform.GetChild(3).GetComponent<Slider>();
        if(Now.value > 0)
        {
            P.AddResource(X, Stock[0].Name, (int)Now.value);
        }
        else if(Now.value < 0)
        {
            P.RemoveResources(X, Stock[0].Name, -(int)Now.value);
        }
        //Minerals
        Now = Market.transform.GetChild(4).GetComponent<Slider>();
        if (Now.value > 0)
        {
            P.AddResource(X, Stock[1].Name, (int)Now.value);
        }
        else if (Now.value < 0)
        {
            P.RemoveResources(X, Stock[1].Name, -(int)Now.value);
        }
        //Components
        Now = Market.transform.GetChild(5).GetComponent<Slider>();
        if (Now.value > 0)
        {
            P.AddResource(X, Stock[2].Name, (int)Now.value);
        }
        else if (Now.value < 0)
        {
            P.RemoveResources(X, Stock[2].Name, -(int)Now.value);
        }
        //Products
        Now = Market.transform.GetChild(6).GetComponent<Slider>();
        if (Now.value > 0)
        {
            P.AddResource(X, Stock[3].Name, (int)Now.value);
        }
        else if (Now.value < 0)
        {
            P.RemoveResources(X, Stock[3].Name, -(int)Now.value);
        }
        //Update the UI by running the command Again
        DisplayMarket(CurrentPlanetIndex);
        
    }
    public void HideMarket()
    {
        Market.SetActive(false);
        Pause = false;
    }
    //functions to update the tallies while sliders move
    public void UpdateFarm()
    {
        Slider Now = Market.transform.GetChild(3).GetComponent<Slider>();
        Text Planet = Market.transform.GetChild(7).GetComponent<Text>();
        Text Ship = Market.transform.GetChild(11).GetComponent<Text>();
        //Negative value should add to Planet Stock and subtract Ship
        Planet.text = "" + (Stock[0].Amount - Now.value);
        Ship.text = "" + (PStock[0].Amount - (-Now.value)); 

    }
    public void UpdateMinerals()
    {
        Slider Now = Market.transform.GetChild(4).GetComponent<Slider>();
        Text Planet = Market.transform.GetChild(8).GetComponent<Text>();
        Text Ship = Market.transform.GetChild(12).GetComponent<Text>();
        //Negative value should add to Planet Stock and subtract Ship
        Planet.text = "" + (Stock[1].Amount - Now.value);
        Ship.text = "" + (PStock[1].Amount - (-Now.value));
    }
    public void UpdateComponents()
    {
        Slider Now = Market.transform.GetChild(5).GetComponent<Slider>();
        Text Planet = Market.transform.GetChild(9).GetComponent<Text>();
        Text Ship = Market.transform.GetChild(13).GetComponent<Text>();
        //Negative value should add to Planet Stock and subtract Ship
        Planet.text = "" + (Stock[2].Amount - Now.value);
        Ship.text = "" + (PStock[2].Amount - (-Now.value));
    }
    public void UpdateProducts()
    {
        Slider Now = Market.transform.GetChild(6).GetComponent<Slider>();
        Text Planet = Market.transform.GetChild(10).GetComponent<Text>();
        Text Ship = Market.transform.GetChild(14).GetComponent<Text>();
        //Negative value should add to Planet Stock and subtract Ship
        Planet.text = "" + (Stock[3].Amount - Now.value);
        Ship.text = "" + (PStock[3].Amount - (-Now.value));
    }
}
