using UnityEngine;
using System;

//Por Jere

public class PauseSystem : MonoBehaviour
{
    public static event Action OnUnpause;

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

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;  //Importante, el tiempo se reanuda, de no aclarar esto la pantalla quedaria congelada
        OnUnpause?.Invoke();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
