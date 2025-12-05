using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventsRelay : MonoBehaviour
{
    [SerializeField] private GameEvent _onBagToggle;
    [SerializeField] private GameEvent _onPause;
    [SerializeField] private GameEvent _onConsumableUse;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.InputControl.OnBagToggle += ToggleBag;
        _player.InputControl.OnConsumableUse += UseConsumable;
        _player.InputControl.OnPause += PauseGame;
    }

    private void OnDestroy()
    {
        _player.InputControl.OnBagToggle -= ToggleBag;
        _player.InputControl.OnConsumableUse -= UseConsumable;
        _player.InputControl.OnPause -= PauseGame;
    }

    void ToggleBag() => _onBagToggle.Raise(this, null);
    void UseConsumable() => _onConsumableUse.Raise(this, null);
    void PauseGame() => _onPause.Raise(this, null);
}
