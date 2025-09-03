using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private Life _life;
    [SerializeField] private Mana _mana;
    [Tooltip("Sped")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Este es el incremento de velocidad cuando el jugador va a correr, no la velocidad a la que va a correr.")]
    [SerializeField] private float _runBoost = 5f;
    [SerializeField] private float _jumpForce = 3f;
    
    [SerializeField] private float _mouseSensibility = 100f;

    private InputController _controller;
    private Movement _move;
    private PlayerAnimations _playerAnim;
    private Rigidbody _rb;

    // Cuando sea que se necesite hacerle daño al jugador, se usa Player.Life.TakeDamage(cantidad);
    public Life Life => _life;
    // Lo mismo para el mana, cuando sea que se necesite gastar mana, se usa Player.Mana.SpendMP(cantidad);
    public Mana Mana => _mana;
    public float Speed => _speed;
    public float RunBoost => _runBoost;
    public InputController InputControl => _controller;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _life = new();
        _mana = new();
        _move = new(transform, _rb, _jumpForce, _speed, _runBoost, this);
        _controller = new(_move, _playerAnim, _mana);

    }

    private void Start()
    {
        _move.OnStart();
    }

    private void Update()
    {
        _move.OnUpdate();
        _controller.OnUpdate();
        _controller.MouseSensibility = _mouseSensibility;
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }
}
