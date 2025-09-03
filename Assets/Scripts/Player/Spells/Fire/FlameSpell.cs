using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modificado por Jere, por las dudas revisar

public class FlameSpell : MonoBehaviour, ISpell
{

    //Titulos

    [Header(" Mana Settings ")]
    private float _manaCostPerSecond = 1f;      //Este script tiene administracion del mana propia por asi decirlo, cuando
    private Mana _mana;
    public bool isActive;

    [Header(" Spells Objects ")]
    private string _spellName = "Flame Spell";
    public GameObject fireInHand;     //El fuego que ilumina, en la mano
    public Transform firePoint;       //Donde aparece el proyectil

    [Header(" Fireball Settings ")]
    public float FireballCooldownTime = 2f; //Tiempo de reincoporacion del proyectil

    public string Name => _spellName;
    public float ManaCost => _manaCostPerSecond;

    public void Init(Mana m, GameObject prefab)
    {
        _mana = m;
        fireInHand = prefab;
    }

    void Start()
    {
        if (fireInHand != null) // fih 💔
        {
            fireInHand.SetActive(false);

            // Con esto el hechizo empieza apagado
        }
    }

    void Update()
    {
        Debug.Log("Update corriendo en: " + gameObject.name);
        if (Input.GetKeyDown(KeyCode.F)) //Se activa y desactiva el hechizo con la tecla "F"
        {
            Debug.Log("Presioné F, voy a alternar el hechizo");
            ToggleSpell(); //Alternar Hechizo
        }
    }

    IEnumerator DrainMana()
    {
        while (isActive && _mana.MP > ManaCost)   //Si el hechizo esta activo y el mana es superior al costo
        {
            _mana.SpendMana(ManaCost); //Resta mana
            yield return new WaitForSeconds(1f); //cada un seg

            if (_mana.MP <= ManaCost) //Si el mana es 0 o menor, se desactiva el hechizo
            {
                isActive = false;
                fireInHand.SetActive(false);
            }

        }
    }

    void ToggleSpell()  //Alternar hechizo
    {
        isActive = !isActive;

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
            StartCoroutine(DrainMana()); //Si esta activo consume mana
        }
    }

    public void Cast() => ToggleSpell();
}
