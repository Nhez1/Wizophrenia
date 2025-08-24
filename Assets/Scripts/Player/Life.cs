using UnityEngine;
using System;

public class Life: MonoBehaviour, IDamageable
{
    //public static event Action GameOverEvent;

    [Header("Stats")]
    [SerializeField] [Range(10f, 200f)] protected float _maxHP = 100;
    [SerializeField] protected float _hp;

    public float maxHP { get { return _maxHP; } }
    public float hp
    {
        get { return _hp; }
        private set => _hp = Mathf.Clamp(value, 0f, _maxHP);
    }


    protected virtual void Start() => _hp = _maxHP;

    public virtual void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            GameOver();
            hp = 0;
        }
    }

    public virtual void TakeHeal(float amount)
    {
        if (hp < maxHP)
        {
            hp += amount;
            hp = Mathf.Min(hp, maxHP); // Limita la vida al máximo
        }

    }

    protected virtual void GameOver()
    {
        Debug.Log("Game Over");
    }
}
