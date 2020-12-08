using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject ButtonParent;
    public GameObject DisplayParent;
    // Start is called before the first frame update
    public void Start()
    {
        UpdateStatsDisplay();
        //REset some of the player Data like Money and Inventory
        DataManager.Data.Gamer.Money = 1000;
        DataManager.Data.Gamer.Inventory = Resource.BuildList(0);
    }
    public void UpgradeSpeed()
    {
        DataManager.Data.UpgradeSpeed();
        HideButtons();
    }
    public void UpgradeStorage()
    {
        DataManager.Data.UpgradeStorage();
        HideButtons();
    }
    public void UpgradeBoost()
    {
        DataManager.Data.UpgradeBoost();
        HideButtons();
    }
    private void HideButtons()
    {
        //Also lets update the visuals
        if(DataManager.Data.Upgrades < 1)
        {
            ButtonParent.SetActive(false);
        }
        UpdateStatsDisplay();
    }
    private void UpdateStatsDisplay()
    {
        //0 Speed, 1 Storage, 2 Boost
        DisplayParent.transform.GetChild(0).GetComponent<Text>().text = "Speed: " + DataManager.Data.Gamer.Speed;
        DisplayParent.transform.GetChild(1).GetComponent<Text>().text = "Storage: " + DataManager.Data.Gamer.Storage;
        DisplayParent.transform.GetChild(2).GetComponent<Text>().text = "Boost: " + DataManager.Data.Gamer.Boost;
        DisplayParent.transform.GetChild(4).GetComponent<Text>().text = "Points: " + DataManager.Data.Upgrades;
    }
    public void LoadNextLevel()
    {
        switch (DataManager.Data.LevelID)
        {
            case 1:
                SceneManager.LoadScene("LevelTwo");
                break;
            case 2:
                SceneManager.LoadScene("LevelThree");
                break;
            case 3:
                SceneManager.LoadScene("Victory");
                break;
        }
    }
}
