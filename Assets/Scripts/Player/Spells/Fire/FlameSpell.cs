using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Modificado por Fecu >:]

public class FlameSpell : MonoBehaviour, ISpell
{
    public static event Action<bool> OnFlameSwitch;

    //Titulos
    private float _manaCostPerSecond = 1f;
    private Mana _mana;
    private string _spellName = "Flame Spell";

    public string Name => _spellName;
    public float ManaCost => _manaCostPerSecond;
    public bool IsActive { get; private set; }

    public void Init(Mana m, GameObject prefab = null)
    {
        _mana = m;
        IsActive = false;
    }

    IEnumerator DrainMana()
    {
        while (IsActive && _mana.MP > ManaCost)   //Si el hechizo esta activo y el mana es superior al costo
        {
            _mana.SpendMana(ManaCost); //Resta mana
            yield return new WaitForSeconds(1f); //cada un seg
        }

        if (_mana.MP <= ManaCost) ToggleSpell(); //Si el mana es menor al costo, se desactiva el hechizo
    }

    private void ToggleSpell()  //Alternar hechizo
    {
        if (_mana == null) return;
        IsActive = !IsActive;

        if (IsActive)
        {
            OnFlameSwitch?.Invoke(IsActive);
            StartCoroutine(DrainMana()); //Si esta activo consume mana
        }
        else
        {
            OnFlameSwitch?.Invoke(IsActive);
            StopCoroutine(DrainMana()); //Si se desactiva, se para la corutina
        }
    }

    public void Cast() => ToggleSpell();
}
