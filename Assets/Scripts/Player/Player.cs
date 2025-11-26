using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header(" Stats ")]
    [SerializeField] private Life _life;
    [SerializeField] private Mana _mana;
    [SerializeField] private Sanity _sanity;
    [Tooltip("Sped")] [SerializeField] private float _speed = 3f;
    [Tooltip("Este es el incremento de velocidad cuando el jugador va a correr, no la velocidad a la que va a correr.")]
    [SerializeField] private float _runBoost = 5f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _reach = 3f;

    [Header(" Internal ")]
    [SerializeField] private float _mouseSensibility = 100f;
    [Tooltip("El punto desde el que se van a instanciar los hechizos")]
    [SerializeField] private Transform spellCastPoint;

    [Header(" Spells ")]
    public GameObject fireInHand;
    public SpellSO flameSpell;
    public SpellSO fireSpell;
    public SpellSO exorciseSpell;

    private PlayerInteraction _interacter;
    private InputController _controller;
    private Movement _move;
    private Rigidbody _rb;
    private SpellManager _spellManager;

    // Cuando sea que se necesite hacerle da�o al jugador, se usa Player.Life.TakeDamage(cantidad);
    public Life Life => _life;
    // Lo mismo para el mana, cuando sea que se necesite gastar mana, se usa Player.Mana.SpendMP(cantidad);
    public Mana Mana => _mana;
    public Sanity Sanity => _sanity;
    public float Speed => _speed;
    public float RunBoost => _runBoost;
    public InputController InputControl => _controller;
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        if (!fireInHand) GetComponentInChildren<Light>();
        _rb = GetComponent<Rigidbody>();

        _sanity = new();
        _life = new(true, 100f);
        _mana = new(this);
        _interacter = new();
        _move = new(transform, _rb, _jumpForce, _speed, _runBoost, this);
        _spellManager = new(_mana, spellCastPoint, this);
        _controller = new(_move, _spellManager, _interacter);
        Inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        _move.OnStart();
        _spellManager.RegisterSpellPrefab(SpellType.FlameSpell, fireInHand);
        _spellManager.RegisterSpellPrefab(SpellType.FireBall, fireSpell.prefab);
        _spellManager.RegisterSpellPrefab(SpellType.Reignite, exorciseSpell.prefab);

        _spellManager.AddSpell(flameSpell);
        _spellManager.AddSpell(fireSpell);
        _spellManager.AddSpell(exorciseSpell);
        _controller.MouseSensibility = _mouseSensibility;
        _interacter.PlayerReach = _reach;
    }

    private void Update()
    {
        _interacter.HoverUpdate();
        _move.OnUpdate();
        _controller.OnUpdate();
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }

    private void OnEnable()
    {
        CauldronObject.OnAlchemyToggle += _interacter.HoverLeave;
        LeftHandler.OnLotusGrab += _controller.LockInputs;
        LeftHandler.OnLotusLeave += _controller.UnlockInputs;
        LeftHandler.OnLotusGrab += SetLightColorBlue;
        LeftHandler.OnLotusLeave += SetLightColorDefault;
    }

    private void OnDisable()
    {
        CauldronObject.OnAlchemyToggle -= _interacter.HoverLeave;
        LeftHandler.OnLotusGrab -= _controller.LockInputs;
        LeftHandler.OnLotusLeave -= _controller.UnlockInputs;
        LeftHandler.OnLotusGrab -= SetLightColorBlue;
        LeftHandler.OnLotusLeave -= SetLightColorDefault;
        _spellManager?.SpellDispose();   //SALTABA ERROR, ASI QUE LO CORREGI
    }

    #region LightInHand
    Light _light;

    void SetLightColorBlue()
    {
        fireInHand.SetActive(true);
        _light = fireInHand.GetComponent<Light>();
        _light.color = new(0.5518868f, 0.8779028f, 1);
    }

    void SetLightColorDefault()
    {
        fireInHand.SetActive(false);
        _light.color = new(1, 0.6399195f, 0);
    }
    #endregion
}
