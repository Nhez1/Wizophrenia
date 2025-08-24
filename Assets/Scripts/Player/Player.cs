using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Life
{
    [Header("Stats")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Este es el incremento de velocidad cuando el jugador va a correr, no la velocidad a la que va a correr.")]
    [SerializeField] private float _runBoost = 5f;
    [SerializeField] private float _jumpForce = 3f;

    private InputController _controller;
    private Movement _move;
    private PlayerAnimations _playerAnim;
    private Rigidbody _rb;
    public float Speed => _speed;
    public float RunBoost => _runBoost;
    public InputController InputControl => _controller;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _move = new Movement(transform, _rb, _jumpForce, _speed, _runBoost, this);
        _controller = new(_move, _playerAnim);
    }

    protected override void Start()
    {
        base.Start();
        _move.OnStart();
    }

    protected virtual void Update()
    {
        _move.OnUpdate();
        _controller.OnUpdate();
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }
}
