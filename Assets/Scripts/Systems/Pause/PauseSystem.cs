using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Por Jere

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool pausedGame = false;
  

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.P))
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;  //Importante, el tiempo se reanuda, de no aclarar esto la pantalla quedaria congelada
        pausedGame = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause ()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pausedGame = true;

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }

    public void ExitToMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
