using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana
{
    [Header("Stats")]
    [SerializeField] [Range(10f, 200f)] private float _maxMP = 100;
    [SerializeField] private float _mp;

    public float maxMP { get { return _maxMP; } }
    public float mp
    {
        get { return _mp; }
        private set => _mp = Mathf.Clamp(value, 0f, _maxMP);
    }

    public Mana()
    {

    }

    protected virtual void Start() => _mp = _maxMP;

    public virtual void ManaSpend(float amount)
    {
        mp -= amount;
    }

    public virtual void ManaRegain(float amount)
    {
        if (mp < maxMP)
        {
            mp += amount;
            mp = Mathf.Min(mp, maxMP); // Limita el mana al máximo
        }

    }
}
