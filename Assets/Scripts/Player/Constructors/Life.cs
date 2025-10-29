using System;
using UnityEngine;

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

    private bool _isP;
    private GameObject _client;

    public Life(bool isPlayer, float maxHP, GameObject gameObject = null)
    {
        MaxHP = maxHP;
        HP = MaxHP;
        _isP = isPlayer;


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
        if (HP <= 0)
            GameOver();
        else if (_isP)
            UpdateHealth();
    }



    public void Heal(float amount)
    {
        if (HP < MaxHP) HP += amount;

        if (_isP) UpdateHealth();
    }

    public void UpdateHealth() => OnHealthChanged?.Invoke(HP);

    public void SetHP(float amount) => HP = amount;
    public void SetMaxHP(float amount) => MaxHP = amount;

    void GameOver()
    {
        if (_isP) Debug.Log("Wizard dead");
        else UnityEngine.Object.Destroy(_client);
    }
}
