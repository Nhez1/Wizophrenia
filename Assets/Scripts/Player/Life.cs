using UnityEngine;
using System;

public class Life: MonoBehaviour, IDamageable
{
    //public static event Action GameOverEvent;

    [Header("HP")]
    [SerializeField] protected float HP;
    [SerializeField] [Range(10f, 200f)] protected float maxHP = 100;

    protected virtual void Start() => HP = maxHP;

    public virtual void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            GameOver();
            HP = 0;
        }
    }

    public virtual void TakeHeal(float amount)
    {
        if (HP < maxHP)
        {
            HP += amount;
            HP = Mathf.Min(HP, maxHP); // Limita la vida al máximo
        }

    }

    protected virtual void GameOver()
    {
        Debug.Log("Game Over");
    }
}
