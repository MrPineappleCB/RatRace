using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUi;
    public bool noesc = false;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape) && noesc == false)
       {
            if (GamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
       } 
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void NoEsc()
    {
        noesc = true;
    }

    public void BackButton()
    {
        noesc = false;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}