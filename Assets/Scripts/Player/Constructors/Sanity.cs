using System;
using UnityEngine;

[Serializable]
public class Sanity
{
    public static event Action<float> OnSanityChanged;
    public static event Action OnGameWin;
    public static event Action OnSanityGameOver;

    [Tooltip("Maximum sanity")]
    [SerializeField] private float _maxSP = 1000f;
    [Tooltip("Current sanity")]
    [SerializeField] private float _sanity;

    public float MaxSP { get { return _maxSP; } private set { _maxSP = value; } }
    public float CurrentSanity { get { return _sanity; } private set => _sanity = Mathf.Clamp(value, 0f, _maxSP); }

    public Sanity() 
    {
        _sanity = 700f;
        OnSanityChanged?.Invoke(CurrentSanity);
    }

    public void Reduce(float amount)
    {
        CurrentSanity -= amount;
        if (CurrentSanity <= 0) OnSanityGameOver?.Invoke();
        else UpdateSanity();
    }

    public void Heal(float amount)
    {
        if (CurrentSanity < MaxSP)
        {
            CurrentSanity += amount;
            if (CurrentSanity >= _maxSP) OnGameWin.Invoke();
        }

        UpdateSanity();
    }

    public void UpdateSanity() => OnSanityChanged?.Invoke(CurrentSanity);
}
