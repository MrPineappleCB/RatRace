using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool easy = false;
    public bool normal = false;
    public bool hard = false;
    public float lap = 0;
    public float laplength = 0;
    public void PlayGame()
    {
        SceneManager.LoadScene("blank");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Easy()
    {
        easy = true;
        normal = false;
        hard = false;
    }

    public void Normal()
    {
        normal = true;
        easy = false;
        hard = false;
    }

    public void Hard()
    {
        hard = true;
        easy = false;
        normal = false;
    }


}
