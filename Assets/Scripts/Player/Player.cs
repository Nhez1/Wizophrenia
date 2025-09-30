using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header(" Stats ")]
    [SerializeField] private Life _life;
    [SerializeField] private Mana _mana;
    [Tooltip("Sped")] [SerializeField] private float _speed = 3f;
    [Tooltip("Este es el incremento de velocidad cuando el jugador va a correr, no la velocidad a la que va a correr.")]
    [SerializeField] private float _runBoost = 5f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _reach = 3f;

    [Header(" Internal ")]
    [SerializeField] private float _mouseSensibility = 100f;
    [Tooltip("El punto desde el que se van a instanciar los hechizos")]
    public Transform spellCastPoint;

    [Header(" Spells ")]
    public GameObject fireInHand;
    public FlameSpellSO flameSpell;
    public SpellSO fireSpell;

    private PlayerInteraction _interacter;
    private InputController _controller;
    private Movement _move;
    private Rigidbody _rb;
    [SerializeField] private SpellManager _spellManager;

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

        _life = new(true);
        _mana = new(this);
        _interacter = new();
        _move = new(transform, _rb, _jumpForce, _speed, _runBoost, this);
        _spellManager = new(_mana, fireInHand, spellCastPoint, this, flameSpell);
        _controller = new(_move, _spellManager, _interacter);
    }

    private void Start()
    {
        _move.OnStart();
        _spellManager.AddSpell(SpellType.FireBall, fireSpell);
    }

    private void Update()
    {
        _interacter.HoverUpdate();
        _move.OnUpdate();
        _controller.OnUpdate();
        _controller.MouseSensibility = _mouseSensibility;
        _interacter.PlayerReach = _reach;
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        _spellManager.SpellDispose();
    }
}
