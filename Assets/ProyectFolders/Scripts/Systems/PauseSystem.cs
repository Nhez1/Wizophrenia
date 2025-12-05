using UnityEngine;
using System;


public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameEvent _onUnpause;
    private bool _paused = false;

    public bool IsPaused => _paused;

    public void TogglePause()
    {
        if (_paused) Unpause();
        else Pause();
    }

    public void Pause()
    {
        _paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _paused = false;
        Time.timeScale = 1;
        _onUnpause.Raise(this, null);
    }
}
