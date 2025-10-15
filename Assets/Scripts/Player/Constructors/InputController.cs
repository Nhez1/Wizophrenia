using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController
{
    public static event Action<float> RefillMana;
    public static event Action<float> RefillHP;
    public static event Action OnPause;
    public static event Action OnBagOpen;

    public float MouseSensibility { get; set; }
    private float _xAxis, _zAxis;
    private float _mouseX, _mouseY;
    private Movement _movement;
    private SpellManager _spells;
    private PlayerInteraction _interacter;

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
        // Mouse input
        _mouseX = Input.GetAxis("Mouse X") * MouseSensibility * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * MouseSensibility * Time.deltaTime;

        // Movimiento
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && _movement.IsGrounded()) _movement.Jump();

        // Cast Flame Spell
        if (Input.GetKeyDown(KeyCode.F)) _spells.CastSpell(SpellType.FlameSpell);

        // Cast Fire Ball
        if (Input.GetKeyDown(KeyCode.Mouse1)) _spells.CastSpell(SpellType.FireBall);

        if (Input.GetKeyDown(KeyCode.Q)) _spells.CastSpell(SpellType.Exorcise);

        // Interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_interacter.CurrentInteractable != null) _interacter.CurrentInteractable.Interact();
        }

        // Refill mana
        if (Input.GetKeyDown(KeyCode.R)) RefillMana?.Invoke(20f);
        if (Input.GetKeyDown(KeyCode.T)) RefillHP?.Invoke(20f);

        // Reset scene
        if (Input.GetKeyDown(KeyCode.Y)) UnityEngine.SceneManagement.SceneManager.LoadScene("Sandbox");

        // Pause game
        if (Input.GetKeyDown(KeyCode.P)) OnPause?.Invoke();

        // Open inventory
        if (Input.GetKeyDown(KeyCode.B)) OnBagOpen?.Invoke();
    }

    public void OnFixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0) _movement.Move(_xAxis, _zAxis);

        //Run
        if (Input.GetKey(KeyCode.LeftShift)) _movement.Run();
        else _movement.isRunning = false;
    }
}
