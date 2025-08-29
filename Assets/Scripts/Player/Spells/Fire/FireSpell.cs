using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modificado por Jere, por las dudas revisar

public class FireSpell : MonoBehaviour
{

    //Titulos

    [Header (" Mana Settings ")]

    public float manaCostPerSecond = 1f;      //Este script tiene administracion del mana propia por asi decirlo, cuando
    public float currentMana = 50f;           //agreguemos la del player, esta se puede borrar
    public bool isActive;

    [Header (" Spells Objects ")]
    public GameObject fireInHand; //El fuego que ilumina, en la mano
    public GameObject fireballPrefab; //Prefab del proyectil
    public Transform firePoint; //Donde aparece el proyectil

    [Header (" Fireball Settings ")] 
    public float FireballCooldownTime = 2f; //Tiempo de reincoporacion del proyectil
    bool canShoot = true;

    void Start()
    {
        if ( fireInHand != null) // fih 💔
        {
            fireInHand.SetActive(false);

            // Con esto el hechizo empieza apagado
        }
        
    }

    void Update()
    {
        Debug.Log("Update corriendo en: " + gameObject.name);
        if ( Input.GetKeyDown(KeyCode.F)) //Se activa y desactiva el hechizo con la tecla "F"
        {
            Debug.Log("Presioné F, voy a alternar el hechizo");
            ToggleSpell(); //Alternar Hechizo
        }
        if (isActive && Input.GetButtonDown("Fire1") && canShoot) //Si el hechizo esta activo, el click izquierdo esta apretado y se puede disparar
        {
            Debug.Log("Intento castear fireball");
            CastFireball();  //se castea la fireball
        }
    }

    IEnumerator SpendMana()
    {
        while (isActive && currentMana > 0)   //Si el hechizo esta activo y el mana es superior a 0
        {
            currentMana -= manaCostPerSecond; //Resta mana
            yield return new WaitForSeconds(1f); //cada un seg
            
            if (currentMana <= 0) //Si el mana es 0 o menor, se desactiva el hechizo
            {
                isActive = false;
                fireInHand.SetActive(false);
            }

        }
    }

    void ToggleSpell()  //Alternar hechizo
    {
        isActive = !isActive;
        Debug.Log("ToggleSpell -> isActive ahora es: " + isActive);
        fireInHand.SetActive(isActive);   // El hechizo esta activo

        if (fireInHand != null)
    {
        fireInHand.SetActive(isActive);
        Debug.Log("fireInHand.SetActive(" + isActive + ")");
    }
    else
    {
        Debug.LogWarning("⚠ fireInHand NO está asignado en el inspector");
    }

        if (isActive)
        {
            StartCoroutine (SpendMana()); //Si esta activo consume mana
        }
    }

    void CastFireball()
    {
        if (fireballPrefab != null && firePoint != null) // Se asegura de que haya un prefab y firepoint existente
        {
            Instantiate (fireballPrefab, firePoint.position, firePoint.rotation); //prefab del fireball, posicion en la q aparece y direccion a la que mira
            StartCoroutine (FireballCooldown()); //esperas 2 segundos para volver a lanzarla
        }
    }

    IEnumerator FireballCooldown()
    {
        canShoot = false;  // Normalmente no se puede lanzar
        yield return new WaitForSeconds(FireballCooldownTime); //Espera 2seg o el numero que tenga fireballCooldown
        canShoot = true;  //Se puede lanzar
    }
}
