using System;
using UnityEngine;

[Serializable]
public class Life
{
    public event Action<float> OnHealthChanged;
    public event Action OnHealthGameOver;
    public event Action OnTakeDamage = delegate { };

    //public static event Action GameOverEvent;
    [Tooltip("Health Points")]
    private float _maxHP;
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
        OnTakeDamage?.Invoke();
        if (HP <= 0) GameOver();
        else if (HP > 0 && _isP) UpdateHealth();
    }

    public void Heal(float amount)
    {
        if (HP < MaxHP) HP += amount;

        if (_isP) UpdateHealth();
    }
    public void UpdateHealth() => OnHealthChanged?.Invoke(HP);

    void GameOver()
    {
        if (_isP) OnHealthGameOver?.Invoke();
        else UnityEngine.Object.Destroy(_client);
    }

    public PlayerData GetData()
    {
        return new PlayerData
        {
            maxSP = MaxHP,
            sp = HP
        };
    }
    public void LoadData(PlayerData data)
    {
        MaxHP = data.maxSP;
        HP = data.sp;
        UpdateHealth();
    }
}

[Serializable]
public struct PlayerData
{
    [Tooltip("The maximum stat points")]
    public float maxSP;
    [Tooltip("The current stat points")]
    public float sp;
}
