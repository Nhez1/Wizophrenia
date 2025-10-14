using UnityEngine;

//Por Jere

public class PauseSystem : MonoBehaviour
{
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
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;  //Importante, el tiempo se reanuda, de no aclarar esto la pantalla quedaria congelada

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
