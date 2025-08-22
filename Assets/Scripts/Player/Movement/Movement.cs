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
    float runBoost = 5f;
    float jumpForce;

    //Internal
    Transform transform;
    Rigidbody rb;
    MonoBehaviour monoBehaviour;
    public bool isRunning = false;

    //Secondary
    float potionModifier = 0;
    bool runApplied = false;

    public Movement(Transform t, Rigidbody r, float jF, float s, MonoBehaviour MB)
    {
        transform = t;
        rb = r;
        Speed = s;
        jumpForce = jF;
        monoBehaviour = MB;
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

        Speed = Mathf.Min(baseSpeed + potionModifier, speedCap);
        //Esta línea es para que la velocidad del jugador buffeado nunca pueda ser mayor al máximo general de velocidad.
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

    public bool IsGrounded()
    {
        //Devuelve si el jugador está tocando el piso. El último  (v) parámetro hay que actualizarlo con la altura del jugador, 1.85f es un default.
        return !Physics.Raycast(transform.position, -Vector3.up, 1.85f);
    }

    //-------------------------------------------------------------------------------- Buff de velocidad
    public void BuffSpeed(float inc)
    {
        potionModifier += inc;
        if (!isRunning) Speed = Mathf.Min(baseSpeed + potionModifier, speedCap); //Si el jugador no está corriendo, 
    }

    public void DebuffSpeed(float dec)
    {
        potionModifier -= dec;
        potionModifier = Mathf.Max(potionModifier, 0f);
        if (!isRunning) Speed = Mathf.Max(baseSpeed + potionModifier, speedMin);
    }

    public IEnumerator TimedSpeedBoost(float sIncrease, float duration)
    {
        BuffSpeed(sIncrease);

        yield return new WaitForSeconds(duration);

        DebuffSpeed(sIncrease);
        OnSpeedBoostEnd?.Invoke();
    }

    public void CallSpeedBoostCoroutine(float sI, float t) => monoBehaviour.StartCoroutine(TimedSpeedBoost(sI, t));
    //Acá estoy llamando a la corutina a través de MonoBehaviour porque esta clase no tiene y es necesario para hacerlo
    //También, lo encierro en un método que después voy a utilizar para pasárselo al evento de la poción.
}
