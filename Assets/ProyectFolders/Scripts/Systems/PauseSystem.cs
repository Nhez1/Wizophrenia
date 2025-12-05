using UnityEngine;
using System;

//Por Jere

public class PauseSystem : MonoBehaviour
{
    public static event Action OnUnpause;

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Time.timeScale = 1;  //Importante, el tiempo se reanuda, de no aclarar esto la pantalla quedaria congelada
        OnUnpause?.Invoke();
    }
}
