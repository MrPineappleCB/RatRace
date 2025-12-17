using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float difficulty = 0;
    public float lap = 0;
    public float laplength = 0;

    public int dropdifficulty;
    public int droplap;
    public int droplength;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GenerationTest");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (dropdifficulty == 0)
        {
            difficulty = 1;
        }
        else if (dropdifficulty == 1)
        {
            difficulty = 2;
        }
        else if (dropdifficulty == 2)
        {
            difficulty = 3;
        }

        if (droplap == 0)
        {
            lap = 1;
        }
        else if (droplap == 1)
        {
            lap = 3;
        }
        else if (droplap == 2)
        {
            lap = 5;
        }

        if (droplength == 0)
        {
            laplength = 10;
        }
        else if (droplength == 1)
        {
            laplength = 15;
        }
        else if (droplength == 2)
        {
            laplength = 20;
        }
    }  
}
