using System;
using UnityEngine;

public class Sanity
{
    public static event Action<float> OnSanityChanged;

    [Tooltip("Maximum sanity")]
    [SerializeField] private float _maxSP;
    [Tooltip("Current sanity")]
    [SerializeField] private float _sanity;

    public float MaxSP { get { return _maxSP; } private set { _maxSP = value; } }
    public float CurrentSanity { get { return _sanity; } private set => _sanity = Mathf.Clamp(value, 0f, _maxSP); }

    public Sanity() { }

    public void Reduce(float amount)
    {
        CurrentSanity -= amount;
        if (CurrentSanity <= 0) GameOver();
        else UpdateSanity();
    }

    public void Heal(float amount)
    {
        if (CurrentSanity < MaxSP) CurrentSanity += amount;
        UpdateSanity();
    }

    public void UpdateSanity() => OnSanityChanged?.Invoke(CurrentSanity);

    void GameOver()
    {
        Debug.Log("Wizard lost his mind");
    }
}
