using UnityEngine;

public class InputController
{
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
        //Mouse input
        _mouseX = Input.GetAxis("Mouse X") * MouseSensibility * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * MouseSensibility * Time.deltaTime;

        //Movimiento
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && _movement.IsGrounded()) _movement.Jump();

        // Flame spell toggle
        if (Input.GetKeyDown(KeyCode.F)) _spells.CastSpell(SpellType.FlameSpell);
        if (Input.GetKeyDown(KeyCode.Mouse1)) _spells.CastSpell(SpellType.FireBall);

        // Interact
        if (Input.GetKeyDown(KeyCode.E)) _interacter.CurrentInteractable.TryInteract();
    }

    public void OnFixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0)
        {
            _movement.Move(_xAxis, _zAxis);
        }

        //Run
        if (Input.GetKey(KeyCode.LeftShift)) _movement.Run();
        else _movement.isRunning = false;
    }
}
