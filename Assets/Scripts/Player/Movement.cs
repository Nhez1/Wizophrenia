using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public static event Action OnSpeedBoostEnd;

    //Stats
    public float Speed { get; private set; }
    float baseSpeed = 5f;
    float speedMin = 2f;
    float speedCap = 15f;
    float runBoost;
    float jumpForce;

    //Internal
    Transform transform;
    Rigidbody rb;
    MonoBehaviour monoBehaviour;
    Player player;
    public bool isRunning = false;

    //Secondary
    float potionModifier = 0;
    bool runApplied = false;

    public Movement(Transform t, Rigidbody r, float jF, float s, float rS, MonoBehaviour MB)
    {
        transform = t;
        rb = r;
        Speed = s;
        runBoost = rS;
        jumpForce = jF;
        monoBehaviour = MB;
        player = MB as Player;
    }

    public void OnStart() => baseSpeed = Speed;

    public void OnUpdate()
    {
        if (!isRunning)
        {
            if (runApplied)
            {
                //Si el jugador no está corriendo y tiene aplicado el buff de correr, removérselo.
                DebuffSpeed(runBoost);
                runApplied = false;
            }
        }
        baseSpeed = player.Speed;   //
        runBoost = player.RunBoost; //Estas dos líneas están para que los cambios en el editor sobre la velocidad se apliquen inmediatamente.
    }

    //-------------------------------------------------------------------------------- Movimiento
    public void Move(float _xAxis, float _zAxis)
    {
        Vector3 dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;

        rb.MovePosition(transform.position + Speed * Time.fixedDeltaTime * dir);
    }

    //-------------------------------------------------------------------------------- Correr
    public void Run()
    {
        if (!runApplied)
        {
            //Si el buff de correr ya no está aplicado, aplicárselo.
            BuffSpeed(runBoost);
            runApplied = true;
        }
        isRunning = true;
    }

    //-------------------------------------------------------------------------------- Salto
    public void Jump() => rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    //--------------------------------------------------------------------------------
    private void UpdateSpeed()
    {
        float walkSpeed = Mathf.Clamp(baseSpeed + potionModifier, speedMin, speedCap);
        Speed = isRunning ? walkSpeed + runBoost : walkSpeed;
        //Si el jugador está corriendo, Speed = walkSpeed + runBoost. Si no, Speed = walkSpeed.
    }

    //--------------------------------------------------------------------------------
    public bool IsGrounded()
    {
        //Devuelve si el jugador está tocando el piso. El último  (v) parámetro hay que actualizarlo con la altura del jugador, 1.85f es un default.
        return !Physics.Raycast(transform.position, -Vector3.up, 1.85f);
    }

    //-------------------------------------------------------------------------------- Buff de velocidad
    public void BuffSpeed(float inc)
    {
        potionModifier += inc;
        UpdateSpeed();
    }

    //-------------------------------------------------------------------------------- Debuff de velocidad
    public void DebuffSpeed(float dec)
    {
        potionModifier -= dec;
        potionModifier = Mathf.Max(potionModifier, 0f);
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
    public void CallSpeedBoostCoroutine(float sI, float t) => monoBehaviour.StartCoroutine(TimedSpeedBoost(sI, t));
    //Acá estoy llamando a la corutina a través de MonoBehaviour porque esta clase no tiene y es necesario para hacerlo
    //También, lo encierro en un método que después voy a utilizar para pasárselo al evento de la poción.
    //
    //Puede que sea prudente revisar esto en el futuro, está medio raro.
}
