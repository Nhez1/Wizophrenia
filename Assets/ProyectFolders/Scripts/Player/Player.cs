using UnityEngine;

public class Player : MementoEntity, IDamageable
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
    [SerializeField] private Transform _spellCastPoint;

    [Header(" Spells ")]
    [SerializeField] private SpellSO _flameSpell;
    [SerializeField] private SpellSO _fireSpell;
    [SerializeField] private SpellSO _reigniteSpell;
    private Light _fireInHand;

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
    public float RunBoost => _runBoost;
    public InputController InputControl => _controller;
    public Inventory Inventory { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody>();

        _life = new(true, 100f);
        _mana = new(this);
        _sanity = new();
        _interacter = new();
        _move = new(transform, _rb, _jumpForce, _speed, _runBoost, this);
        _spellManager = new(_mana, _spellCastPoint, this);
        _controller = new(_move, _spellManager, _interacter);

        Inventory = FindObjectOfType<Inventory>();
        _fireInHand = GetComponentInChildren<Light>(true);
    }

    private void Start()
    {
        _move.OnStart();

        _spellManager.RegisterSpellPrefab(SpellType.FlameSpell, _fireInHand.gameObject);
        _spellManager.RegisterSpellPrefab(SpellType.FireBall, _fireSpell.prefab);
        _spellManager.RegisterSpellPrefab(SpellType.Reignite, _reigniteSpell.prefab);

        _spellManager.AddSpell(_flameSpell);
        _spellManager.AddSpell(_fireSpell);
        _spellManager.AddSpell(_reigniteSpell);

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
    void SetLightColorBlue()
    {
        _fireInHand.gameObject.SetActive(true);
        _fireInHand.color = new(0.5518868f, 0.8779028f, 1);
    }

    void SetLightColorDefault()
    {
        _fireInHand.gameObject.SetActive(false);
        _fireInHand.color = new(1, 0.6399195f, 0);
    }
    #endregion

    protected override void SaveStates()
    {
        Debug.Log("Saved");
        _memento.SaveMemory(_life.GetData(), _mana.GetData(), _sanity.GetData());
    }

    protected override void LoadStates(object[] oldState)
    {
        Debug.Log("Loaded");
        _life.LoadData((PlayerData)oldState[0]);
        _mana.LoadData((PlayerData)oldState[1]);
        _sanity.LoadData((PlayerData)oldState[2]);
    }
}
