using System;
using UnityEngine;

[Serializable]
public class LifeOther
{
    public event Action OnTakeDamage = delegate { };

    //public static event Action GameOverEvent;
    [Tooltip("Health Points")]
    private float _maxHP;
    [SerializeField] private float _hp;

    public float MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public float HP { get { return _hp; } private set => _hp = Mathf.Clamp(value, 0f, _maxHP); }

    private GameObject _client;

    public LifeOther(float maxHP, GameObject gameObject)
    {
        MaxHP = maxHP;
        HP = MaxHP;

        _client = gameObject;
    }

    public void Damage(float amount)
    {
        if (_maxHP <= 0f)
        {
            Debug.LogWarning("Life not initialized properly!");
            return;
        }

        HP -= amount;
        if (HP <= 0) GameOver();
    }

    public void Heal(float amount)
    {
        if (HP < MaxHP) HP += amount;
    }

    void GameOver()
    {
        UnityEngine.Object.Destroy(_client);
    }
}