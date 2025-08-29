using UnityEngine;

public class InputController
{
    public float MouseSensibility { get; set; }
    private float _xAxis, _zAxis;
    private float _mouseX, _mouseY;
    private Movement _movement;
    private PlayerAnimations _animations;
    private Mana _mana;
    //private BasicGun _shotgun;
    //private InventoryPlayer _inventory;
    //private WeaponManager _weaponManager;

    public float MouseX => _mouseX;
    public float MouseY => _mouseY;

    public InputController(Movement m, PlayerAnimations anim, Mana ma/*, BasicGun sg, InventoryPlayer inv, WeaponManager wM*/)
    {
        _movement = m;
        _animations = anim;
        _mana = ma;
        //_grabBehaviour = gB;
        //_shotgun = sg;
        //_inventory = inv;
        //_weaponManager = wM;
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

        //Animaciones
        //_animations.CheckInputs(_xAxis, _zAxis);

        //Uso de poción de vida
        //if (Input.GetKeyDown(KeyCode.C) && _inventory.HasPotion<HealPotion>()) _inventory.UsePotionItem<HealPotion>();

        //Uso de poción de velocidad
        //if (Input.GetKeyDown(KeyCode.X) && _inventory.HasPotion<SpeedPotion>()) _inventory.UsePotionItem<SpeedPotion>();

        //Cambio de armas//
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    _weaponManager.EquipWeapon(0);
        //    _shotgun.gunActive = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    _weaponManager.EquipWeapon(1);
        //    _shotgun.gunActive = false;
        //}

        //Disparo
        if (Input.GetKeyDown(KeyCode.F)) _mana.SpendMana(10);
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
