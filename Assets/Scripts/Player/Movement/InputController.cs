using UnityEngine;

public class InputController
{
    private Vector3 _dir;
    private float _xAxis, _zAxis;
    private Movement _move;
    private PlayerAnimations _animations;
    //private BasicGun _shotgun;
    //private InventoryPlayer _inventory;
    //private WeaponManager _weaponManager;

    public InputController(Movement m, PlayerAnimations anim/*, BasicGun sg, InventoryPlayer inv, WeaponManager wM*/)
    {
        _move = m;
        _animations = anim;
        //_grabBehaviour = gB;
        //_shotgun = sg;
        //_inventory = inv;
        //_weaponManager = wM;
    }

    public void OnUpdate()
    {
        //Movimiento
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && _move.IsGrounded()) _move.Jump();

        //Animaciones
        _animations.CheckInputs(_xAxis, _zAxis);

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
            _dir = new Vector3(_xAxis, 0, _zAxis);

            _move.Move(_xAxis, _zAxis);
        }

        //Run
        if (Input.GetKey(KeyCode.LeftShift)) _move.Run();
        else _move.isRunning = false;
    }
}
