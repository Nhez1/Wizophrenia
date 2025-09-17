using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public GameObject menuPause;
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
        menuPause.SetActive(false);
        Time.timeScale = 1;
        pausedGame = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause ()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;
        pausedGame = true;

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }

    public void ExitToMainMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
