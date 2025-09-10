using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Modificado por Fecu >:]

public class FlameSpell : ISpell
{
    public static event Action<bool, float> OnFlameSwitch;

    //Titulos
    private float _manaCostPerSecond = 1f;
    private Mana _mana;
    private string _spellName = "Flame Spell";

    public string Name => _spellName;
    public float ManaCost => _manaCostPerSecond;
    public bool IsActive { get; private set; }

    public void Init(Mana m, GameObject prefab = null, Transform castPoint = null, MonoBehaviour mb = null)
    {
        _mana = m;
        IsActive = false;
    }

    private void ToggleSpell()  //Alternar hechizo
    {
        if (_mana == null) return;
        IsActive = !IsActive;

        if (IsActive) OnFlameSwitch?.Invoke(IsActive, ManaCost);
        else OnFlameSwitch?.Invoke(IsActive, ManaCost);
    }

    public void Cast() => ToggleSpell();
}
