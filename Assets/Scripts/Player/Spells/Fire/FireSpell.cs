using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modificado por Jere, por las dudas revisar

public class FireSpell : MonoBehaviour, ISpell
{

    //Titulos

    [Header(" Mana Settings ")]
    private float _manaCost = 1f;
    private Mana _mana;

    [Header(" Spells Objects ")]
    private string _spellName = "Fire Ball";
    public GameObject fireballPrefab; //Prefab del proyectil
    public Transform firePoint;       //Donde aparece el proyectil

    [Header(" Fireball Settings ")]
    public float FireballCooldownTime = 2f; //Tiempo de reincoporacion del proyectil
    bool canShoot = true;

    public string Name => _spellName;
    public Mana Mana => _mana;
    public float ManaCost => _manaCost;

    public void Init(Mana m, GameObject prefab)
    {
        _mana = m;
        fireballPrefab = prefab;
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
        if (fireballPrefab != null && firePoint != null) // Se asegura de que haya un prefab y firepoint existente
        {
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation); //prefab del fireball, posicion en la q aparece y direccion a la que mira
            StartCoroutine(FireballCooldown()); //esperas 2 segundos para volver a lanzarla
        }
    }

    IEnumerator FireballCooldown()
    {
        canShoot = false;  // Normalmente no se puede lanzar
        yield return new WaitForSeconds(FireballCooldownTime); //Espera 2seg o el numero que tenga fireballCooldown
        canShoot = true;  //Se puede lanzar
    }
}
