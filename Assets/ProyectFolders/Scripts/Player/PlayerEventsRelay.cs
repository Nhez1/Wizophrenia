using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventsRelay : MonoBehaviour
{
    [SerializeField] private GameEvent _onBagToggle;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.InputControl.OnBagToggle += ToggleBag;
    }

    private void OnDestroy()
    {
        _player.InputControl.OnBagToggle -= ToggleBag;
        
    }

    void ToggleBag() => _onBagToggle.Raise(this, null);
}
