using UnityEngine;

public class InputController
{
    private float _xAxis, _zAxis;
    public float _mouseX, _mouseY;
    private float _mouseSensitivity = 100f;
    private Movement _movement;
    private PlayerAnimations _animations;
    //private BasicGun _shotgun;
    //private InventoryPlayer _inventory;
    //private WeaponManager _weaponManager;

    public InputController(Movement m, PlayerAnimations anim/*, BasicGun sg, InventoryPlayer inv, WeaponManager wM*/)
    {
        _movement = m;
        _animations = anim;
        //_grabBehaviour = gB;
        //_shotgun = sg;
        //_inventory = inv;
        //_weaponManager = wM;
    }

    public void OnUpdate()
    {
        //Mouse input
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

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

        ////Disparo
        //if (Input.GetKeyDown(KeyCode.Mouse0) && _shotgun.gunActive) _shotgun.Shoot();
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
