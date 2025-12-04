using System;
using System.Collections;
using UnityEngine;

public class Movement
{
    public static event Action OnSpeedBoostEnd;

    //Stats
    public float Speed { get; private set; }
    private float _baseSpeed;
    private float _speedMin = 2f;
    private float _speedCap = 15f;
    private float _runBoost;
    private float _jumpForce;

    //Internal
    private Transform _transform;
    private Rigidbody _rb;
    private MonoBehaviour _cRunner;
    private Player _player;
    public bool isRunning = false;

    //Secondary
    private float _potionModifier = 0;
    private bool _runApplied = false;

    public Movement(Transform t, Rigidbody r, float jF, float s, float rS, MonoBehaviour MB)
    {
        _transform = t;
        _rb = r;
        Speed = s;
        _runBoost = rS;
        _jumpForce = jF;
        _cRunner = MB;
        _player = MB as Player;
    }

    public void OnStart()
    {
        _baseSpeed = Speed;
        _runBoost = _player.RunBoost;
    }

    public void OnUpdate()
    {
        if (!isRunning)
        {
            if (_runApplied)
            {
                //Si el jugador no está corriendo y tiene aplicado el buff de correr, removérselo.
                DebuffSpeed(_runBoost);
                _runApplied = false;
            }
        }
    }

    //-------------------------------------------------------------------------------- Movimiento
    public void Move(float _xAxis, float _zAxis)
    {
        Vector3 dir = (_transform.right * _xAxis + _transform.forward * _zAxis).normalized;

        _rb.MovePosition(_transform.position + Speed * Time.fixedDeltaTime * dir);
    }

    //-------------------------------------------------------------------------------- Correr
    public void Run()
    {
        if (!_runApplied)
        {
            //Si el buff de correr ya no está aplicado, aplicárselo.
            BuffSpeed(_runBoost);
            _runApplied = true;
        }

        isRunning = true;
    }

    //-------------------------------------------------------------------------------- Salto
    public void Jump() => _rb.AddForce(_transform.up * _jumpForce, ForceMode.Impulse);

    //--------------------------------------------------------------------------------
    private void UpdateSpeed()
    {
        float walkSpeed = Mathf.Clamp(_baseSpeed + _potionModifier, _speedMin, _speedCap);
        Speed = isRunning ? walkSpeed + _runBoost : walkSpeed;
        //Si el jugador está corriendo, Speed = walkSpeed + runBoost. Si no, Speed = walkSpeed.
    }

    //--------------------------------------------------------------------------------
    public bool IsGrounded()
    {
        //Devuelve si el jugador está tocando el piso. El último  (v) parámetro hay que actualizarlo con la altura del jugador, 1.85f es un default.
        return !Physics.Raycast(_transform.position, -Vector3.up, 1.85f);
    }

    //-------------------------------------------------------------------------------- Buff de velocidad
    public void BuffSpeed(float inc)
    {
        _potionModifier += inc;
        UpdateSpeed();
    }

    //-------------------------------------------------------------------------------- Debuff de velocidad
    public void DebuffSpeed(float dec)
    {
        _potionModifier -= dec;
        _potionModifier = Mathf.Max(_potionModifier, 0f);
        UpdateSpeed();
    }

    //-------------------------------------------------------------------------------- Timer del buff
    public IEnumerator TimedSpeedBoost(float sIncrease, float duration)
    {
        BuffSpeed(sIncrease);

        yield return new WaitForSeconds(duration);

        DebuffSpeed(sIncrease);
        OnSpeedBoostEnd?.Invoke();
    }

    //--------------------------------------------------------------------------------
    public void CallSpeedBoostCoroutine(float sI, float t) => _cRunner.StartCoroutine(TimedSpeedBoost(sI, t));
    //Acá estoy llamando a la corutina a través de MonoBehaviour porque esta clase no tiene y es necesario para hacerlo
    //También, lo encierro en un método que después voy a utilizar para pasárselo al evento de la poción.
    //
    //Puede que sea prudente revisar esto en el futuro, está medio raro.
}
