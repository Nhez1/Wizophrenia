using System;
using UnityEngine;

public class InputController
{
    public event Action OnBagToggle;
    public event Action OnPause;
    public event Action OnConsumableUse;

    public float MouseSensibility { get; set; }
    private float _xAxis, _zAxis;
    private float _mouseX, _mouseY;
    private Movement _movement;
    private SpellManager _spells;
    private PlayerInteraction _interacter;

    private bool _paused = false;
    private bool _lotusLock = false;

    public float MouseX => _mouseX;
    public float MouseY => _mouseY;

    public InputController(Movement m, SpellManager spellManager, PlayerInteraction interacter)
    {
        _movement = m;
        _spells = spellManager;
        _interacter = interacter;
    }

    public void OnUpdate()
    {
        if (_paused) return;

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
            OnPause?.Invoke();
            _paused = true;
        }

        if (_lotusLock) return;

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

    public void OnFixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0) _movement.Move(_xAxis, _zAxis);

        //Run
        if (Input.GetKey(KeyCode.LeftShift)) _movement.Run();
        else _movement.isRunning = false;
    }

    public void LotusLock() => _lotusLock = true;
    public void LotusUnlock() => _lotusLock = false;
    public void UnpauseInputs() => _paused = false;
}