using UnityEngine;
using System;

[System.Serializable]
public class Life: IDamageable
{
    //public static event Action GameOverEvent;
    [Tooltip("Maximum HP")]
    [SerializeField] private float _maxHP = 100;
    [Tooltip("Health Points")]
    [SerializeField] private float _hp;

    public float MaxHP { get { return _maxHP; } private set { } }
    public float HP { get { return _hp; } private set => _hp = Mathf.Clamp(value, 0f, _maxHP); }


    public Life()
    {
        HP = MaxHP;
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0) GameOver();
    }

    public void TakeHeal(float amount)
    {
        if (HP < MaxHP) HP += amount;

    }

    public void SetHP(float amount) => HP = amount;
    public void SetMaxHP(float amount) => MaxHP = amount;

    protected virtual void GameOver()
    {
        Debug.Log("Game Over");
    }
}
