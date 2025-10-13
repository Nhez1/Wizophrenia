using UnityEngine;
using UnityEngine.SceneManagement;

//Por Jere

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Canvas inventoryMenu;
    public bool pausedGame = false;

    private void OnEnable()
    {
        InputController.OnPause += Pause;
    }

    private void OnDisable()
    {
        InputController.OnPause -= Pause;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pausedGame = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;  //Importante, el tiempo se reanuda, de no aclarar esto la pantalla quedaria congelada
        pausedGame = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
