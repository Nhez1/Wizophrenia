using System;
using UnityEngine;

public class InputController
{
    public event Action OnBagToggle;
    public event Action OnConsumableUse;

    public float MouseSensibility { get; set; }
    private float _xAxis, _zAxis;
    private float _mouseX, _mouseY;
    private Movement _movement;
    private SpellManager _spells;
    private PlayerInteraction _interacter;

    private bool _paused = false;
    private PauseSystem _pauseSystem;
    private MenuManager _menuManager;
    

    
    private bool _lotusLock = false;

    public float MouseX => _mouseX;
    public float MouseY => _mouseY;

    public InputController(Movement m, SpellManager spellManager, PlayerInteraction interacter, PauseSystem pauseSystem, MenuManager menuManager)
    {
        _movement = m;
        _spells = spellManager;
        _interacter = interacter;
        _pauseSystem = pauseSystem;
        _menuManager = menuManager;
        
    }

    public void OnUpdate()
    {
        if (!_paused)
        {
            // Mouse input
            _mouseX = Input.GetAxis("Mouse X") * MouseSensibility * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * MouseSensibility * Time.deltaTime;

            // Movimiento
            _xAxis = Input.GetAxisRaw("Horizontal");
            _zAxis = Input.GetAxisRaw("Vertical");

            // Salto
            if (Input.GetKeyDown(KeyCode.Space) && _movement.IsGrounded()) _movement.Jump();

            // Interact
            if (Input.GetKeyDown(KeyCode.E))
            {
                _interacter.CurrentInteractable?.Interact();
            }

            // Pause game
            if (Input.GetKeyDown(KeyCode.P))
            {
                _pauseSystem.TogglePause();
                if (_pauseSystem.IsPaused) 
                {
                    _menuManager.ShowPauseMenu();
                }
                else
                {
                    _menuManager.HidePauseMenu();
                }
            }

            if (_pauseSystem.IsPaused)
            {
                return;
            }

            if (!_lotusLock)
            {
                // Cast Flame Spell
                if (Input.GetKeyDown(KeyCode.F)) _spells.CastSpell(SpellType.FlameSpell);

                // Cast Fire Ball
                if (Input.GetKeyDown(KeyCode.Mouse1)) _spells.CastSpell(SpellType.FireBall);

                if (Input.GetKeyDown(KeyCode.Q)) _spells.CastSpell(SpellType.Reignite);

                // Open inventory
                if (Input.GetKeyDown(KeyCode.B)) OnBagToggle?.Invoke();

                // Use consumable on left hand
                if (Input.GetKeyDown(KeyCode.C)) OnConsumableUse?.Invoke();
            }
        }
        // Se restauran los inputs al despausarse el juego
    }

    public void OnFixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0) _movement.Move(_xAxis, _zAxis);

        //Run
        if (Input.GetKey(KeyCode.LeftShift)) _movement.Run();
        else _movement.isRunning = false;
    }

    public void LockInputs() => _lotusLock = true;
    public void UnlockInputs() => _lotusLock = false;
}