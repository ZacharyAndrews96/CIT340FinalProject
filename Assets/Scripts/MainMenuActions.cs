using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuActions : MonoBehaviour
{
    public GameObject Tutorial;
    // Start is called before the first frame update
    void Start()
    {
        if(Tutorial != null)
        {
            new DataManager();
        }
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void DisplayTutorial()
    {
        if (Tutorial.activeSelf)
        {
            Tutorial.SetActive(false);
        }
        else
        {
            Tutorial.SetActive(true);
        }
    }
    public void ExitApp()
    {
     #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
    }
}
