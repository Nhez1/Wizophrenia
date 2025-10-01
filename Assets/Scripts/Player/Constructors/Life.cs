using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Life
{
    public static event Action<float> OnHealthChanged;

    //public static event Action GameOverEvent;
    [Tooltip("Maximum HP")]
    [SerializeField] private float _maxHP;
    [Tooltip("Health Points")]
    [SerializeField] private float _hp;

    public float MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public float HP { get { return _hp; } private set => _hp = Mathf.Clamp(value, 0f, _maxHP); }

    private bool p;
    private GameObject _client;

    public Life(bool isPlayer, float maxHP, GameObject gameObject = null)
    {
        MaxHP = maxHP;
        HP = MaxHP;
        p = isPlayer;


        _client = gameObject;
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0) GameOver();
        else UpdateHealth();
    }

    public void TakeHeal(float amount)
    {
        if (HP < MaxHP) HP += amount;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (p) OnHealthChanged?.Invoke(HP);
    }

    public void SetHP(float amount) => HP = amount;
    public void SetMaxHP(float amount) => MaxHP = amount;

    void GameOver()
    {
        if (p) Debug.Log("Game Over");
        else UnityEngine.Object.Destroy(_client);
    }
}
