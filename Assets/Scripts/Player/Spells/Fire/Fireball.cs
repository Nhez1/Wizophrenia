using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Modificado por Jere, por las dudas revisar

public class FireBall : MonoBehaviour, ISpell
{
    private Mana _mana;
    private MonoBehaviour runner;

    [Header(" Spells Objects ")]
    private string _spellName = "Fire Ball";
    public GameObject fireballPrefab; //Prefab del proyectil
    public Transform firePoint;       //Donde aparece el proyectil

    [Header(" Fireball Settings ")]
    private float _manaCost = 5f;
    public float FireballCooldownTime = 2f; //Tiempo de reincoporacion del proyectil
    bool canShoot = false;
    bool onCD = false;

    public string Name => _spellName;
    public Mana Mana => _mana;
    public float ManaCost => _manaCost;

    public void Init(Mana m, GameObject prefab, Transform castPoint, MonoBehaviour mb)
    {
        _mana = m;
        fireballPrefab = prefab;
        firePoint = castPoint;
        runner = mb;
        FlameEffectSO.OnFlameSwitch += SwitchActive;
    }

    //void Update()
    //{
    //    Debug.Log("Update corriendo en: " + gameObject.name);
    //    if (Input.GetKeyDown(KeyCode.F)) //Se activa y desactiva el hechizo con la tecla "F"
    //    {
    //        Debug.Log("Presioné F, voy a alternar el hechizo");
    //    }
    //    if (Input.GetButtonDown("Fire1") && canShoot) //Si el hechizo esta activo, el click izquierdo esta apretado y se puede disparar
    //    {
    //        Debug.Log("Intento castear fireball");
    //        CastFireball();  //se castea la fireball
    //    }
    //}

    public void Cast()
    {
        if (fireballPrefab != null) // Se asegura de que haya un prefab y firepoint existente
        {
            if (firePoint != null)
            {
                if (canShoot && !onCD)
                {
                    Instantiate(fireballPrefab, firePoint.position, Camera.main.transform.rotation); //prefab del fireball, posicion en la q aparece y direccion a la que mira
                    Mana.Spend(ManaCost);
                    runner.StartCoroutine(FireballCooldown()); //esperas 2 segundos para volver a lanzarla
                }
            }
            else Debug.Log("FirePoint not used inside FireBall.cs");
        }
        else Debug.Log("FireBall prefab not found inside FireBall.cs");
    }

    IEnumerator FireballCooldown()
    {
        onCD = true;  // Normalmente no se puede lanzar
        yield return new WaitForSeconds(FireballCooldownTime); //Espera 2seg o el numero que tenga fireballCooldown
        onCD = false;  //Se puede lanzar
    }

    public void SwitchActive(float x) => canShoot = !canShoot;
}
//Marker